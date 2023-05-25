module Lazy.Tests

open System.Threading
open NUnit.Framework
open ILazy
open FsUnit

let lazies supplier : ILazy<obj> list =
    [ UnsafeLazy<obj>(supplier)
      BlockingLazy<obj>(supplier)
      LockFreeLazy<obj>(supplier) ]

let runParallel<'a> amount (someLazy: ILazy<'a>) =
    Array.init amount (fun _ -> async { return someLazy.Get() }) |> Async.Parallel

[<Test>]
let ``Lazies should work correctly when run sequentially`` () =
    let supplier = fun () -> obj ()

    lazies supplier
    |> List.iter (fun someLazy ->
        let myLazy: ILazy<obj> = someLazy
        let result = myLazy.Get()
        obj.ReferenceEquals(result, myLazy.Get()) |> should be True
        obj.ReferenceEquals(result, myLazy.Get()) |> should be True
        obj.ReferenceEquals(result, myLazy.Get()) |> should be True)

[<Test>]
let ``Blocking Lazy should not cause races and return the same object`` () =
    let manualResetEvent = new ManualResetEvent false
    let count = ref 0

    let supplier () =
        manualResetEvent.WaitOne() |> ignore
        Interlocked.Increment count |> ignore
        obj ()
        
    let safeLazy: ILazy<obj> = BlockingLazy<obj>(supplier)
    
    let calculationsCount = 10
    let calculations = safeLazy |> runParallel calculationsCount
    manualResetEvent.Set() |> ignore

    calculations
    |> Async.RunSynchronously
    |> Array.pairwise
    |> Array.map obj.ReferenceEquals
    |> should equal (Array.init (calculationsCount - 1) (fun _ -> true))

    count.Value |> should equal 1

[<Test>]
let ``Lock free lazy should always return the same object`` () =
    let manualResetEvent = new ManualResetEvent false

    let supplier () =
        manualResetEvent.WaitOne() |> ignore
        obj ()

    let someLazy: ILazy<obj> = LockFreeLazy<obj>(supplier)

    let calculationsCount = 10
    let calculations = someLazy |> runParallel calculationsCount

    manualResetEvent.Set() |> ignore

    calculations
    |> Async.RunSynchronously
    |> Array.pairwise
    |> Array.map obj.ReferenceEquals
    |> should equal (Array.init (calculationsCount - 1) (fun _ -> true))
