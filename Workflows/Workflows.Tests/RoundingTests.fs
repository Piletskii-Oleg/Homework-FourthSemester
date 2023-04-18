module Workflows.Tests.RoundingTests

open Rounding
open NUnit.Framework
open FsUnit

let rounding = Rounding

[<Test>]
let ``Rounding a number should be correct`` () =
    rounding 3 {
        let! a = 2.23423
        return a
    } |> should (equalWithin 0.00001) 2.234
    
[<Test>]
let ``Calculation in a rounding workflow should return correct result`` () =
    rounding 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    } |> should (equalWithin 0.000001) 0.048
    