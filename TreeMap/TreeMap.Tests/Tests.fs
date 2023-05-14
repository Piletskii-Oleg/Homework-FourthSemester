module TreeMap.Tests

open FsUnit
open NUnit.Framework

[<Test>]
let ``Tree should map correctly`` () =
    let tree = Node(3, LeftBranch(4, Tip 4), Node(6, RightBranch(3, Tip 6), Tip 5))
    let mapped_tree = tree_map (fun x -> x * 2) tree
    mapped_tree |> should equal (Node(6, LeftBranch(8, Tip 8), Node(12, RightBranch(6, Tip 12), Tip 10)))