namespace PhoneBook

open System.ComponentModel.DataAnnotations
open EntityFrameworkCore.FSharp.Extensions
open Microsoft.EntityFrameworkCore

[<CLIMutable>]
type PhoneRecord =
        { [<Key>] Id: int;
        Phone: int;
        Name: string }
    
type PhoneBookContext () =
    inherit DbContext ()
    
    [<DefaultValue>] val mutable phoneRecords : DbSet<PhoneRecord>
    member this.PhoneRecords
        with get() = this.phoneRecords
        and set value = this.phoneRecords <- value
    
    override _.OnModelCreating builder =
        builder.RegisterOptionTypes()
        
    override _.OnConfiguring(options: DbContextOptionsBuilder) : unit =
        options.UseSqlite("Data Source=phoneRecords.db").UseFSharpTypes()
        |> ignore