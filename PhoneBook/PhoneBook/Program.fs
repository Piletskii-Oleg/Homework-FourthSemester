let options =
    "0 - quit\n" + "1 - read from file\n" + "2 - add record\n" + "3 - find name by phone\n"
    + "4 - find phone by name\n" + "5 - print everything\n" + "6 - save current data to file\n"

let rec waitForInput phoneBook =
   printf "Please input a command: "
   let input = System.Int32.Parse(System.Console.ReadLine())
   match input with
   | 0 -> ()
   | 1 ->
       printf "Please enter full path to the file: "
       let path = System.Console.ReadLine()
       waitForInput (PhoneBook.parseFile path)
   | 2 ->
       printfn "Please enter the name and the phone that should be added."
       printf "Name: "
       let name = System.Console.ReadLine()
       printf "Phone: "
       let phone = System.Console.ReadLine()
       waitForInput (PhoneBook.addRecord {Name = name; Phone = phone} phoneBook)
   | 3 ->
       printf "Please enter the name by which the phone should be found: "
       let name = System.Console.ReadLine()
       printfn $"%s{PhoneBook.findByName name phoneBook}"
       waitForInput phoneBook
   | 4 ->
       printf "Please enter the phone by which the name should be found: "
       let phone = System.Console.ReadLine()
       printfn $"%s{PhoneBook.findByPhone phone phoneBook}"
       waitForInput phoneBook
   | 5 ->
       printfn $"%s{PhoneBook.getPhoneBookString phoneBook}"
       waitForInput phoneBook
   | 6 ->
       printf "Please enter full path to the file: "
       let path = System.Console.ReadLine()
       PhoneBook.saveToFile path phoneBook
       waitForInput phoneBook
   | _ ->
       printfn $"%s{options}"
       waitForInput phoneBook
       

[<EntryPoint>]
let main _ =
   printfn $"%s{options}"
   waitForInput []
   0