module Tests.RhombusTests

open NUnit.Framework
open FsUnit
open Rhombus

[<Test>]
let ``Rhombus of width 4 must be correct`` () =
    getRhombus 4
    |> should equal [ "   *   "; "  ***  "; " ***** "; "*******"; " ***** "; "  ***  "; "   *   " ]

[<Test>]
let ``Getting rhombus with negative or zero size must return empty string`` () =
    List.init 10 (fun x -> -x)
    |> List.iter (fun x -> getRhombus x |> should equal [ "" ])
