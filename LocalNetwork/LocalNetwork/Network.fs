module Network

open Computer

type Node(computer: Computer, connections: Connections, id: int) as this =
    let infectConnections () =
        if computer.IsInfected then
            connections.Infect

    let mutable connections = connections

    let tryConnectTo node =
        connections.Edges
        |> List.tryFind (fun (edge: Edge) -> edge.Connects this node)
        |> function
            | None ->
                let edge = Edge(this, node)
                connections.Add(edge)
                Some edge
            | Some _ -> None

    let addEdge edge = connections.Add edge

    member _.Id = id
    member _.Computer = computer
    member _.Connections = connections
    member _.InfectConnections = infectConnections
    member _.AddEdge edge = addEdge edge
    member _.TryConnectTo node = tryConnectTo node

    new(computer: Computer, id: int) = Node(computer, Connections [], id)

and Edge(start: Node, stop: Node, canInfect: bool) =
    let mutable canInfect = canInfect

    let connects node1 node2 =
        (start = node1 && stop = node2) || (start = node2 && stop = node1)

    member _.TryInfectComputers =
        if canInfect then
            start.Computer.TryInfect()
            stop.Computer.TryInfect()

    member _.Infect = canInfect <- true

    member _.Clean = canInfect <- false

    member _.CanInfect = canInfect

    member _.Connects node1 node2 = connects node1 node2

    new(start: Node, stop: Node) as this =
        Edge(start, stop, false)
        then
            start.AddEdge this
            stop.AddEdge this

and Connections(edges: List<Edge>) =
    let mutable edges = edges

    let add edge = edges <- edge :: edges

    member _.Edges = edges

    member _.Infect = edges |> List.iter (fun edge -> edge.Infect)

    member _.TryInfectComputers = edges |> List.iter (fun edge -> edge.TryInfectComputers)

    member _.Clean = edges |> List.iter (fun edge -> edge.Clean)

    member _.Add edge = add edge

and Graph(nodes: List<Node>) =
    member _.Nodes = nodes

    member _.InfectConnections =
        nodes |> List.iter (fun (node: Node) -> node.InfectConnections())

    member _.TryInfectComputers =
        nodes |> List.iter (fun (node: Node) -> node.Connections.TryInfectComputers)

    member _.CleanConnections = nodes |> List.iter (fun node -> node.Connections.Clean)

let step (network: Graph) =
    network.InfectConnections
    network.TryInfectComputers
    network.CleanConnections
    network
