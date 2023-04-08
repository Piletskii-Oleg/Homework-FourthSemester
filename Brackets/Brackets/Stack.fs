module Stack

let push element stack =
    match stack with
    | [] -> [ element ]
    | _ -> element :: stack

let remove stack = List.tail stack

let peek stack =
    match stack with
    | [] -> None
    | head :: _ -> Some head
