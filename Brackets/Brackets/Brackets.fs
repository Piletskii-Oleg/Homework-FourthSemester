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

let parse : string -> List<Symbol> =
    Seq.toList >> List.map transformElement >> List.filter filterElement

let check sequence =
    let rec check_elements stack seq =
        match seq with
        | [] when stack <> [] -> false
        | [] -> true
        | _ -> 
            match List.head seq with
            | OpeningCurly | OpeningParentheses | OpeningSquare ->
                check_elements (Stack.push (Seq.head seq) stack) (List.tail seq)
            | ClosingCurly ->
                match Stack.peek stack with
                | None -> false
                | Some OpeningCurly -> check_elements (Stack.remove stack) (List.tail seq)
                | _ -> false
            | ClosingParentheses ->
                match Stack.peek stack with
                | None -> false
                | Some OpeningParentheses -> check_elements (Stack.remove stack) (List.tail seq)
                | _ -> false
            | ClosingSquare ->
                match Stack.peek stack with
                | None -> false
                | Some OpeningSquare -> check_elements (Stack.remove stack) (List.tail seq)
                | _ -> false
            | Other _ -> false
    sequence |> parse |> check_elements []