module PhoneBook

open System.IO

type PhoneRecord = { Phone: string; Name: string }

let addRecord value phoneBook : PhoneRecord list = value :: phoneBook

let parseFile (path: string) =
    use reader = new StreamReader(path)
    let wholeString = reader.ReadToEnd()

    wholeString.Split "\n"
    |> Seq.map (fun (string: string) -> string.Split())
    |> Seq.map (fun element ->
        { Name = element[0]
          Phone = element[1] })
    |> Seq.toList

let findByName value phoneBook =
    phoneBook
    |> List.tryFind (fun record -> value = record.Name)
    |> function
        | None -> "not found"
        | Some record -> record.Phone

let findByPhone value phoneBook =
    phoneBook
    |> List.tryFind (fun record -> value = record.Phone)
    |> function
        | None -> "not found"
        | Some record -> record.Name

let getPhoneBookString phoneBook =
    let rec getPhoneBookLine phoneBookString phoneBookTail =
        match phoneBookTail with
        | [] -> phoneBookString
        | head :: tail -> getPhoneBookLine (phoneBookString + $"%s{head.Name} %s{head.Phone}\n") tail

    getPhoneBookLine "" phoneBook

let saveToFile (path: string) phoneBook =
    use writer = new StreamWriter(path)
    phoneBook |> getPhoneBookString |> writer.Write
