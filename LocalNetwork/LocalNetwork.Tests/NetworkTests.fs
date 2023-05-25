module LocalNetwork.NetworkTests

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

let getDefaultNodes () =
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

    nodes

[<Test>]
let ``Trying to infect computers when the chance is 1 should work correctly`` () =
    let nodes = getDefaultNodes ()
    let guaranteedGraph = Graph(nodes, infectGuaranteed)
    guaranteedGraph |> step |> ignore

    guaranteedGraph.Nodes[0].Computer.IsInfected |> should be True
    guaranteedGraph.Nodes[2].Computer.IsInfected |> should be True
    guaranteedGraph.Nodes[3].Computer.IsInfected |> should be True
    guaranteedGraph.Nodes[4].Computer.IsInfected |> should be True

[<Test>]
let ``IsFinished property of the graph should be true when all computers are infected`` () =
    let nodes = getDefaultNodes ()
    let guaranteedGraph = Graph(nodes, infectGuaranteed)
    guaranteedGraph |> step |> step |> ignore
    
    guaranteedGraph.IsFinished |> should be True

[<Test>]
let ``Infection should not spread to more than 1 computer far at once`` () =
    let nodes = getDefaultNodes ()
    let guaranteedGraph = Graph(nodes, infectGuaranteed)
    step guaranteedGraph |> ignore

    guaranteedGraph.Nodes[5].Computer.IsInfected |> should be False
    guaranteedGraph.Nodes[6].Computer.IsInfected |> should be False
    
    guaranteedGraph.IsFinished |> should be False

[<Test>]
let ``If the chance of infecting is 0, no computer should be infected after a step`` () =
    let nodes = getDefaultNodes ()
    let safeGraph = Graph(nodes, neverInfect)
    safeGraph |> step |> step |> step |> step |> ignore

    safeGraph.Nodes
    |> List.filter (fun node -> not (node.Id = 1))
    |> List.map (fun node -> node.Computer)
    |> List.map (fun computer -> computer.IsInfected)
    |> should equal (List.init (List.length safeGraph.Nodes - 1) (fun _ -> false))
