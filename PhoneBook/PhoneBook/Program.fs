open System.IO
open PhoneBook

let options =
    "0 - quit\n" + "1 - read from file\n" + "2 - add record\n" + "3 - find name by phone\n"
    + "4 - find phone by name\n" + "5 - print everything\n" + "6 - save current data to file"


[<EntryPoint>]
let main args =
   printf $"%s{options}"
   0