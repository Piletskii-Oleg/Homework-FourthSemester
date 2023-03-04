let reverse_list ls =
    let rec reverse ls_copy acc =
        match ls_copy with
        | head :: tail -> reverse tail (head :: acc)
        | [] -> acc
    reverse ls []
printfn $"%A{reverse_list [3; 5]}"