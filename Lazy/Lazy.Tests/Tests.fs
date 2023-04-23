module Lazy.Tests

open System.Threading
open ILazy
open FsUnit
open NUnit.Framework

let lazies supplier : ILazy<int> list =
    [ UnsafeLazy<int>(supplier)
      SafeLazy<int>(supplier)
      LockFreeLazy<int>(supplier) ]

[<Test>]
let ``Lazies should work correctly when run sequentially`` () =
    let supplier = fun () -> 2

    lazies supplier
    |> List.iter (fun someLazy ->
        let myLazy: ILazy<int> = someLazy
        myLazy.Get() |> should equal 2
        myLazy.Get() |> should equal 2
        myLazy.Get() |> should equal 2)

let runParallel<'a> amount (someLazy: ILazy<'a>) =
    let tasks = Array.init amount (fun _ -> async { return someLazy.Get() })
    tasks |> Async.Parallel |> Async.RunSynchronously

[<Test>]
let ``Safe Lazy should not cause races`` () =
    let mutable count = 0

    let safeLazy: ILazy<unit> =
        SafeLazy<unit>(fun () -> count <- Interlocked.Increment(ref count))

    safeLazy |> runParallel 100000 |> ignore

    count |> should equal 1

[<Test>]
let ``Lock free lazy should always return the same object`` () =
    let lockFreeLazy: ILazy<List<int>> = LockFreeLazy<List<int>>(fun () -> [ 2; 4; 11 ])

    lockFreeLazy
    |> runParallel 10000
    |> Array.pairwise
    |> Array.map obj.ReferenceEquals
    |> Array.iter (should be True)
