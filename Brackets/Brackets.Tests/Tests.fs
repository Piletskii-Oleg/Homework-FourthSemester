module Brackets.Tests

open FsUnit
open NUnit.Framework

let assertIs (boolean: bool) list =
    list
    |> List.map check
    |> should equal (List.init list.Length (fun _ -> boolean))

[<Test>]
let ``Correct bracket sequences should return true`` () =
    ["[(())]"; "{}(())[[]]"; "{{((([[[]]])))}}"]
    |> assertIs true
    
[<Test>]
let ``Strings that contain symbols other than brackets but have correct bracket combinations should return true`` () =
    ["(wewWEo[qwe]lkq23q238)"; "(jej[[E[W]lll]002-]{--==++})"]
    |> assertIs true
    
[<Test>]
let ``Incorrect bracket combinations should return false`` () =
    ["(((0))]["; "[[]"; "{{{{{{{{"; "}}}"; "{2}{})"]
    |> assertIs false
    
