module PointFree.Tests

open NUnit.Framework
open FsCheck

let check func1 func2 x l = func1 x l = func2 x l

let functions = [ func1; func2; func3; func4; func5; func6; func7; func8 ]

[<Test>]
let ``All steps are correct`` () =
    functions
    |> List.pairwise
    |> List.map (fun (x, y) -> Check.QuickThrowOnFailure (check x y))
    |> ignore