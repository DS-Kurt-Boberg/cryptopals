module StringUtilities 
open System
open System.Globalization
open System.Text.RegularExpressions
let MostCommonLetters = ['E';'T';'A';'O';'I';'N';' ';'S';'H';'R';'D';'L';'U']
let CharRegex = "[a-zA-Z\s]"

let stringtoBytes(input : string, numberStyle:NumberStyles) = 
    let byteLength = input.Length / 2
    let bytes = Array.init byteLength (fun i -> Byte.Parse(input.Substring(i*2,2),numberStyle))
    bytes
    
let ByteToBase64(input : byte[]) =
    Convert.ToBase64String(input)
    
let ValidateString(testString : string, expectedString : string) = 
    match testString with
        | _expectedString ->
            "pass"
        | _ ->
            "expected " + expectedString + " got " + testString

let CharIsValid(input : char) =
    let isMatch =
        Regex.IsMatch((input.ToString()),CharRegex)
    isMatch
            
let ScoreText(input : string) =
    let CharByFreq =
        input
        |> Seq.countBy id
        |> Seq.sortBy (snd >> (~-))
        |> Seq.toList
        
    let intermediate =
        CharByFreq
        |> List.filter (fun tpl -> CharIsValid (fst(tpl)))
    let score = 
        intermediate
        |> Seq.sumBy snd
    score

let ScoreListOfStrings(input:string[]) =
    let strings = 
        input 
            |> Array.map(fun s -> (s,ScoreText(s)))
    let sortedStrings =
        strings
            |> Array.sortByDescending snd
            |> Array.head
    sortedStrings
        |> fst
