module Lazy.Tests

open System.Threading
open ILazy
open FsUnit
open NUnit.Framework

let lazies supplier : ILazy<int> list =
    [ UnsafeLazy<int>(supplier); SafeLazy<int>(supplier) ]

[<Test>]
let ``Unsafe lazy should work correctly`` () =
    let supplier = fun () -> 2
    lazies supplier
    |> List.iter (fun someLazy ->
        let myLazy: ILazy<int> = someLazy
        myLazy.Get() |> should equal 2
        myLazy.Get() |> should equal 2
        myLazy.Get() |> should equal 2)

[<Test>]
let ``Safe Lazy should not cause races`` () =
    let mutable count = 0
    let safeLazy: ILazy<unit> = SafeLazy<unit>(fun () -> count <- Interlocked.Increment(ref count))

    let tasksAmount = 1000000
    let getValue = async { safeLazy.Get() }
    let tasks = Array.init tasksAmount (fun _ -> getValue)

    tasks
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

    count |> should equal 1
