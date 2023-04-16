module LocalNetwork.Tests

open NUnit.Framework
open Network
open Computer
open FsUnit

[<SetUp>]
let graph =
    let node1 = Node(Computer(Windows), 1)
    let node2 = Node(Computer(Linux, true), 2)
    let node3 = Node(Computer(Linux), 3)
    let node4 = Node(Computer(Mac), 4)
    let node5 = Node(Computer(Windows), 5)

    node1.TryConnectTo node2 |> ignore
    node2.TryConnectTo node3 |> ignore
    node2.TryConnectTo node4 |> ignore
    node2.TryConnectTo node5 |> ignore

    Graph [ node1; node2; node3; node4; node5 ]

[<Test>]
let ``TryConnectTo node should work correctly`` () =
    let node1 = Node(Computer(Mac), 1)
    let node2 = Node(Computer(Windows), 2)

    match node1.TryConnectTo node2 with
    | None -> shouldFail (fun () -> ())
    | Some _ -> ()

    match node2.TryConnectTo node2 with
    | None -> ()
    | Some _ -> shouldFail (fun () -> ())

    match node2.TryConnectTo node1 with
    | None -> ()
    | Some _ -> shouldFail (fun () -> ())

[<Test>]
let ``Adding edges should work correctly`` () =
    let node1 = Node(Computer(Mac), 1)
    let node2 = Node(Computer(Windows), 2)
    let edge = Edge(node1, node2)

    match node1.TryAddEdge edge with
    | None -> shouldFail (fun () -> ())
    | Some _ -> ()

    match node2.TryAddEdge edge with
    | None -> shouldFail (fun () -> ())
    | Some _ -> ()

    node1.Connections |> should equal [ edge ]
    node2.Connections |> should equal [ edge ]

[<Test>]
let ``Adding the same edge twice should fail`` () =
    let node1 = Node(Computer(Mac), 1)
    let node2 = Node(Computer(Windows), 2)
    let edge = Edge(node1, node2)

    match node1.TryAddEdge edge with
    | None -> shouldFail (fun () -> ())
    | Some _ -> ()

    match node1.TryAddEdge edge with
    | None -> ()
    | Some _ -> shouldFail (fun () -> ())

[<Test>]
let ``Edge connectivity check should work correctly`` () =
    let node1 = Node(Computer(Mac), 1)
    let node2 = Node(Computer(Windows), 2)
    let node3 = Node(Computer Linux, 3)
    let edge12 = Edge(node1, node2)
    let edge23 = Edge(node2, node3)

    edge12.Connects node1 node2 |> should be True
    edge12.Connects node2 node1 |> should be True

    edge23.Connects node2 node3 |> should be True
    edge23.Connects node3 node2 |> should be True

    edge12.Connects node1 node3 |> should be False
    edge12.Connects node2 node3 |> should be False

    edge23.Connects node1 node3 |> should be False
    edge23.Connects node1 node2 |> should be False

[<Test>]
let ``Infecting connections should make all edges able to infect`` () =
    graph.InfectConnections

    graph.Nodes
    |> List.map (fun node -> node.Connections)
    |> List.map (fun edges -> edges |> List.map (fun edge -> edge.CanInfect))
    |> List.iter (fun canInfectList -> canInfectList |> List.iter (fun canInfect -> canInfect |> should be True))

[<Test>]
let ``Trying to infect computers should work correctly`` () =
    graph.InfectConnections
    graph.TryInfectComputers
