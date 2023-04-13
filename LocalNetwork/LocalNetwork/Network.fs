module Network

open Computer

type Node(computer: Computer, connections: Connections) =
    let infectConnections =
        if computer.IsInfected then
            connections.Infect
            
    let mutable mConnections = connections

    member _.Computer = computer
    member _.Connections = mConnections
    member _.InfectConnections = infectConnections
    member _.ChangeConnections newConnections = mConnections <- newConnections

    new(computer: Computer) = Node(computer, Connections [])

and Edge(start: Node, stop: Node, canInfect: bool) =
    let mutable mCanInfect = canInfect

    member _.TryInfectComputers =
        if canInfect then
            start.Computer.TryInfect
            stop.Computer.TryInfect

    member _.Infect = mCanInfect <- true

    member _.Clean = mCanInfect <- false

    new(start: Node, stop: Node) = Edge(start, stop, false)

and Connections(edges: List<Edge>) =
    member _.Infect = edges |> List.iter (fun edge -> edge.Infect)

    member _.TryInfectComputers = edges |> List.iter (fun edge -> edge.TryInfectComputers)

    member _.Clean = edges |> List.iter (fun edge -> edge.Clean)

    member _.Add edge = edge :: edges

and Graph(graph: List<Node>) =
    member _.InfectConnections =
        graph |> List.iter (fun (node: Node) -> node.InfectConnections)

    member _.TryInfectComputers =
        graph |> List.iter (fun (node: Node) -> node.Connections.TryInfectComputers)

    member _.CleanConnections = graph |> List.iter (fun node -> node.Connections.Clean)

let step (network: Graph) =
    network.InfectConnections
    network.TryInfectComputers
    network.CleanConnections
    network
