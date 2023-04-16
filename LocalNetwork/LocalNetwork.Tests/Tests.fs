module LocalNetwork.Tests

open NUnit.Framework
open Network
open Computer
open FsUnit

[<SetUp>]
let graph =
    let comp1 = Computer(Windows)
    let comp2 = Computer(Linux, true)
    let comp3 = Computer(Linux)
    let comp4 = Computer(Mac)
    let comp5 = Computer(Windows)

    let node1 = Node(comp1, 1)
    let node2 = Node(comp2, 2)
    let node3 = Node(comp3, 3)
    let node4 = Node(comp4, 4)
    let node5 = Node(comp5, 5)

    node1.TryConnectTo node2 |> ignore
    node2.TryConnectTo node3 |> ignore
    node2.TryConnectTo node4 |> ignore
    node2.TryConnectTo node5 |> ignore

    Graph [ node1; node2; node3; node4; node5 ]

[<Test>]
let ``Infecting connections should make all edges able to infect`` () =
    graph.InfectConnections

    graph.Nodes
    |> List.map (fun node -> node.Connections)
    |> List.map (fun connections -> connections.Edges)
    |> List.map (fun edges -> edges |> List.map (fun edge -> edge.CanInfect))
    |> List.iter (fun canInfectList -> canInfectList |> List.iter (fun canInfect -> canInfect |> should be True))
    
[<Test>]
let ``Trying to infect computers should work correctly`` () =
    graph.InfectConnections
    graph.TryInfectComputers
    