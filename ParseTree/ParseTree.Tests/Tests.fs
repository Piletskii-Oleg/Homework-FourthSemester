module ParseTree.Tests

open FsUnit
open NUnit.Framework

[<Test>]
let ``Tree should be evaluated correctly`` () =
    let tree1 = Multiply(Sum(Element 4, Element 2), Divide(Subtract(Element 7, Element -7), Element 7))
    let tree2 = Sum(Sum(Element 4, Sum(Element 6, Element -1)), Divide(Element 6, Element 3))
    calculate tree1 |> should equal 12
    calculate tree2 |> should equal 11