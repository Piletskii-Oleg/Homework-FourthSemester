module LambdaInterpreter.Tests

open FsUnit
open NUnit.Framework
open LambdaInterpreter

[<Test>]
let ``K I should equal K_star`` () =
    let K = Abs("x", Abs("y", Var "x"))
    let K_star = Abs("y", Abs("x", Var "x"))
    let I = Abs("x", Var "x")
    betaReduce (App(K, I)) |> should equal K_star
    
[<Test>]
let ``I I should equal I`` () =
    let I = Abs("x", Var "x")
    betaReduce (App(I, I)) |> should equal I
    
[<Test>]
let ``Variable name should change if there is conflict`` () =
    let term = App(Abs("x", Abs("z", App(Var "x", Var "y"))), App(Var "y", Var "z"))
    let result = Abs("a", App(App(Var "y", Var "z"), Var "y"))
    betaReduce term |> should equal result