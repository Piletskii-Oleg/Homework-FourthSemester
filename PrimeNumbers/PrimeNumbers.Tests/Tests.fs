module PrimeNumbers.Tests

open FsUnit
open NUnit.Framework

[<Test>]
let ``Prime number check should be correct`` () =
    is_prime 6563 |> should be True
    is_prime 7879 |> should be True
    is_prime 7689 |> should be False

[<Test>]
let ``First values of the list should be prime numbers`` () =
    let primes = [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59 ]

    generate_primes_list
    |> Seq.take (List.length primes)
    |> should equal primes 

[<Test>]
let ``generate_prime_list and primes_list are equivalent`` () =
    generate_primes_list
    |> Seq.take 100
    |> should equal (primes_list |> Seq.take 100)
