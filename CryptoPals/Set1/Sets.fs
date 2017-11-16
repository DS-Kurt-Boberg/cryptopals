module Set1
open StringUtilities
open CryptoUtilities
open ByteUtilities
open System.Globalization
open System
open System.IO
open System.Text

module Challenge1 =
    let Solution : string =
        let hexByteString = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d"
        let outString = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t"

        let testString = Convert.ToBase64String(stringtoBytes(hexByteString,NumberStyles.HexNumber))
        ValidateString(testString,outString)

module Challenge2 =
    let Solution : string =
        let byte1 = stringtoBytes("1c0111001f010100061a024b53535009181c",NumberStyles.HexNumber)
        let byte2 = stringtoBytes("686974207468652062756c6c277320657965",NumberStyles.HexNumber)

        let outBytes = stringtoBytes("746865206b696420646f6e277420706c6179",NumberStyles.HexNumber)

        let testBytes = Array.map2 (fun i j -> i ^^^ j) byte1 byte2
        ValidateBytes(testBytes,outBytes)

module Challenge3 =
    let Solution : string = 
        let testBytes = stringtoBytes("1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736",NumberStyles.HexNumber)
        let DecryptBytes = IntArrayToByteArray([|0..255|])

        DecryptBytes
            |> Array.map( fun b -> 
                XORByteArray(testBytes,[|b|])
                )
            |> ScoreListOfCandidates

module Challenge4= 
    let Solution : string =
        let allLines =
            File.ReadAllLines(".\Set1Challenge4.txt")

        let DecryptBytes = IntArrayToByteArray([|0..255|])
        
        let testStrings = 
            allLines
            |> Seq.map(fun s -> stringtoBytes(s,NumberStyles.HexNumber))
            |> Seq.map ( fun testBytes ->
                DecryptBytes
                |> Array.map (fun b ->
                    XORByteArray(testBytes,[|b|])
                    )
                |> ScoreListOfCandidates
                )
                |> Seq.toArray

        if testStrings.Length = 0 then "No Solutions" 
        else
            testStrings
                |> ScoreListOfStrings
           
module Challenge5 = 

    let Solution : string = 

        let plaintext1 = "Burning 'em, if you ain't quick and nimble"
        let plaintext2 = "I go crazy when I hear a cymbal"
        let cipherbytes1 = stringtoBytes("0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272",NumberStyles.HexNumber)
        let cipherbytes2 = stringtoBytes("a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f",NumberStyles.HexNumber)

        let key = Encoding.ASCII.GetBytes("ICE")

        let encryptedbytes1 = XORByteArray(Encoding.ASCII.GetBytes(plaintext1),key)
        let encryptedbytes2 = XORByteArray(Encoding.ASCII.GetBytes(plaintext2),key)

        let pass1 =
            match encryptedbytes1 with
            | cipherbytes1 ->
                "pass"
            | _ ->
                "fail"
        let pass2 = 
            match encryptedbytes2 with
            | cipherbytes2 ->
                "pass"
            | _ -> 
                "fail"
        pass1 + ", " + pass2

module Challenge6 =

    let Solution : string =
        let testbytes1 = Encoding.ASCII.GetBytes("this is a test")
        let testbytes2 = Encoding.ASCII.GetBytes("wokka wokka!!!")

        printfn "%s" (HammingDistance(testbytes1,testbytes2).ToString())

        let allLines =
            File.ReadAllLines(".\Set1Challenge6.txt")

        let byteStream = 
            allLines 
            |> Seq.map (fun s -> Convert.FromBase64String(s))
            |> Seq.concat
            |> Seq.toArray

        let key = ExtractKeyLength(byteStream,40)

        key.ToString()