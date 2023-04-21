module SafeStack

type SafeStack<'a>(stack: List<'a>) =
    let mutable mStack = stack
    let lockObject = obj ()

    let push element =
        let newStack =
            match mStack with
            | [] -> [ element ]
            | list -> (element :: list)
        mStack <- newStack

    let tryPop () =
        match mStack with
        | [] -> None
        | head :: _ ->
            mStack <- List.tail mStack
            Some head

    member _.Stack = mStack

    member _.SafePush element =
        lock lockObject (fun _ -> push element)

    member _.SafeTryPop() = lock lockObject (fun _ -> tryPop ())
    member _.UnsafePush element = push element
    member _.UnsafeTryPop = tryPop