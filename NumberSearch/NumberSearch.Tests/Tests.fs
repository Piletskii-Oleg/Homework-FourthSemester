module NumberSearch.Tests

open NUnit.Framework
open FsUnit
open FsCheck

[<Test>]
let ``Search in empty array should return -1`` () =
    let empty_arr_returns_minus_one (elem: 'a) = find_element elem [] = -1 
    Check.QuickThrowOnFailure empty_arr_returns_minus_one
    
[<Test>]
let ``Search should work correctly on int list`` () =
    find_element 2 [2; 5; 0] |> should equal 0
    find_element 5 [2; 5; 0] |> should equal 1
    find_element 0 [2; 5; 0] |> should equal 2
    
[<Test>]
let ``Search should work correctly on string list`` () =
    let list = ["hello"; "and"; "goodbye"]
    find_element "and" list |> should equal 1
    
[<Test>]
let ``Search should return -1 if element is not present on the list`` () =
    find_element 2 [5; 8; 13; 423; 5] |> should equal -1