module PhoneBook.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``Data from the file should be read correctly`` () =
    let book = parseFile "../../../TestFiles/Data.txt"

    let expected =
        [ { Name = "John"; Phone = "123456789" }
          { Name = "Igor"; Phone = "+791412312" }
          { Name = "Nadya"
            Phone = "89008005535" }
          { Name = "Ivan"; Phone = "88005553533" } ]

    book |> should equal (Book expected)


[<Test>]
let ``It should be possible to add values to the phone book`` () =
    let expected =
        [ { Name = "Nadya"
            Phone = "89008005535" }
          { Name = "Igor"; Phone = "+791412312" }
          { Name = "John"; Phone = "123456789" } ]

    Book []
    |> addRecord { Name = "John"; Phone = "123456789" }
    |> addRecord { Name = "Igor"; Phone = "+791412312" }
    |> addRecord
        { Name = "Nadya"
          Phone = "89008005535" }
    |> should equal (Book expected)

[<Test>]
let ``Finding records by name should be correct`` () =
    let list =
        [ { Name = "John"; Phone = "123456789" }
          { Name = "Igor"; Phone = "+791412312" }
          { Name = "Nadya"
            Phone = "89008005535" } ]

    let book = Book list

    findByName "Igor" book |> should equal "+791412312"
    findByName "John" book |> should equal "123456789"
    findByName "Nadya" book |> should equal "89008005535"

[<Test>]
let ``Finding records by phone should be correct`` () =
    let list =
        [ { Name = "John"; Phone = "123456789" }
          { Name = "Igor"; Phone = "+791412312" }
          { Name = "Nadya"
            Phone = "89008005535" } ]

    let book = Book list

    findByPhone "+791412312" book |> should equal "Igor"
    findByPhone "123456789" book |> should equal "John"
    findByPhone "89008005535" book |> should equal "Nadya"

[<Test>]
let ``Printing should be correct`` () =
    Book
        [ { Name = "John"; Phone = "123456789" }
          { Name = "Igor"; Phone = "+791412312" }
          { Name = "Nadya"
            Phone = "89008005535" } ]
    |> getPhoneBookString
    |> should equal "John 123456789\nIgor +791412312\nNadya 89008005535\n"

[<Test>]
let ``Saving to file should be correct`` () =
    let path = "../../../TestFiles/Saved.txt"

    Book
        [ { Name = "John"; Phone = "123456789" }
          { Name = "Igor"; Phone = "+791412312" }
          { Name = "Nadya"
            Phone = "89008005535" } ]
    |> saveToFile path

    use reader = new System.IO.StreamReader(path)

    reader.ReadToEnd()
    |> should equal "John 123456789\nIgor +791412312\nNadya 89008005535\n"

    reader.Close()
    System.IO.File.Delete path

[<Test>]
let ``Adding same person twice should only add them once`` () =
    Book []
    |> addRecord { Name = "John"; Phone = "123456789" }
    |> addRecord { Name = "John"; Phone = "123456789" }
    |> should equal (Book [ { Name = "John"; Phone = "123456789" } ])
    
[<Test>]
let ``Searching for non-existent records should return 'not found'`` () =
    let book = Book [ { Name = "John"; Phone = "123456789" }]
    book |> findByName "Santa" |> should equal "not found"
    book |> findByPhone "91283222" |> should equal "not found"
    
[<Test>]
let ``String of an empty book should be empty`` () =
    Book []
    |> getPhoneBookString
    |> should equal ""