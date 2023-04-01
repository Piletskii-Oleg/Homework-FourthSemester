﻿module PhoneBook

open System.IO

/// A record that stores name of a person and his phone number
type PhoneRecord =
    { Phone: string;
      Name: string }
    
/// Adds a record to the phone book
let addRecord value phoneBook : PhoneRecord list =
    value :: phoneBook
   
/// Gets a phone book from the file
let parseFile (path: string) =
    use reader = new StreamReader(path)
    let wholeString = reader.ReadToEnd()
    
    wholeString.Split "\n"
    |> Seq.map (fun (string: string) -> string.Split())
    |> Seq.map (fun element -> {Name = element[0]; Phone = element[1]})
    |> Seq.toList
    
/// Finds phone number of a person by his name
let findByName value phoneBook=
    let rec iterate listCopy =
        match listCopy with
        | [] -> "not found"
        | head :: tail ->
            match head with
            | {Phone = phone; Name = name} when name = value -> phone
            | _ -> iterate tail
    iterate phoneBook
    
/// Finds name of a person by his phone number
let findByPhone value phoneBook =
    let rec iterate listCopy =
        match listCopy with
        | [] -> "not found"
        | head :: tail ->
            match head with
            | {Phone = phone; Name = name} when phone = value -> name
            | _ -> iterate tail
    iterate phoneBook
    
/// Returns a string composed of PhoneRecords
let getPhoneBookString phoneBook =
    let rec getPhoneBookLine phoneBookString phoneBookTail = 
        match phoneBookTail with
        | [] -> phoneBookString
        | head :: tail -> getPhoneBookLine (phoneBookString + $"%s{head.Name} %s{head.Phone}\n") tail
    getPhoneBookLine "" phoneBook
    
/// Saves a phone book to the file
let saveToFile (path: string) phoneBook =
    use writer = new StreamWriter(path)
    phoneBook |> getPhoneBookString |> writer.Write