module Fibonacci.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``First 10 fibonacci numbers should be calculated correctly`` () =
    List.init 10 fibonacci |> should equalSeq [0; 1; 1; 2; 3; 5; 8; 13; 21; 34]
    
[<Test>]
let ``N-th Fibonacci term should be calculated correctly`` () =
    fibonacci 16 |> should equal 987
    fibonacci 30 |> should equal 832040
    
[<Test>]
let ``Applying function to a negative number gives -1`` () =
    List.init 10000 (fun x -> -x)
    |> List.tail // first element is 0
    |> List.map fibonacci
    |> List.iter (fun x -> x |> should equal -1L)