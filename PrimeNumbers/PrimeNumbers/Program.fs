module PrimeNumbers
    
let is_prime n =
    let rec check_divisor i =
        match i with
        | _ when i = n / 2 + 1 -> true
        | _ when n % i = 0 -> false
        | _ -> check_divisor (i + 1)
    match n with
    | 0 | 1 -> false
    | _ -> check_divisor 2

let rec next_prime n =
    match n with
    | _ when is_prime (n + 1) -> n + 1
    | _ -> next_prime (n + 1)

let generate_primes_list = Seq.initInfinite id |> Seq.filter is_prime

let primes_list = Seq.unfold (fun state -> Some(state, next_prime state)) 2