module Program

open Computer
open Network

let comp1 = Computer(Windows, false)
let comp2 = Computer(Linux, false)
let comp3 = Computer(Linux, false)
let comp4 = Computer(Mac, false)
let comp5 = Computer(Windows, true)

let node1 = Node(comp1)
let node2 = Node(comp2)
let node3 = Node(comp3)
let node4 = Node(comp4)
let node5 = Node(comp5)

let edge12 = Edge(node1, node2)
let edge23 = Edge(node2, node3)
let edge24 = Edge(node2, node4)
let edge25 = Edge(node2, node5)

let connections1 = Connections([edge12])
node1.ChangeConnections connections1
let connections2 = Connections([edge12; edge23; edge24; edge25])
let connections3 = Connections [edge23]
let connections4 = Connections [edge24]
let connections5 = Connections [edge25]
node2.ChangeConnections connections2
node3. ChangeConnections connections3
node4.ChangeConnections connections4
node5.ChangeConnections connections5

let graph = Graph [node1; node2; node3; node4; node5]
step graph |> ignore // |> step |> step |> step |> ignore