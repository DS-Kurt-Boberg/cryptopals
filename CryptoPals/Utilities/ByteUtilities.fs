module ByteUtilities
open System

let XOR(b1:byte,b2:byte) = b1 ^^^ b2

let IntArrayToByteArray(input:int[])=
    input
    |> Array.map (fun x -> 
        if x > 255 then failwith "Out of bounds: > 255" else if x < 0 then failwith "Out of bounds: < 0" else x)
    |> Array.map ( fun x -> Convert.ToByte(x))

let ValidateBytes(testBytes : byte[],expectedBytes : byte[]) =
    match testBytes with
        | _outBytes ->
           "pass"
        | _ ->
            "expected " + BitConverter.ToString(expectedBytes) + " got " + BitConverter.ToString(testBytes)