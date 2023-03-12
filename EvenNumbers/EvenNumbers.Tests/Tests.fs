module EvenNumbers.Tests

open FsUnit
open FsCheck
open NUnit.Framework

[<Test>]
let ``Counting function 1 must work correctly`` () =
    count_even1 [2; 5; 6; 8; 11] |> should equal 3
    count_even1 [3; 66; -4; 50; 12] |> should equal 4

[<Test>]
let ``Counting functions must be equal`` () =
    let first_equals_second ls = count_even1 ls = count_even2 ls
    let second_equals_third ls = count_even2 ls = count_even3 ls
    let first_equals_third ls = count_even1 ls = count_even3 ls
    Check.QuickThrowOnFailure first_equals_second
    Check.QuickThrowOnFailure second_equals_third
    Check.QuickThrowOnFailure first_equals_third