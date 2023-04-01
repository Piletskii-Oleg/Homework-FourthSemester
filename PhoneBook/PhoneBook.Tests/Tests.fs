module PhoneBook.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``Data from the file should be read correctly`` () =
    let list = parseFile "../../../TestFiles/Data.txt"
    list |> should equal [
        {Name = "John"; Phone = "123456789"};
        {Name = "Igor"; Phone = "+791412312"};
        {Name = "Nadya"; Phone = "89008005535"};
        {Name = "Ivan"; Phone = "88005553533"};
    ]
    
[<Test>]
let ``It should be possible to add values to the phone book`` () =
    []
    |> addRecord {Name = "John"; Phone = "123456789"}
    |> addRecord {Name = "Igor"; Phone = "+791412312"}
    |> addRecord {Name = "Nadya"; Phone = "89008005535"}
    |> should equal [
        {Name = "Nadya"; Phone = "89008005535"}
        {Name = "Igor"; Phone = "+791412312"};
        {Name = "John"; Phone = "123456789"};
    ]

[<Test>]
let ``Finding records by name should be correct`` () =
    let list = [
        {Name = "John"; Phone = "123456789"};
        {Name = "Igor"; Phone = "+791412312"};
        {Name = "Nadya"; Phone = "89008005535"}
    ]
    findByName "Igor" list |> should equal "+791412312"
    findByName "John" list |> should equal "123456789"
    findByName "Nadya" list |> should equal "89008005535"
    
[<Test>]
let ``Finding records by phone should be correct`` () =
    let list = [
        {Name = "John"; Phone = "123456789"};
        {Name = "Igor"; Phone = "+791412312"};
        {Name = "Nadya"; Phone = "89008005535"}
    ]
    findByPhone "+791412312" list |> should equal "Igor"
    findByPhone "123456789" list |> should equal "John"
    findByPhone "89008005535" list |> should equal "Nadya"
    
[<Test>]
let ``Printing should be correct`` () =
    [
        {Name = "John"; Phone = "123456789"};
        {Name = "Igor"; Phone = "+791412312"};
        {Name = "Nadya"; Phone = "89008005535"}
    ]
    |> getPhoneBookString
    |> should equal "John 123456789\nIgor +791412312\nNadya 89008005535\n"
    
[<Test>]
let ``Saving to file should be correct`` () =
    let path = "../../../TestFiles/Saved.txt"
    [
        {Name = "John"; Phone = "123456789"};
        {Name = "Igor"; Phone = "+791412312"};
        {Name = "Nadya"; Phone = "89008005535"}
    ]
    |> saveToFile path
    
    use reader = new System.IO.StreamReader(path)
    
    reader.ReadToEnd() |> should equal "John 123456789\nIgor +791412312\nNadya 89008005535\n"
    reader.Close()
    System.IO.File.Delete path