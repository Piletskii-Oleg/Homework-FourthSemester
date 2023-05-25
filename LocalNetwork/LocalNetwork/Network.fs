module Network

open Computer

let tryAdd element list =
    list
    |> List.contains element
    |> function
        | false -> Some(element :: list)
        | true -> None

type Node(computer: Computer, connections: List<Edge>, id: int) as this =
    let mutable mConnections = connections

    let infectConnections () =
        mConnections |> List.iter (fun edge -> edge.Infect)

    let tryInfectConnections () =
        if computer.IsInfected then
            infectConnections ()

    let tryInfectComputers infectChance =
        mConnections |> List.iter (fun edge -> edge.TryInfectComputers infectChance)

    let clearConnections () =
        mConnections |> List.iter (fun edge -> edge.Clean)

    let tryConnectTo node =
        if node = this then
            None
        else
            mConnections
            |> List.tryFind (fun edge -> edge.Connects this node)
            |> function
                | None ->
                    let edge = Edge(this, node)

                    this.TryAddEdge edge |> ignore
                    node.TryAddEdge edge |> ignore

                    Some edge
                | Some _ -> None

    member _.Id = id
    member _.Computer = computer
    member _.Connections = mConnections
    member _.IsInfected = computer.IsInfected

    member _.TryInfectConnections() = tryInfectConnections ()
    member _.TryInfectComputers infectChance = tryInfectComputers infectChance
    member _.CleanConnections() = clearConnections ()

    member _.TryConnectTo node = tryConnectTo node

    member _.TryAddEdge edge =
        match tryAdd edge mConnections with
        | None -> None
        | Some newList ->
            mConnections <- newList
            Some edge

    new(computer: Computer, id: int) = Node(computer, [], id)

and Edge(start: Node, stop: Node, canInfect: bool) =
    let mutable mCanInfect = canInfect

    let connects node1 node2 =
        (start = node2 && stop = node1) || (start = node1 && stop = node2)

    member _.TryInfectComputers infectChance =
        if mCanInfect then
            start.Computer.TryInfect infectChance
            stop.Computer.TryInfect infectChance

    member _.Infect = mCanInfect <- true

    member _.Clean = mCanInfect <- false

    member _.CanInfect = mCanInfect

    member _.Connects node1 node2 = connects node1 node2

    new(start: Node, stop: Node) = Edge(start, stop, false)

type Graph(nodes: List<Node>, infectChance: InfectChance) =
    let infectConnections () =
        nodes |> List.iter (fun node -> node.TryInfectConnections())

    let tryInfectComputers () =
        nodes |> List.iter (fun node -> node.TryInfectComputers infectChance)

    let cleanConnections () =
        nodes |> List.iter (fun node -> node.CleanConnections())

    member _.Nodes = nodes

    member _.DoStep() =
        infectConnections ()
        tryInfectComputers ()
        cleanConnections ()

    member _.IsFinished = nodes |> List.forall (fun node -> node.IsInfected)

    new(nodes: List<Node>) = Graph(nodes, baseInfectChance)

let logState (graph: Graph) =
    let computers =
        graph.Nodes
        |> List.map (fun node -> node.Computer)
        |> List.map (fun computer -> computer.ToString)

    let computerIds =
        graph.Nodes
        |> List.map (fun node -> node.Id)
        |> List.map (fun id -> id.ToString())

    List.zip computerIds computers

let step (network: Graph) =
    network.DoStep()
    printfn $"%A{logState network}"
    network
