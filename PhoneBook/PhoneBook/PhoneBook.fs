module PhoneBook

open System.IO

type PhoneRecord = { Phone: string; Name: string }

type Book = Book of PhoneRecord list

let addRecord value (Book book) =
    if book |> List.contains value then
        Book book
    else
        Book(value :: book)

let parseFile (path: string) =
    use reader = new StreamReader(path)
    let wholeString = reader.ReadToEnd()

    wholeString.Split "\n"
    |> Seq.map (fun (string: string) -> string.Split())
    |> Seq.map (fun element ->
        { Name = element[0]
          Phone = element[1] })
    |> Seq.toList
    |> Book

let findByName value (Book book) =
    book
    |> List.tryFind (fun record -> value = record.Name)
    |> function
        | None -> "not found"
        | Some record -> record.Phone

let findByPhone value (Book book) =
    book
    |> List.tryFind (fun record -> value = record.Phone)
    |> function
        | None -> "not found"
        | Some record -> record.Name

let getPhoneBookString (Book book) =
    let rec getPhoneBookLine phoneBookString phoneBookTail =
        match phoneBookTail with
        | [] -> phoneBookString
        | head :: tail -> getPhoneBookLine (phoneBookString + $"%s{head.Name} %s{head.Phone}\n") tail

    getPhoneBookLine "" book

let saveToFile (path: string) book =
    use writer = new StreamWriter(path)
    book |> getPhoneBookString |> writer.Write
