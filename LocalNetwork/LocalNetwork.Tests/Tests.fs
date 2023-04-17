module LocalNetwork.Tests

open NUnit.Framework
open Network
open Computer
open FsUnit

let infectGuaranteed =
    function
    | Windows -> 1.0
    | Linux -> 1.0
    | Mac -> 1.0
    
let neverInfect =
    function
    | Windows -> 0.0
    | Linux -> 0.0
    | Mac -> 0.0

[<SetUp>]
let nodes =
    [ Node(Computer Windows, 0)
      Node(Computer(Linux, true), 1)
      Node(Computer Linux, 2)
      Node(Computer Mac, 3)
      Node(Computer Windows, 4)
      Node(Computer Mac, 5)
      Node(Computer Linux, 6) ]

[<SetUp>]
let connectNodes =
    nodes[0].TryConnectTo nodes[1] |> ignore
    nodes[1].TryConnectTo nodes[2] |> ignore
    nodes[1].TryConnectTo nodes[3] |> ignore
    nodes[1].TryConnectTo nodes[4] |> ignore
    nodes[2].TryConnectTo nodes[5] |> ignore
    nodes[2].TryConnectTo nodes[6] |> ignore
    nodes[3].TryConnectTo nodes[6] |> ignore

[<SetUp>]
let graph = Graph nodes

[<SetUp>]
let guaranteedGraph = Graph(nodes, infectGuaranteed)

let safeGraph = Graph(nodes, neverInfect)

[<Test>]
let ``TryConnectTo node should work correctly`` () =
    let node1 = Node(Computer Mac, 1)
    let node2 = Node(Computer Windows, 2)

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
    let node1 = Node(Computer Mac, 1)
    let node2 = Node(Computer Windows, 2)
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
    let node1 = Node(Computer Mac, 1)
    let node2 = Node(Computer Windows, 2)
    let edge = Edge(node1, node2)

    match node1.TryAddEdge edge with
    | None -> shouldFail (fun () -> ())
    | Some _ -> ()

    match node1.TryAddEdge edge with
    | None -> ()
    | Some _ -> shouldFail (fun () -> ())

[<Test>]
let ``Edge connectivity check should work correctly`` () =
    let node1 = Node(Computer Mac, 1)
    let node2 = Node(Computer Windows, 2)
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
let ``Infecting connections should make edges next to infected computers able to infect`` () =
    graph.InfectConnections

    graph.Nodes
    |> List.filter (fun node -> node.Computer.IsInfected)
    |> List.map (fun node -> node.Connections)
    |> List.map (fun edges -> edges |> List.map (fun edge -> edge.CanInfect))
    |> List.iter (fun canInfectList -> canInfectList |> List.iter (should be True))

[<Test>]
let ``Trying to infect computers when the chance is 1 should work correctly`` () =
    step guaranteedGraph |> ignore

    nodes[0].Computer.IsInfected |> should be True
    nodes[2].Computer.IsInfected |> should be True
    nodes[3].Computer.IsInfected |> should be True
    nodes[4].Computer.IsInfected |> should be True

[<Test>]
let ``Infection should not spread to more than 1 computer far at once`` () =
    step guaranteedGraph |> ignore

    nodes[5].Computer.IsInfected |> should be False
    nodes[6].Computer.IsInfected |> should be False

[<Test>]
let ``Cleaning connections should work correctly`` () =
    graph.InfectConnections
    graph.CleanConnections

    graph.Nodes
    |> List.map (fun node -> node.Connections)
    |> List.map (fun edges -> edges |> List.map (fun edge -> edge.CanInfect))
    |> List.iter (fun canInfectList -> canInfectList |> List.iter (should be False))
    
[<Test>]
let ``If the chance of infecting is 0, no computer should be infected after a step`` () =
    safeGraph |> step |> step |> step |> step |> ignore
    
    safeGraph.Nodes
    |> List.filter (fun node -> not (node.Id = 1))
    |> List.map (fun node -> node.Computer)
    |> List.iter (fun computer -> computer.IsInfected |> should be False)
