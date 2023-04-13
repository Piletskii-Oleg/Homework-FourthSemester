let options =
    "\"quit\" - quit\n"
    + "\"read\" - read from file\n"
    + "\"add\" - add record\n"
    + "\"findPhone\" - find name by phone\n"
    + "\"findName\" - find phone by name\n"
    + "\"print\" - print everything\n"
    + "\"save\" - save current data to file\n"

let rec processInput phoneBook =
    printf "Please input a command: "
    let input = System.Console.ReadLine()

    match input with
    | "quit" -> ()
    | "read" ->
        printf "Please enter full path to the file: "
        let path = System.Console.ReadLine()
        processInput (PhoneBook.parseFile path)
    | "add" ->
        printfn "Please enter the name and the phone that should be added."
        printf "Name: "
        let name = System.Console.ReadLine()
        printf "Phone: "
        let phone = System.Console.ReadLine()
        processInput (PhoneBook.addRecord { Name = name; Phone = phone } phoneBook)
    | "findPhone" ->
        printf "Please enter the name by which the phone should be found: "
        let name = System.Console.ReadLine()
        printfn $"%s{PhoneBook.findByName name phoneBook}"
        processInput phoneBook
    | "findName" ->
        printf "Please enter the phone by which the name should be found: "
        let phone = System.Console.ReadLine()
        printfn $"%s{PhoneBook.findByPhone phone phoneBook}"
        processInput phoneBook
    | "print" ->
        printfn $"%s{PhoneBook.getPhoneBookString phoneBook}"
        processInput phoneBook
    | "save" ->
        printf "Please enter full path to the file: "
        let path = System.Console.ReadLine()
        PhoneBook.saveToFile path phoneBook
        processInput phoneBook
    | _ ->
        printfn $"%s{options}"
        processInput phoneBook

[<EntryPoint>]
let main _ =
    printfn $"%s{options}"
    processInput []
    0
