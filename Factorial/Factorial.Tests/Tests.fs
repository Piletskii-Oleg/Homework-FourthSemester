module Factorial.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``Factorial should give correct results`` () =
    factorial 0 |> should equal 1
    factorial 5 |> should equal 120
    factorial 15 |> should equal 1307674368000L
    
[<Test>]
let ``Factorial of negative numbers should return -1`` () =
    List.init 10000 (fun x -> -x)
    |> List.tail // first element is 0
    |> List.map factorial
    |> List.iter (fun x -> x |> should equal -1L)