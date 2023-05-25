module Computer

open System

type OS =
    | Windows
    | Linux
    | Mac

let baseInfectChance =
    function
    | Windows -> 0.8
    | Linux -> 0.1
    | Mac -> 0.4

type InfectChance = OS -> float

let random = Random()

let getRandom () = random.Next(0, 101) |> Convert.ToDouble

type Computer(system: OS, isInfected: bool) =
    let mutable mIsInfected = isInfected

    let infect () =
        mIsInfected <- true
        printfn $"%A{system} is infected!"

    let tryInfect infectChance =
        if getRandom () / 100.0 <= infectChance system && not mIsInfected then
            infect ()

    member _.TryInfect = tryInfect
    member _.IsInfected = mIsInfected
    member _.System = system
    member _.ToString = $"%A{system} is infected: %b{mIsInfected}"

    new(system: OS) = Computer(system, false)
