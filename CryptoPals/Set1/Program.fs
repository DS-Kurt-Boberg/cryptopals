open System
open Set1
[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    printfn "Set 1 Challenge 1"
    printfn "%s" Set1.Challenge1.Solution
    printfn "Set 1 Challenge 2"
    printfn "%s" Set1.Challenge2.Solution
    printfn "Set 1 Challenge 3"
    printfn "%s" Set1.Challenge3.Solution
    printfn "Set 1 Challenge 4"
    printfn "%s" Set1.Challenge4.Solution
    printfn "Set 1 Challenge 5"
    printfn "%s" Set1.Challenge5.Solution
    printfn "Set 1 Challenge 6"
    printfn "%s" Set1.Challenge6.Solution
    Console.ReadKey() |> ignore
    0 // return an integer exit code
