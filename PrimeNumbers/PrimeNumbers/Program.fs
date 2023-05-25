module PrimeNumbers

let is_prime n =
    let rec check_divisor i =
        if i = n / 2 + 1 then true
        else if n % i = 0 then false
        else check_divisor (i + 1)

    match n with
    | 0
    | 1 -> false
    | _ -> check_divisor 2

let rec next_prime n =
    if is_prime (n + 1) then n + 1 else next_prime (n + 1)

let generate_primes_list = Seq.initInfinite id |> Seq.filter is_prime

let primes_list = Seq.unfold (fun state -> Some(state, next_prime state)) 2
