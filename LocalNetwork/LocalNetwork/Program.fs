module Program

open Network
open Computer

let nodes =
    [ Node(Computer Windows, 0)
      Node(Computer(Linux, true), 1)
      Node(Computer Linux, 2)
      Node(Computer Mac, 3)
      Node(Computer Windows, 4)
      Node(Computer Mac, 5)
      Node(Computer Linux, 6) ]

nodes[0].TryConnectTo nodes[1] |> ignore
nodes[1].TryConnectTo nodes[2] |> ignore
nodes[1].TryConnectTo nodes[3] |> ignore
nodes[1].TryConnectTo nodes[4] |> ignore
nodes[2].TryConnectTo nodes[5] |> ignore
nodes[2].TryConnectTo nodes[6] |> ignore
nodes[3].TryConnectTo nodes[6] |> ignore

let graph = Graph nodes

graph |> step |> ignore
