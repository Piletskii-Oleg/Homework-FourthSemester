module Program

type Term =
    | Variable of string
    | Application of Term * Term
    | Abstraction of string * Term
    
let rec isVarFree var term =
    match (var, term) with
    | Variable x, Variable y when x = y -> true
    | Variable x, Application(term1, term2) -> (isVarFree (Variable x) term1) && (isVarFree (Variable x) term2)
    | Variable x, Abstraction(var, term) when not (x = var) -> true
    | _, _ -> false
    
let rec isVarBound var term =
    match (var, term) with
    | Variable x, Variable y -> true
    | Variable x, Application(term1, term2) -> (isVarBound (Variable x) term1) && (isVarBound (Variable x) term2)
    | Variable x, Abstraction(var, term) when x = var -> true
    | _, _ -> false
    
let rec substitute term1 var term2 =
    match (term1, var, term2) with
    | Variable item, Variable var, term when item = var -> term
    | Variable item, Variable var, term when not (item = var) -> Variable item
    | Application (firstTerm, secondTerm), Variable var, term ->
        Application(substitute firstTerm (Variable var) term, substitute secondTerm (Variable var) term)
    | Abstraction(item, term), Variable var, Variable var1 -> Abstraction(item, term)
    | Abstraction(item, firstTerm), Variable var, secondTerm
        when (not(isVarFree (Variable item) secondTerm) || not(isVarFree (Variable var) secondTerm)) ->
        Abstraction(item, (substitute firstTerm (Variable var) secondTerm))

let a = substitute (Variable "x") (Variable "x") (Variable "y")

let rec printTerm term =
    match term with
    | Variable var -> printf $"%s{var}"
    | Application(term, term1) -> printf "("; printTerm term; printf " "; printTerm term1; printf ")"
    | Abstraction(item, term) -> printf($"L%s{item}."); printTerm term
    
printTerm a
printfn ""
        
// let rec beta term1 term2 = 
//     
//     
// let rec calculate (term: Term<'a>) =
//     match term with
//     | Variable item -> item
//     | Abstraction(var, term) -> calculate term var
//     | Application(term, term1) -> beta term1 term2
        
