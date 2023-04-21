module Tests.SupermapTests

open NUnit.Framework
open FsUnit
open Supermap

[<Test>]
let ``Supermap should work in general case`` () =
    [ 1.0; 2.0 ]
    |> supermap (fun x -> [ x + 1.0; x * 10.0 ])
    |> should equal [ 2.0; 10.0; 3.0; 20.0 ]

[<Test>]
let ``Empty list should stay empty after supermap`` () =
    [] |> supermap (fun x -> [ x; x + 1 ]) |> should equal List.empty<int>
