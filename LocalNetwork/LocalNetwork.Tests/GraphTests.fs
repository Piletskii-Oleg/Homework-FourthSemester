module LocalNetwork.GraphTests

open NUnit.Framework
open Network
open Computer
open FsUnit

[<Test>]
let ``TryConnectTo node should work correctly`` () =
    let node1 = Node(Computer Mac, 1)
    let node2 = Node(Computer Windows, 2)

    let edge12 = node1.TryConnectTo node2
    edge12.IsSome |> should be True

    let edge22 = node2.TryConnectTo node2
    edge22.IsNone |> should be True

    let edge21 = node2.TryConnectTo node1
    edge21.IsNone |> should be True

[<Test>]
let ``Adding edges should work correctly`` () =
    let node1 = Node(Computer Mac, 1)
    let node2 = Node(Computer Windows, 2)
    let edge = Edge(node1, node2)

    let added1 = node1.TryAddEdge edge
    added1.IsSome |> should be True

    let added2 = node2.TryAddEdge edge
    added2.IsSome |> should be True

    node1.Connections |> should equal [ edge ]
    node2.Connections |> should equal [ edge ]

[<Test>]
let ``Adding the same edge twice should fail`` () =
    let node1 = Node(Computer Mac, 1)
    let node2 = Node(Computer Windows, 2)
    let edge = Edge(node1, node2)

    let added = node1.TryAddEdge edge
    added.IsSome |> should be True

    let addedAgain = node1.TryAddEdge edge
    addedAgain.IsNone |> should be True

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
