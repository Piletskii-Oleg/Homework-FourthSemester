module Stack

type Stack<'a> = Stack of List<'a>

let push element (stack: Stack<'a>) =
    match stack with
    | Stack [] -> Stack [ element ]
    | Stack list -> Stack(element :: list)

let pop stack =
    match stack with
    | Stack [] -> Stack []
    | Stack list -> Stack(List.tail list)

let peek stack =
    match stack with
    | Stack [] -> None
    | Stack(head :: _) -> Some head
