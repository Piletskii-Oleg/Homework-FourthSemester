module PhoneBook

open System.IO

type PhoneRecord =
    { Phone: string;
      Name: string }
    
let addRecord value phoneBook : PhoneRecord list =
    value :: phoneBook
   
let parseFile (path: string) =
    use reader = new StreamReader(path)
    let wholeString = reader.ReadToEnd()
    
    wholeString.Split "\n"
    |> Seq.map (fun (string: string) -> string.Split())
    |> Seq.map (fun element -> {Name = element[0]; Phone = element[1]})
    |> Seq.toList
    
let findByName value phoneBook=
    let rec iterate listCopy =
        match listCopy with
        | [] -> "not found"
        | head :: tail ->
            match head with
            | {Phone = phone; Name = name} when name = value -> phone
            | _ -> iterate tail
    iterate phoneBook
    
let findByPhone value phoneBook =
    let rec iterate listCopy =
        match listCopy with
        | [] -> "not found"
        | head :: tail ->
            match head with
            | {Phone = phone; Name = name} when phone = value -> name
            | _ -> iterate tail
    iterate phoneBook
    
let getPhoneBookString phoneBook =
    let rec getPhoneBookLine phoneBookString phoneBookTail = 
        match phoneBookTail with
        | [] -> phoneBookString
        | head :: tail -> getPhoneBookLine (phoneBookString + $"%s{head.Name} %s{head.Phone}\n") tail
    getPhoneBookLine "" phoneBook
    
let saveToFile (path: string) phoneBook =
    use writer = new StreamWriter(path)
    phoneBook |> getPhoneBookString |> writer.Write