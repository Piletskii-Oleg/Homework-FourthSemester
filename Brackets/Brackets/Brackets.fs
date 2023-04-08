module Brackets

type Symbol =
    | OpeningSquare
    | ClosingSquare
    | OpeningParentheses
    | ClosingParentheses
    | OpeningCurly
    | ClosingCurly
    | Other of char

let transformElement element =
    match element with
    | '{' -> OpeningCurly
    | '}' -> ClosingCurly
    | '(' -> OpeningParentheses
    | ')' -> ClosingParentheses
    | '[' -> OpeningSquare
    | ']' -> ClosingSquare
    | symbol -> Other symbol

let filterElement element =
    match element with
    | Other _ -> false
    | _ -> true

let parse: string -> List<Symbol> =
    Seq.toList >> List.map transformElement >> List.filter filterElement

let rec checkElements stack seq =
    let matchBracket symbol stack seq func =
        match Stack.peek stack with
        | None -> false
        | Some bracket when symbol = bracket -> func (Stack.remove stack) (List.tail seq)
        | _ -> false

    match seq with
    | [] when stack <> [] -> false
    | [] -> true
    | _ ->
        match List.head seq with
        | OpeningCurly
        | OpeningParentheses
        | OpeningSquare -> checkElements (Stack.push (Seq.head seq) stack) (List.tail seq)
        | ClosingCurly -> matchBracket OpeningCurly stack seq checkElements
        | ClosingParentheses -> matchBracket OpeningParentheses stack seq checkElements
        | ClosingSquare -> matchBracket OpeningSquare stack seq checkElements
        | Other _ -> false

let check sequence = sequence |> parse |> checkElements []
