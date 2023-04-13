open System

type OS =
    | Windows
    | Linux
    | Mac

type Computer = { system: OS; isInfected: bool }

type Node =
    { computer: Computer
      adjacent: List<Node> }

let infectChance =
    function
    | Windows -> 0.8
    | Linux -> 0.1
    | Mac -> 0.4

let random = Random()
let generate = random.Next(0, 101) |> Convert.ToDouble |> (/) 100.0

let tryInfect computer =
    if generate > infectChance computer.system then
        { computer with isInfected = true }
    else
        computer

let tryInfectAdjacent node =
    { node with
        adjacent =
            List.map
                (fun adjacent ->
                    { adjacent with
                        computer = tryInfect adjacent.computer })
                node.adjacent }

let step network = List.map
