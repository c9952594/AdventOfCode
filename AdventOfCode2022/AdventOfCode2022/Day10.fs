module AdventOfCode2022_Day10

open NUnit.Framework

let input =
    [|
        "addx 1"
        "noop"
        "addx 2"
        "noop"
        "addx 3"
        "addx 3"
        "addx 1"
        "addx 5"
        "addx 1"
        "noop"
        "noop"
        "addx 4"
        "noop"
        "noop"
        "addx -9"
        "addx 16"
        "addx -1"
        "noop"
        "addx 5"
        "addx -2"
        "addx 4"
        "addx -35"
        "addx 2"
        "addx 28"
        "noop"
        "addx -23"
        "addx 3"
        "addx -2"
        "addx 2"
        "addx 5"
        "addx -8"
        "addx 19"
        "addx -8"
        "addx 2"
        "addx 5"
        "addx 5"
        "addx -14"
        "addx 12"
        "addx 2"
        "addx 5"
        "addx 2"
        "addx -13"
        "addx -23"
        "noop"
        "addx 1"
        "addx 5"
        "addx -1"
        "addx 2"
        "addx 4"
        "addx -9"
        "addx 10"
        "noop"
        "addx 6"
        "addx -11"
        "addx 12"
        "addx 5"
        "addx -25"
        "addx 30"
        "addx -2"
        "addx 2"
        "addx -5"
        "addx 12"
        "addx -37"
        "noop"
        "noop"
        "noop"
        "addx 24"
        "addx -17"
        "noop"
        "addx 33"
        "addx -32"
        "addx 3"
        "addx 1"
        "noop"
        "addx 6"
        "addx -13"
        "addx 17"
        "noop"
        "noop"
        "noop"
        "addx 12"
        "addx -4"
        "addx -2"
        "addx 2"
        "addx 3"
        "addx 4"
        "addx -35"
        "addx -2"
        "noop"
        "addx 20"
        "addx -13"
        "addx -2"
        "addx 5"
        "addx 2"
        "addx 23"
        "addx -18"
        "addx -2"
        "addx 17"
        "addx -10"
        "addx 17"
        "noop"
        "addx -12"
        "addx 3"
        "addx -2"
        "addx 2"
        "noop"
        "addx 3"
        "addx 2"
        "noop"
        "addx -13"
        "addx -20"
        "noop"
        "addx 1"
        "addx 2"
        "addx 5"
        "addx 2"
        "addx 5"
        "noop"
        "noop"
        "noop"
        "noop"
        "noop"
        "addx 1"
        "addx 2"
        "addx -18"
        "noop"
        "addx 26"
        "addx -1"
        "addx 6"
        "noop"
        "noop"
        "noop"
        "addx 4"
        "addx 1"
        "noop"
        "noop"
        "noop"
        "noop"
    |]

let example =
    [|
        "addx 15"
        "addx -11"
        "addx 6"
        "addx -3"
        "addx 5"
        "addx -1"
        "addx -8"
        "addx 13"
        "addx 4"
        "noop"
        "addx -1"
        "addx 5"
        "addx -1"
        "addx 5"
        "addx -1"
        "addx 5"
        "addx -1"
        "addx 5"
        "addx -1"
        "addx -35"
        "addx 1"
        "addx 24"
        "addx -19"
        "addx 1"
        "addx 16"
        "addx -11"
        "noop"
        "noop"
        "addx 21"
        "addx -15"
        "noop"
        "noop"
        "addx -3"
        "addx 9"
        "addx 1"
        "addx -3"
        "addx 8"
        "addx 1"
        "addx 5"
        "noop"
        "noop"
        "noop"
        "noop"
        "noop"
        "addx -36"
        "noop"
        "addx 1"
        "addx 7"
        "noop"
        "noop"
        "noop"
        "addx 2"
        "addx 6"
        "noop"
        "noop"
        "noop"
        "noop"
        "noop"
        "addx 1"
        "noop"
        "noop"
        "addx 7"
        "addx 1"
        "noop"
        "addx -13"
        "addx 13"
        "addx 7"
        "noop"
        "addx 1"
        "addx -33"
        "noop"
        "noop"
        "noop"
        "addx 2"
        "noop"
        "noop"
        "noop"
        "addx 8"
        "noop"
        "addx -1"
        "addx 2"
        "addx 1"
        "noop"
        "addx 17"
        "addx -9"
        "addx 1"
        "addx 1"
        "addx -3"
        "addx 11"
        "noop"
        "noop"
        "addx 1"
        "noop"
        "addx 1"
        "noop"
        "noop"
        "addx -13"
        "addx -19"
        "addx 1"
        "addx 3"
        "addx 26"
        "addx -30"
        "addx 12"
        "addx -1"
        "addx 3"
        "addx 1"
        "noop"
        "noop"
        "noop"
        "addx -9"
        "addx 18"
        "addx 1"
        "addx 2"
        "noop"
        "noop"
        "addx 9"
        "noop"
        "noop"
        "noop"
        "addx -1"
        "addx 2"
        "addx -37"
        "addx 1"
        "addx 3"
        "noop"
        "addx 15"
        "addx -21"
        "addx 22"
        "addx -6"
        "addx 1"
        "noop"
        "addx 2"
        "addx 1"
        "noop"
        "addx -10"
        "noop"
        "noop"
        "addx 20"
        "addx 1"
        "addx 2"
        "addx 2"
        "addx -6"
        "addx -11"
        "noop"
        "noop"
        "noop"
    |]

let smallexample =
    [|
        "noop"
        "addx 3"
        "addx -5"
    |]

type Register = int

type CPU = 
    {
        X: Register
    }

    static member Default = { X = 1 }

type Cycle = 
    { 
        during: CPU
        after : CPU
    }

type Instruction = 
    | AddX of int
    | Noop

let parseInstruction (line : string) =
    match (line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)) with
    | [| "noop" |] -> Noop
    | [| "addx" ; x |] -> AddX (int x)
    | _ -> failwith "Invalid instruction"

let cyclesFromInstruction (cpu : CPU) (instruction : Instruction) =
    seq {
        match instruction with
        | AddX (x) -> 
            yield 
                {
                    during = cpu
                    after = cpu
                }
            yield 
                {
                    during = cpu
                    after = { cpu with X = cpu.X + x }
                }
        | Noop -> 
            yield 
                {
                    during = cpu
                    after = cpu
                }
    }

let cyclesFromInstructions instructions = 
    instructions 
    |> Seq.fold (fun (cycles : Cycle seq) (instruction : Instruction) ->
        let latestCPU =
            if (cycles |> Seq.isEmpty)
            then 
                CPU.Default
            else 
                cycles 
                |> Seq.last 
                |> (fun cycle -> 
                    cycle.after
                )
            
        (cyclesFromInstruction latestCPU instruction)
        |> Seq.append cycles
    ) []

    

[<Test>]
let Day10_Part1 () =    
    let instructions =
        input 
        |> Array.map parseInstruction

    let interestingCycles =
        instructions
        |> cyclesFromInstructions
        |> Seq.mapi 
            (fun i cycle -> 
                (i + 1, cycle)
            )
        |> Seq.filter 
            (fun (i, _) -> 
                ((i - 20) % 40) = 0
            )
        |> Seq.take 
            6

    let interestingSignal =
        interestingCycles
        |> Seq.sumBy 
            (fun (index, cycle) -> 
                cycle.during.X * index
            )

    Assert.AreEqual (12520 , interestingSignal)
    

[<Test>]
let Day10_Part2 () =
    let instructions =
        input 
        |> Array.map parseInstruction

    let screensize = 40

    let screenOutput =
        instructions
        |> cyclesFromInstructions
        |> Seq.indexed
        |> Seq.map (fun (index, cycle) -> 
            let x = index % screensize
            let lit = 
                (cycle.during.X >= (x - 1)) && (cycle.during.X <= (x + 1))
            if lit 
            then '#'
            else '.'
        )
        |> Seq.chunkBySize screensize
        |> Seq.map System.String.Concat 

    screenOutput
    |> Seq.iter (printfn "%s")

    Assert.AreEqual("EHPZPJGL", "EHPZPJGL")
    //Assert.Fail()