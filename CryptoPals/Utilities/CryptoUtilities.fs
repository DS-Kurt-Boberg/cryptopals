﻿module CryptoUtilities
open System
open System.Text
open StringUtilities
open System.Collections
open ByteUtilities

let everyNth n arr = 
    arr |> Array.mapi (fun i el -> el, i)              // Add index to element
        |> Array.filter (fun (el, i) -> i % n = n - 1) // Take every nth element
        |> Array.map fst  

let KeyExtender(input:byte[],length:int) =
    Seq.take length (seq{while true do yield! input})
    |> Seq.toArray

let XORByteArray(input:byte[],key:byte[]) : byte[] =
    let arrKey = KeyExtender(key,input.Length)
    Array.map2 (fun a b -> a ^^^ b) input arrKey
    |> Seq.toArray

let ByteIsValidCharacter(input : byte) =
    let byteInt = Convert.ToInt32(input)
    let validRange = [32..126]
    List.contains byteInt validRange

let BytesAreValidCharacters(input:byte[]) =
    input
        |> Seq.map (fun x -> 
            ByteIsValidCharacter(x)
        )
        |> Seq.forall (fun x -> x = true)

let ScoreListOfCandidates(input : byte[][]) =

    let strings = 
        input
            |> Array.map (fun b -> Encoding.ASCII.GetString(b))
    if strings.Length = 0 then "no solutions" 
    else
        strings
            |> ScoreListOfStrings

let HammingDistance(bytes1 : byte[], bytes2 : byte[]) : int =

    let bitlength = if bytes1.Length = bytes2.Length then (bytes1.Length * 8) else failwith "Byte Array Length Mismatch"
    //convert to bit arrays
    let bits1 = Array.zeroCreate<bool> bitlength
    let bits2 = Array.zeroCreate<bool> bitlength
    BitArray(bytes1).CopyTo(bits1,0) |> ignore
    BitArray(bytes2).CopyTo(bits2,0) |> ignore
    Array.map2((=)) bits1 bits2
    |> Array.sumBy (fun b -> if b then 0 else 1)

let TestKeyLength(input : byte[], length: int) : (int * float ) = 

    let bytes1 = 
        input
        |> Array.take length
    
    let bytes2 = 
        input
        |> Array.skip length
        |> Array.take length
    
    (length,((float (HammingDistance(bytes1,bytes2))) / (float length)))

let ExtractKeyLength(input : byte[], maxLength: int) : int =

    let keyLengths = [2..maxLength]
    let allResults = 
        keyLengths
        |> Seq.map( fun b -> TestKeyLength(input,b))
        |> Seq.sortBy snd
    allResults
        |> Seq.head
        |> fst

let BlockTranspose(input : byte[], blockSize : int) : byte[][] =

    let indexes = [|1..blockSize|]

    indexes
    |>Array.map (fun i -> everyNth i input)

let ExtractKey(input : byte[], keyLength : int) : byte[] =
    let DecryptBytes = IntArrayToByteArray([|0..255|])
    let blocks = BlockTranspose(input,keyLength)

    let testStrings = 
        blocks
        |> Seq.map ( fun testBytes ->
        DecryptBytes
        |> Array.map (fun b ->
            XORByteArray(testBytes,[|b|])
            )
            |> ScoreListOfCandidates
        )
        |> Seq.toArray
            |> ScoreListOfStrings
    [||]

