module Program

open LambdaInterpreter

let rec printTerm term =
    match term with
    | Var var -> printf $"%s{var}"
    | App(term, term1) -> printf "("; printTerm term; printf " "; printTerm term1; printf ")"
    | Abs(var, term) -> printf($"L%s{var}."); printTerm term
    
let term = App(Abs("x", Abs("z", App(Var "x", Var "y"))), App(Var "y", Var "z"))
term |> printTerm
printfn ""
term |> betaReduce |> printTerm