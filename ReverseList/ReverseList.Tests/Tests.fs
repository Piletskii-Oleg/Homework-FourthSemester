module ReverseList.Tests

open FsCheck
open NUnit.Framework

[<Test>]
let ``Reversing twice gives back the original list`` () =
    let reverse_twice_correct (list:list<'a>) = reverse_list (reverse_list list) = list
    Check.QuickThrowOnFailure reverse_twice_correct
    
    
[<Test>]
let ``Reversing the list works correctly`` () =
    let reverse_is_correct (list:list<'a>) = reverse_list list = List.rev list
    Check.QuickThrowOnFailure reverse_is_correct