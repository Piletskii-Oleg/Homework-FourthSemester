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

type Computer(system: OS, isInfected: bool) =
    let mutable mIsInfected = isInfected

    let tryInfect infectChance =
        let number = random.Next(0, 101) |> Convert.ToDouble

        if number / 100.0 < infectChance system && not mIsInfected then
            mIsInfected <- true
            printfn $"%A{system} is infected!"

    member this.TryInfect ?chance =
        match chance with
        | None -> tryInfect infectChance
        | Some newChance -> tryInfect newChance

    member this.IsInfected = isInfected
    member this.System = system

    new(system: OS) = Computer(system, false)
