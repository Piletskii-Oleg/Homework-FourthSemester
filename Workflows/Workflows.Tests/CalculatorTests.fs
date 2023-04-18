module Workflows.Tests.CalculatorTests

open Calculator
open NUnit.Framework
open FsUnit

let calculator = StringCalculator()

[<Test>]
let ``Calculator should be able to conduct operations with correct numbers`` () =
    calculator {
        let! a = "1"
        let! b = "2"
        let sum = a + b
        return sum
    }
    |> should equal (Some 3)

    calculator {
        let! a = "12"
        let! b = "4"
        return a * b
    }
    |> should equal (Some 48)

    calculator {
        let! a = "12"
        let! b = "4"
        return a / b
    }
    |> should equal (Some 3)

    calculator {
        let! a = "12"
        let! b = "4"
        return a - b
    }
    |> should equal (Some 8)

[<Test>]
let ``Calculator should return None on incorrect input`` () =
    calculator {
        let! a = "ss"
        let! b = "2"
        return a + b
    }
    |> should equal None

    calculator {
        let! a = "40"
        let! b = "lol"
        return a - b
    }
    |> should equal None
