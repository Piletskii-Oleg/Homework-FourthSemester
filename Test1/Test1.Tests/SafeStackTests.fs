module Tests.SafeStackTests

open NUnit.Framework
open FsUnit
open SafeStack

[<Test>]
let ``Single-threaded Push and TryPop should work correctly`` () =
    let stack = SafeStack([ 2; 3 ])
    stack.SafePush 4
    stack.Stack |> should equal [ 4; 2; 3 ]

    stack.SafeTryPop() |> should equal (Some 4)
    stack.SafeTryPop() |> should equal (Some 2)
    stack.SafeTryPop() |> should equal (Some 3)
    stack.SafeTryPop() |> should equal None

[<Test>]
let ``Multi-threaded safe Push should work correctly`` () =
    let stack = SafeStack([ 2; 3 ])
    let asyncPush element = async { return stack.SafePush element }
    List.init 10000 asyncPush |> Async.Parallel |> Async.RunSynchronously |> ignore

    stack.Stack.Length |> should equal 10002

[<Test>]
let ``Unsafe Push should not work correctly`` () =
    let stack = SafeStack([ 2; 3 ])

    let asyncPush element =
        async { return stack.UnsafePush element }

    List.init 1000000 asyncPush
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

    stack.Stack.Length |> should not' (equal 1000002)

[<Test>]
let ``Safe TryPop should work correctly`` () =
    let stack = SafeStack(List.init 10000 id)
    let asyncTryPop _ = async { return stack.SafeTryPop() }

    List.init 10000 asyncTryPop
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

    stack.Stack.Length |> should equal 0
    stack.SafeTryPop() |> should equal None

[<Test>]
let ``Unsafe TryPop should not work correctly`` () =
    let stack = SafeStack(List.init 1000000 id)
    let asyncTryPop _ = async { return stack.UnsafeTryPop() }

    List.init 1000000 asyncTryPop
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

    stack.Stack.Length |> should not' (equal 0)
    stack.SafeTryPop() |> should not' (equal None)
