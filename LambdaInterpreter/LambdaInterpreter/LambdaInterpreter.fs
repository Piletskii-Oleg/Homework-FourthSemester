module LambdaInterpreter

type Variable = string
    
type Term =
    | Var of Variable
    | App of Term * Term
    | Abs of Variable * Term
    
let rec getFreeVariables term =
    match term with
    | Var var -> Set.singleton var
    | Abs(var, term) -> Set.remove var (getFreeVariables term)
    | App(term1, term2) -> Set.union (getFreeVariables term1) (getFreeVariables term2)
    
let isVarFree var term =
    term |> getFreeVariables |> Set.contains var
    
let rec getFreeVar set =
    let rec getNextVar var =
        if set |> Set.contains var then getNextVar ("'" + var)
        else var
    getNextVar "a"

let rec printTerm term =
    match term with
    | Var var -> printf $"%s{var}"
    | App(term, term1) -> printf "("; printTerm term; printf " "; printTerm term1; printf ")"
    | Abs(var, term) -> printf($"L%s{var}."); printTerm term
    
let rec substitute term var subst =
    match term with
    | Var x when x = var -> subst
    | Var _ -> term
    | App(term1, term2) -> App (substitute term1 var subst, substitute term2 var subst)
    | Abs(x, _) when x = var -> term
    | Abs(y, absTerm) when (isVarFree y term && isVarFree var absTerm) |> not ->
        Abs(y, substitute absTerm var term)
    | Abs(y, absTerm) -> 
        let newVar = getFreeVar (Set.union (getFreeVariables absTerm) (getFreeVariables subst))
        Abs(newVar, substitute (substitute absTerm y (Var newVar)) var subst)
        
let rec betaReduce expression =
    match expression with
    | Var _ -> expression
    | Abs (var, term) -> Abs (var, betaReduce term)
    | App (Abs(var, term1), term2) -> substitute term1 var term2
    | App (term1, term2) -> App (betaReduce term1, betaReduce term2)
    
let term = App(Abs("x", App(Var("x"), Var("y"))), Var ("a"))
printTerm term
printfn ""
term |> betaReduce |> printTerm
    
