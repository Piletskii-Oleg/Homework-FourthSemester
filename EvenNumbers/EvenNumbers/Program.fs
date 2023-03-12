module EvenNumbers

let count_even1 = List.map (fun x -> if x % 2 = 0 then 1 else 0) >> List.sum

let count_even2 = List.filter (fun x -> x % 2 = 0) >> List.length

let count_even3 = 0 |> List.fold (fun count n -> if n % 2 = 0 then count + 1 else count)