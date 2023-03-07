module Factorial.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``Factorial should give correct results`` () =
    factorial 0 |> should equal 1
    factorial 5 |> should equal 120
    factorial 15 |> should equal 1307674368000L