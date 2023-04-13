module Computer

open System

type OS =
    | Windows
    | Linux
    | Mac

let infectChance =
    function
    | Windows -> 0.8
    | Linux -> 0.1
    | Mac -> 0.4

let random = Random()
let generate =
    let number = random.Next(0, 101) |> Convert.ToDouble
    number / 100.0

type Computer(system: OS, isInfected: bool) =
    let mutable mIsInfected = isInfected

    let tryInfect =
        if generate > infectChance system && not mIsInfected then
            mIsInfected <- true
            printfn $"%A{system} is infected!"

    member this.TryInfect = tryInfect
    member this.IsInfected = isInfected
    member this.System = system
