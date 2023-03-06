module NumberSearch

/// Returns index of the element in the list;
/// returns -1 if the element is not present in the list.
let find_element element ls =
    let rec check_element i ls_copy =
       match ls_copy with
       | [] -> -1
       | head :: _ when head = element -> i
       | _ :: tail -> check_element (i + 1) tail
    check_element 0 ls
printfn $"%A{find_element 4 [1; 3; 5]}, %A{find_element 5 [1; 3; 5]}"
