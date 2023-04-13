module Program

open Computer
open Network

let comp1 = Computer(Windows, false)
let comp2 = Computer(Linux, true)
let comp3 = Computer(Linux, false)
let comp4 = Computer(Mac, false)
let comp5 = Computer(Windows, false)

let node1 = Node(comp1, 1)
let node2 = Node(comp2, 2)
let node3 = Node(comp3, 3)
let node4 = Node(comp4, 4)
let node5 = Node(comp5, 5)

let edge12 = Edge(node1, node2)
let edge23 = Edge(node2, node3)
let edge24 = Edge(node2, node4)
let edge25 = Edge(node2, node5)

let graph = Graph [node1; node2; node3; node4; node5]
step graph |> ignore // step |> step |> step |> ignore