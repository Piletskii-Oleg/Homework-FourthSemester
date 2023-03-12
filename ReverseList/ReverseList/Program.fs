module ReverseList

/// Takes a list and returns a reversed copy of it.
let reverse_list ls =
    let rec reverse ls_copy acc =
        match ls_copy with
        | head :: tail -> reverse tail (head :: acc)
        | [] -> acc
    reverse ls []