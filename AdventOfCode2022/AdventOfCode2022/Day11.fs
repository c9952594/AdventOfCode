module AdventOfCode2022_Day11

open NUnit.Framework

let input =
    [|
        "Monkey 0:"
        "  Starting items: 85, 79, 63, 72"
        "  Operation: new = old * 17"
        "  Test: divisible by 2"
        "    If true: throw to monkey 2"
        "    If false: throw to monkey 6"
        ""
        "Monkey 1:"
        "  Starting items: 53, 94, 65, 81, 93, 73, 57, 92"
        "  Operation: new = old * old"
        "  Test: divisible by 7"
        "    If true: throw to monkey 0"
        "    If false: throw to monkey 2"
        ""
        "Monkey 2:"
        "  Starting items: 62, 63"
        "  Operation: new = old + 7"
        "  Test: divisible by 13"
        "    If true: throw to monkey 7"
        "    If false: throw to monkey 6"
        ""
        "Monkey 3:"
        "  Starting items: 57, 92, 56"
        "  Operation: new = old + 4"
        "  Test: divisible by 5"
        "    If true: throw to monkey 4"
        "    If false: throw to monkey 5"
        ""
        "Monkey 4:"
        "  Starting items: 67"
        "  Operation: new = old + 5"
        "  Test: divisible by 3"
        "    If true: throw to monkey 1"
        "    If false: throw to monkey 5"
        ""
        "Monkey 5:"
        "  Starting items: 85, 56, 66, 72, 57, 99"
        "  Operation: new = old + 6"
        "  Test: divisible by 19"
        "    If true: throw to monkey 1"
        "    If false: throw to monkey 0"
        ""
        "Monkey 6:"
        "  Starting items: 86, 65, 98, 97, 69"
        "  Operation: new = old * 13"
        "  Test: divisible by 11"
        "    If true: throw to monkey 3"
        "    If false: throw to monkey 7"
        ""
        "Monkey 7:"
        "  Starting items: 87, 68, 92, 66, 91, 50, 68"
        "  Operation: new = old + 2"
        "  Test: divisible by 17"
        "    If true: throw to monkey 4"
        "    If false: throw to monkey 3"
    |]

let example =
    [|
        "Monkey 0:"
        "  Starting items: 79, 98"
        "  Operation: new = old * 19"
        "  Test: divisible by 23"
        "    If true: throw to monkey 2"
        "    If false: throw to monkey 3"
        ""
        "Monkey 1:"
        "  Starting items: 54, 65, 75, 74"
        "  Operation: new = old + 6"
        "  Test: divisible by 19"
        "    If true: throw to monkey 2"
        "    If false: throw to monkey 0"
        ""
        "Monkey 2:"
        "  Starting items: 79, 60, 97"
        "  Operation: new = old * old"
        "  Test: divisible by 13"
        "    If true: throw to monkey 1"
        "    If false: throw to monkey 3"
        ""
        "Monkey 3:"
        "  Starting items: 74"
        "  Operation: new = old + 3"
        "  Test: divisible by 17"
        "    If true: throw to monkey 0"
        "    If false: throw to monkey 1"
    |]



type Worry = bigint
type WorryAfterOperation = bigint

type Monkey =
    {
        id: int
        startingItems: Worry array
        operation: Worry -> WorryAfterOperation
        test: WorryAfterOperation -> bool
        ifTrue: int
        ifFalse: int
        numberOfItemsInspected: bigint
        divisor: bigint
    }

let parseMonkey (lines: string array) = 
    let parseMonkeyId (line : string) = 
        (line.Split(' ')[1]).Trim(':')
        |> int

    let parseStartingItems (line : string) = 
        (line.TrimStart().Split(' ')[2..])
        |> Array.map (fun x -> x.Trim(','))
        |> Array.map int
        |> Array.map bigint

    let parseOperation (line : string) : (Worry -> WorryAfterOperation) = 
        let operation = line.TrimStart().Split(' ')[4..]
        match operation with
        | [| "+" ; "old" |] -> 
            (fun x -> x + x)
        | [| "*" ; "old" |] -> 
            (fun x -> x * x)
        | [| "+" ; y |] -> 
            (fun x -> x + ((int >> bigint) y))
        | [| "*" ; y |] -> 
            (fun x -> x * ((int >> bigint) y))
        | _ -> 
            failwith (sprintf "Unknown operation: %A" operation)

    let parseDivisor (line : string) =
        (line.Trim().Split(' ')[3])
        |> int
        |> bigint

    let parseTest (divisor : bigint) =
        (fun (worry : WorryAfterOperation) -> 
            let worryIsDivisibleByDivisor = 
                worry % divisor = (bigint 0)
            worryIsDivisibleByDivisor
        )
        
    let parseMonkeyIndex (line : string) = 
        (line.Trim().Split(' ')[5])
        |> int

    let divisor = parseDivisor lines.[3]

    {
        id = parseMonkeyId lines.[0]
        startingItems = parseStartingItems lines.[1]
        operation = parseOperation lines.[2]
        divisor = divisor
        test = parseTest divisor 
        ifTrue = parseMonkeyIndex lines.[4]
        ifFalse = parseMonkeyIndex lines.[5]
        numberOfItemsInspected = 0I
        
    }


let runMonkey calmFunc (monkey : Monkey) (monkeys : Monkey array) : (Monkey array)  =
    if (Array.isEmpty monkey.startingItems)
    then monkeys
    else
        let throwsGoingToOtherMonkeys =  
            monkey.startingItems
            |> Array.map (fun startingItem ->
                let itemsNewWorryValue = 
                    startingItem
                    |> monkey.operation 
                    |> calmFunc

                let shouldThrow = 
                    itemsNewWorryValue
                    |> monkey.test

                if shouldThrow
                then (monkey.ifTrue, itemsNewWorryValue)
                else (monkey.ifFalse, itemsNewWorryValue)
            )

        let monkeysWithCurrentReset =
            monkeys
            |> Array.updateAt 
                monkey.id 
                {
                    monkey with 
                        startingItems = [||]
                        numberOfItemsInspected = 
                            monkey.numberOfItemsInspected + (monkey.startingItems.Length |> bigint)
                }

        throwsGoingToOtherMonkeys
        |> Array.fold 
            (fun monkeys (otherMonkeyIndex, item) ->
                let monkeyAtIndex = 
                    monkeys.[otherMonkeyIndex]
                    
                monkeys
                |> Array.updateAt 
                    otherMonkeyIndex 
                    {
                        monkeyAtIndex with 
                            startingItems = 
                                [| item |] 
                                |> Array.append monkeyAtIndex.startingItems
                    }
            )
            monkeysWithCurrentReset
        
let runAllMonkeys runMonkey (monkeys : Monkey array) : Monkey array =
    // The monkeys change so we need to use the id to get the monkey
    
    monkeys 
    |> Array.map (fun monkey -> monkey.id)
    |> Seq.fold (fun monkeys monkeyId -> 
        runMonkey monkeys.[monkeyId] monkeys
    ) monkeys

let rec runRounds runAllMonkeys monkeys =
    seq {
        yield monkeys
        yield! runRounds runAllMonkeys (runAllMonkeys monkeys)
    }

[<Test>]
let Day11_Part1 () = 
    let monkeys = 
        input
        |> Array.filter(fun x -> x <> "")
        |> Array.chunkBySize 6
        |> Array.map parseMonkey

    let rounds = 
        let calmDown (worry : bigint) = 
            worry / (bigint 3)

        let runMonkey = runMonkey calmDown
        let runAllMonkeys = runAllMonkeys runMonkey 

        runRounds runAllMonkeys monkeys
        |> Seq.take 21

    let finalNumberOfItemsInspected =
        rounds
        |> Seq.last
        |> Seq.map (fun monkey -> monkey.numberOfItemsInspected)
        |> Seq.sortByDescending id
        |> Seq.take 2
        |> Seq.reduce (*)

    Assert.AreEqual (118674I , finalNumberOfItemsInspected)



[<Test>]
let Day11_Part2 () = 
    let monkeys = 
        input
        |> Array.filter(fun x -> x <> "")
        |> Array.chunkBySize 6
        |> Array.map parseMonkey
    
    let rounds = 
        let calmDown  =
            let rec gcd x y = if y = 0I then abs x else gcd y (x % y)
            let lcm x y = x * y / (gcd x y)

            let leastCommonMultiple = 
                monkeys
                |> Array.map (fun monkey -> monkey.divisor)
                |> Array.reduce lcm 
                    
            (fun (worry : bigint) -> 
                worry % leastCommonMultiple
            )
            
        let runMonkey = runMonkey calmDown
        let runAllMonkeys = runAllMonkeys runMonkey 

        runRounds runAllMonkeys monkeys
        |> Seq.take 10001

    let finalNumberOfItemsInspected =
        rounds
        |> Seq.last
        |> Seq.map (fun monkey -> monkey.numberOfItemsInspected)
        |> Seq.sortByDescending id
        |> Seq.take 2
        |> Seq.reduce (*)

    Assert.AreEqual (32333418600I , finalNumberOfItemsInspected)