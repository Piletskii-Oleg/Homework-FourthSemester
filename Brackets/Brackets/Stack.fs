module Stack

let push element stack =
    match stack with
    | [] -> [element]
    | _ -> element :: stack
    
let remove (stack: List<'a>) =
    List.tail stack
    
let peek (stack: List<'a>) =
    match stack with
    | [] -> None
    | head :: _ -> Some head