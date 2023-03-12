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
    let list = generate_primes_list |> Seq.take 40 |> Seq.toList
    List.filter is_prime list = list |> should be True
    
[<Test>]
let ``generate_prime_list and primes_list are equivalent`` () =
    generate_primes_list |> Seq.take 100 |> Seq.compareWith Operators.compare (primes_list |> Seq.take 100) |> should equal 0