let args : string array = fsi.CommandLineArgs |> Array.tail

let varName = args.[0]
let absolutePath = 
    args.[1]
    |> System.IO.Path.GetFullPath

let formattedLines =
    seq {
        yield sprintf "let %s =" varName
        yield "    [|"

        let lines = System.IO.File.ReadAllLines(absolutePath)

        let linesSurroundedByQuotes = 
            lines 
            |> Array.map (fun line -> sprintf "        \"%s\"" line)

        yield! linesSurroundedByQuotes
        yield "    |]"
    }
    |> String.concat "\n"

System.IO.File.WriteAllText(absolutePath, formattedLines)

//printfn "%A" formattedLines