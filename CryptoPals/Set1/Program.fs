open System
open Set1
[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    printfn "%s" Set1.Challenge1.Solution
    printfn "%s" Set1.Challenge2.Solution
    printfn "%s" Set1.Challenge3.Solution
    printfn "%s" Set1.Challenge4.Solution
    printfn "%s" Set1.Challenge5.Solution
    printfn "%s" Set1.Challenge6.Solution
    Console.ReadKey() |> ignore
    0 // return an integer exit code
