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

    node1.ConnectTo node2
    node2.ConnectTo node1

    Graph [ node1; node2; node3; node4; node5 ]