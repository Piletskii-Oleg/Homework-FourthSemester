module PointFree.Tests

open NUnit.Framework
open FsCheck

[<Test>]
let ``Step 1 is correct`` () =
    let func1equals2 x l = func1 x l = func2 x l
    Check.QuickThrowOnFailure func1equals2
    
[<Test>]
let ``Step 2 is correct`` () =
    let func2equals3 x l = func2 x l = func3 x l
    Check.QuickThrowOnFailure func2equals3
    
[<Test>]
let ``Step 3 is correct`` () =
    let func3equals4 x l = func3 x l = func4 x l
    Check.QuickThrowOnFailure func3equals4
    
[<Test>]
let ``Step 4 is correct`` () =
    let func4equals5 x l = func4 x l = func5 x l
    Check.QuickThrowOnFailure func4equals5
    
[<Test>]
let ``Step 5 is correct`` () =
    let func5equals6 x l = func5 x l = func6 x l
    Check.QuickThrowOnFailure func5equals6
    
[<Test>]
let ``Step 6 is correct`` () =
    let func6equals7 x l = func6 x l = func7 x l
    Check.QuickThrowOnFailure func6equals7
    
[<Test>]
let ``Step 7 is correct`` () =
    let func7equals8 x l = func7 x l = func8 x l
    Check.QuickThrowOnFailure func7equals8