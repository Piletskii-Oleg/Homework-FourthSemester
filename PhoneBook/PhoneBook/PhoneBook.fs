module PhoneBook

open System.IO

type PhoneRecord =
    { Phone: string;
      Name: string }
    
let addRecord phoneBook value : PhoneRecord list =
    value :: phoneBook
   
let parseFile (stream: Stream) =
    use reader = new StreamReader(stream)
    let wholeString = reader.ReadToEnd()
    
    wholeString.Split "\n"
    |> Seq.map (fun (string: string) -> string.Split())
    |> Seq.map (fun [|phone; name|] -> {Phone = phone; Name = name})
    |> Seq.toList
    
let findByName phoneBook value =
    let rec iterate listCopy =
        match listCopy with
        | [] -> "not found"
        | head :: tail ->
            match head with
            | {Phone = phone; Name = name} when name = value -> phone
            | _ -> iterate tail
    iterate phoneBook
    
let findByPhone phoneBook value =
    let rec iterate listCopy =
        match listCopy with
        | [] -> "not found"
        | head :: tail ->
            match head with
            | {Phone = phone; Name = name} when phone = value -> name
            | _ -> iterate tail
    iterate phoneBook
    
let rec print phoneBook =
    match phoneBook with
    | [] -> printfn ""
    | head :: tail -> printfn $"%s{head.Name} - %s{head.Phone}"; print tail
    
let saveToFile phoneBook (path: string) =
    use writer = new StreamWriter(path)
    let rec writeLine list =
        match list with
        | [] -> writer.Close
        | head :: tail -> writer.WriteLine $"%s{head.Name} %s{head.Phone}"; writeLine tail
    writeLine phoneBook