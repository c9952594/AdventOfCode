module AdventOfCode2022_Day12

open NUnit.Framework

let input =
    [|
        "abcccaaaaacccacccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaaaacaccccccaaacccccccccccccccccccccccccccccccccccaaaaaaaaccccccccccccccccccccccccccccccccaaaaaa"
        "abcccaaaaacccaaacaaacccccccccccccccccaaccccccacccaacccccccccccaacccccaaaaaaaaaaaccaaaaaaccccccccccccccccccccccccccccccccccaaaaaccccccccccccccccccccccccccccccccccaaaaaa"
        "abccccaaaaaccaaaaaaaccccccccccccccaaaacccccccaacaaacccccccccaaaaaacccaaaaaaaaaaaccaaaaaacccccccccccccccccccccccccccccccccccaaaaaccccccccccccccaaacccccccccccccccccaaaaa"
        "abccccaacccccaaaaaacccccccccccccccaaaaaacccccaaaaaccccccccccaaaaaacaaaaaaaaaaaaaccaaaaaacccccccccccccccccccccccccccccccccccaacaaccccccccccccccaaaaccccccccccccccccccaaa"
        "abccccccccccaaaaaaaacccccccccccaaccaaaaaccccccaaaaaacccccccccaaaaacaaaaaaaaccccccccaaaaacccccccccccccccccccccccccccccccccccaacccccccccccccccccaaaaccaaacccccccccccccaac"
        "abaaaaaccccaaaaaaaaaaccccccaaccaacaaaaacccccaaaaaaaaaaacccccaaaaacaaaacaaaaacccccccaacaacccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaaaaaaacccccccccccaaac"
        "abaaaaaccccaaaaaaaaaacaacccaaaaaacaccaacccccaaaaaaaaaaacccccaaaaaccccccaaaaaccccccccccaacccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaakkkllccccccccccccccc"
        "abaaaaacccccccaaacaaaaaaccccaaaaaaaccccccaaacccaacccaaaaaaacccccccccccccaaaaacccccccccaaaaaaccccccccccccccccccccccccccaaccccccccccccccccccccccackkkkkklllccccaaaccccccc"
        "abaaaaacccccccaaacaaaaaaacccaaaaaaaccccccaaaacaaacaaaaaaaacccccccccccccaaaaaacccccccccaaaaaaccaacaacccccccccccccccaaaaaacccccccccccccccccccaaakkkkkkkkllllcccaaacaccccc"
        "abaaaaaccccccccaacaaaaaaaacaaaaaaccccccccaaaaaaacaaaaaaaaacccccccccccccaaaacccccccccaaaaaaacccaaaaacccccccccccccccaaaaaaccccccccccccccccjjjjjkkkkkkpppplllcccaaaaaacccc"
        "abaaaccccccccccccccaaaaaaacaaaaaacccccccccaaaaaaccaaaaaaaccccccccccccccccaaaccccccccaaaaaaaccccaaaaacccccccccccccccaaaaaaaccccaaccccccjjjjjjjkkkkppppppplllcccaaaaacccc"
        "abccccccccccccccccaaaaaacccccccaaccccccaaaaaaaacccccaaaaaaccccccccccccccccccccccccccccaaaaaaccaaaaaacccccccccccccccaaaaaaaaaacaacccccjjjjjjjjjkooppppppplllcccaaacccccc"
        "abccccccccccccccccaaaaaacccccccccccccccaaaaaaaaacccaaacaaacccccccccccccccccccccccaaaccaaccaaccaaaaccccccccccccccccaaaaaaaccaaaaaccccjjjjooooooooopuuuupppllccccaaaccccc"
        "abccccccccccccccccccccaaccccccccccccccccaaaaaaaacccaaaccaacccccccccccccccccccccccaaaaaaacccccccaaaccccccccccccccccaaaaaaccccaaaaaaccjjjoooooooooouuuuuupplllccccaaccccc"
        "abccaaaaccccaaacccccccccccccccccccccccccccaaaaaaaccaaccccccccccccaacccccccccccccccaaaaacccaaccaaaccccccccccccccccccccaaacccaaaaaaaccjjjoootuuuuuuuuuuuuppllllccccaccccc"
        "abccaaaaaccaaaacccccccccccccccccccccccccccaacccacccccccccccccccacaaaacccccccccccaaaaaaacccaaaaaaacccccccccccccccccccccccccaaaaaacccciijnoottuuuuuuxxyuvpqqlmmcccccccccc"
        "abcaaaaaaccaaaacccccccaaaaccccccccccacccccaaccccaaaccccccccccccaaaaaacccccccccccaaaaaaaaccaaaaaacccccccccccccccccaacccccccaacaaacccciiinntttxxxxuxxyyvvqqqqmmmmddddcccc"
        "abcaaaaaacccaaacccccccaaaaccccaaaaaaaaccaaaaccccaacaacccccccccccaaaaccccccccccccaaaaaaaacccaaaaaaaacccccccccccccaaaaccccccccccaacccciiinntttxxxxxxxyyvvqqqqqmmmmdddcccc"
        "abcaaaaaacccccccccccccaaaacccccaaaaaacccaaaaaaaaaaaaacccccccccccaaaaccccccccccccccaaacacccaaaaaaaaacccccccccccccaaaacccccccccccccccciiinnnttxxxxxxxyyvvvvqqqqmmmdddcccc"
        "abcccaaccccccccccccccccaaccccccaaaaaacccaaaaaaaaaaaaaaccccccccccaacaccccccccccccccaaaccccaaaaaaaaaacccccccccccccaaaacccccccccccccccciiinnntttxxxxxyyyyyvvvqqqqmmmdddccc"
        "SbccccccccccccccccccccccccccccaaaaaaaaccaaaccaaaaaaaaacccccccccccccccccccccccccccccccccccaaaaaaacccccccccaacccccccccccccccccccccccccciiinntttxxxxEzyyyyyvvvqqqmmmdddccc"
        "abcccccccccccccccccccccccccccaaaaaaaaaacccccccaaaaaacccccccccccccaaacccccaacaacccccccccccccccaaaaaaccccccaacaaacccccccccccccccccccccciiinntttxxxyyyyyyyvvvvqqqmmmdddccc"
        "abcccccccccccccccccccccccccccaaaaaaaaaaccccccaaaaaaaaccccccccccccaaaccccccaaaacccccccccccccccaaaaaaccccccaaaaacccccccccccccccccccccciiinnnttxxyyyyyyyvvvvvqqqqmmmdddccc"
        "abcccccccccccccccccccccccccccacacaaacccccccccaaaaaaacccccccccccaaaaaaaacccaaaaacccccccccccccccaaaaaaaacaaaaaaccccccccccccccccccccccciiinntttxxwyyyyywwvvrrrqqmmmdddcccc"
        "abaccccccccccccccccccccccccccccccaaacccccccccaaacaaaaacccccccccaaaaaaaaccaaaaaacccccccccccccccaaaaaaaacaaaaaaacccccccccccccccccccccchhnnnttwwwwwwwyyywvrrrrnnnnmdddcccc"
        "abaccccccccccccccccccccccccccccccaaccccccccccccccaaaaaacccccccccaaaaaccccaaaacaccccccccccccccccaaaaacccccaaaaaaccccccaaaccccccaaaccchhnmmttswwwwwwywwwrrrrnnnnneeeccccc"
        "abaccccccccccccccccccccccccccccccccccccccccccccccaaaaaacccccaaccaaaaaacccccaaccccccccccccccccccaaaaaaccccaaccaaccccaaaaaacccccaaacahhhmmmsssssssswwwwwrrrnnnneeeecccccc"
        "abaaaccccccccccccccccccccccccccccaaaccccccccccccccaaaaaccccaaaccaaaaaacccccccccccccccccccccccccaaaaaaccccaaccccccccaaaaaacccaaaaaaahhhmmmmsssssssswwwwrrnnnneeeeacccccc"
        "abaaaccccccccccccccccccccccccccccaaaaaaccccccccccaaaaacaaaaaaaccaaaccaccccccccaaaaaccccccccccccaaaacacccccccccccccccaaaaacccaaaaaaahhhhmmmmssssssswwwwrrnnneeeeaacccccc"
        "abaaacccccccccccccccccccccccccccaaaaaaaccccccccccaaaaacaaaaaaaaaacccaaaaacccccaaaaacccccccccacaaaaaccacccccccccccccaaaaacccccaaaaaachhhmmmmmmmmmsssrrrrrnnneeeaaaaacccc"
        "abaccccccccccccccaaaaccccccccccaaaaaaaacccccccccccccccccaaaaaaaaacccaaaaaccccaaaaaacccccccccaaaaaaaaaacccccccccccccaaaaaccccccaaaaachhhhmmmmmmmooossrrronneeeaaaaaacccc"
        "abaccccccccccccccaaaaccccccccccaaaaaaacccccccccccccccccccaaaaaaaccccaaaaaacccaaaaaaccccaaaccaaaaaaaaaacccccccccccaaccccccccccaaaaaacchhhhhggggooooorrroonnfeeaaaaaccccc"
        "abcccccccccccccccaaaaccccccccccccaaaaaacccccccccccccccccaaaaaaccccccaaaaaacccaaaaaaccccaaaaaacaaaaaacccccccccaaccaacccccccccccaacccccchhhhggggggoooooooooffeaaaaacccccc"
        "abccccccccccccccccaacccccccccccccaaaaaacccccccaaccacccccaaaaaaacccccaaaaaaccccaaaccccccaaaaaacaaaaaacccccccccaaaaacccccccccccccccccccccccgggggggggooooooffffaaaaaaccccc"
        "abccccccccccccccccccccccccccaaaccaacccccccccccaaaaacccccaaccaaacccccccaaacccccccccccccaaaaaaacaaaaaacccccccccaaaaaaaaccccccccccccccccccccccaaaggggfooooffffccccaacccccc"
        "abaaccccccccccccccccccccccccaaacaccccccccccccaaaaacccccccccccaacccccccaaaacccaacccccccaaaaaaaaaaaaaaaccccccccccaaaaacccccccccccccaaaccccccccccccggfffffffffcccccccccccc"
        "abaaccccccccccccccccccccccaacaaaaacccccccccccaaaaaacccccccccccccccccccaaaacaaaacccccccaaaaaaaaaccccaccccccccccaaaaaccccccccccccccaaaccccccccccccagfffffffccccccccccccca"
        "abaacccccccaacccccccccccccaaaaaaaaccccccaacccccaaaacccccccccccccccccccaaaaaaaaacccccccccaaacaacaaacccccccccccaaacaaccccaaccaaccaaaaaaaaccccccccaaaccffffcccccccccccccaa"
        "abaaaaaaaccaaccccccccccccccaaaaacccccccaaaacccaaccccccccccccccccccacaaaaaaaaaaccccccccccaaacaaaaaacccccccccccccccaaccccaaaaaaccaaaaaaaacccccccccaacccccccccccccccaaacaa"
        "abaaaaaaaaaaccccccccccccccccaaaaaccccccaaaacccccccccccccccccccccccaaaaaaaaaaaaccccccccccccccaaaaaacccccccccccccccccccccaaaaaacccaaaaaacccccccccaaacccccccccccccccaaaaaa"
        "abaaaacaaaaaaaacccccccccccccaacaaccccccaaaaccccccccccccccccccccccccaaaaaaaaaaacccccccccccccaaaaaaaacccccccccccccccccccaaaaaaaaccaaaaaaccccccccccccccccccccccccccccaaaaa"
    |]

let example =
    [|
        "Sabqponm"
        "abcryxxl"
        "accszExk"
        "acctuvwj"
        "abdefghi"
    |]


VerifyTests.VerifierSettings.AddExtraSettings
    (fun settings -> 
        settings.NullValueHandling <- Argon.NullValueHandling.Include
        settings.DefaultValueHandling <- Argon.DefaultValueHandling.Include
    )

type Position = int
type Distance = int
type Elevation = char
type XDimension = int
type YDimension = int

type Point =
    {
        X: XDimension
        Y: YDimension
    }

type FoundSearch = 
    {
        Position: Position
        Distance: Distance
        Elevation: Elevation
    }

type SearchPosition =
    | NotSearched of Elevation
    | ToSearch of FoundSearch
    | ExitFound of Distance
    | Searched
    | EdgeFound

let findExits 
    (rows : int)
    (columns : int)
    =
    let pointOfPosition (position : Position) : Point =
        let x = position % columns
        let y = position / columns

        { X = x; Y = y }

    let positionOfPoint (point : Point) : Position =
        point.Y * columns + point.X

    let iterate 
        (map : SearchPosition array)
        =
        let positionsOfCurrentToSearch = 
            map
            |> Array.indexed
            |> Array.choose 
                (
                function
                | index, ToSearch _ -> 
                    Some index
                | _ -> 
                    None
                )
           
        let newMap =
            map
            |> Array.mapi (fun currentPosition current ->
                match current with
                | NotSearched currentElevation -> 
                    let currentPoint = pointOfPosition currentPosition

                    let north = { X = currentPoint.X; Y = currentPoint.Y - 1 }
                    let south = { X = currentPoint.X; Y = currentPoint.Y + 1 }
                    let east = { X = currentPoint.X + 1; Y = currentPoint.Y }
                    let west = { X = currentPoint.X - 1; Y = currentPoint.Y }

                    [ north; south; east; west ]
                    |> List.filter (fun neighbour ->
                        neighbour.X >= 0
                        && neighbour.X < columns
                        && neighbour.Y >= 0
                        && neighbour.Y < rows
                    )
                    |> List.map 
                        positionOfPoint
                    |> List.choose (fun neighbourPosition ->
                        map.[neighbourPosition]
                        |> 
                            function
                            | ToSearch x -> 
                                Some x
                            | _ -> 
                                None
                    )
                    |> List.map (fun neighbour -> 
                        if ((int)neighbour.Elevation) = ((int)currentElevation) + 1 then 
                            ExitFound (neighbour.Distance + 1)
                        elif (neighbour.Elevation = 'E') && (currentElevation = 'z') then 
                            ExitFound (neighbour.Distance + 1)
                        elif (neighbour.Elevation = 'a') && (currentElevation = 'S') then 
                            ExitFound (neighbour.Distance + 1)
                        elif neighbour.Elevation = currentElevation then
                            ToSearch
                                {
                                    Position = currentPosition
                                    Distance = neighbour.Distance + 1
                                    Elevation = neighbour.Elevation
                                }
                        else
                            EdgeFound
                    )
                    |> List.sortBy (fun item ->
                        match item with
                        | ExitFound _ -> 
                            System.Int32.MinValue
                        | ToSearch search -> 
                            search.Distance
                        | _ -> 
                            System.Int32.MaxValue
                    )
                    |> List.tryHead
                    |> Option.defaultValue current
                | _ -> 
                    current
            )

        positionsOfCurrentToSearch
        |> Array.fold (fun (newMap : SearchPosition array) (index : int) ->
            newMap
            |> Array.updateAt
                index
                Searched
        ) newMap

    (fun (elevations : Elevation array) (start : Position) -> 
        let searchElevations =
            elevations
            |> Array.mapi (fun index elevation -> 
                if index = start then
                    ToSearch 
                        {
                            Position = index
                            Distance = 0
                            Elevation = elevation
                        }
                else
                    NotSearched elevation
            )

        let iterateUntilNoMoreSearchesAvailable = 
            searchElevations
            |> Array.unfold (fun map ->
                map
                |> Array.exists (fun item ->
                    match item with
                    | ToSearch _ -> true
                    | _ -> false
                )
                |>  function
                    | true -> 
                        let newMap = iterate map
                        Some (newMap, newMap)
                    | false -> 
                        None                
            ) 
            |> Array.last

        let exits = 
            iterateUntilNoMoreSearchesAvailable
            |> Array.indexed
            |> Array.choose (fun (index, item) ->
                match item with
                | ExitFound distance -> 
                    Some ((pointOfPosition index), distance)
                | _ -> 
                    None
            )
            |> Array.sortBy snd

        exits
    )

[<Test>]
let Day12_Part1 () = 
    let elevationMap = 
        example

    let (rows, columns) =
        elevationMap.Length
        ,
        elevationMap.[0].Length

    let pointOfPosition (position : Position) : Point =
        let x = position % columns
        let y = position / columns

        { X = x; Y = y }

    let positionOfPoint (point : Point) : Position =
        point.Y * columns + point.X

    let elevations =
        elevationMap
        |> Array.map Seq.toArray
        |> Array.concat

    let start = 
        elevations
        |> Array.findIndex (fun elevation -> elevation = 'E')
        
    let exits = 
        findExits rows columns elevations start


    let stack = 
        [[start, 0]]
    
    let rec iterate (elevations : char array) (currentStack : (Position * Distance) list list) =
        let currentPath = currentStack |> List.head
        let current = currentPath |> List.head
        let currentPosition = fst current
        let currentDistance = snd current
        
        let current =
            elevations.[currentPosition]

        if (current = 'S')
        then [currentPath]
        else 
            let exits = 
                findExits rows columns elevations currentPosition

            let newStack =
                exits
                |> Array.sortByDescending snd
                |> Array.map 
                    (fun exit ->
                        (positionOfPoint (fst exit), (snd exit)) :: currentPath
                    )
                |> Array.fold 
                    (fun currentStack x -> 
                        x::currentStack
                    ) currentStack

            iterate elevations newStack

    let pathTaken = 
        iterate elevations stack
        |> List.head

    let distanceTravelled = 
        pathTaken
        |> List.sumBy snd

    let testOutput =
        pathTaken
        |> List.map (fun (position, distance) ->
            let current =
                elevations.[position]

            current
            ,
            pointOfPosition position
            ,
            distance
        )

    VerifyNUnit.Verifier.Verify(distanceTravelled).ToTask() 
    |> Async.AwaitTask 
    |> Async.RunSynchronously
    |> ignore

[<Test>]
let Day12_Part2 () = 
    Assert.Pass()




// let adjustedToRemoveInvalidNeighbours input =
//     input
//     |> Map.map(fun _ current ->
//         let neighbours =
//             current.Neighbours
//             |> List.filter (fun neighbourPosition ->
//                 input
//                 |> Map.tryFind neighbourPosition
//                 |> Option.filter (fun neighbour ->
//                     (neighbour.Elevation = current.Elevation) 
//                     ||
//                     match current.Elevation with
//                     | 'a' ->
//                         neighbour.Elevation = 'S'
//                     | 'E' ->
//                         neighbour.Elevation = 'z'
//                     | _ ->
//                         (int)neighbour.Elevation = ((int)current.Elevation) - 1
//                 )
//                 |> Option.isSome
//             )

//         { current with Neighbours = neighbours }
//     )


// let rec walkBack 
//     (currentMap : Map<Position, ElevationWithPosition>) 
//     (currentStack : List<ElevationWithPosition>) 
//     : List<ElevationWithPosition> 
//     =
//     let current = 
//         currentStack.Head

//     let currentMap =
//         currentMap
//         |> Map.remove current.Position

//     if (current.Elevation = 'S')
//     then 
//         currentStack
//     else 
//         match current.Neighbours with
//         | [] ->
//             // remove current from currentmap
            
//             walkBack currentMap currentStack.Tail
//         | neighbourPosition :: neighbours ->
//             let neighbour =
//                 currentMap
//                 |> Map.tryFind neighbourPosition

//             match neighbour with
//             | None ->
//                 // remove neighbour from current.neighbours
                
//                 let adjustedCurrent =
//                     { current with Neighbours = neighbours }

//                 walkBack currentMap (adjustedCurrent :: currentStack.Tail)
//             | Some neighbour ->
//                 let currentRemovedFromNeighboursNeighbours =
//                     neighbour.Neighbours
//                     |> List.filter (fun neighbour ->
//                         neighbour <> current.Position
//                     )

//                 let adjustedNeighbour =
//                     { neighbour with Neighbours = currentRemovedFromNeighboursNeighbours }

//                 let adjustedStack =
//                     { current with Neighbours = neighbours } :: currentStack.Tail

//                 let newStack =
//                     adjustedNeighbour:: adjustedStack

//                 walkBack currentMap newStack



    // let walk = 
    //     walkBack input [start]
    //     |> List.map (fun position ->
    //         position.Elevation
    //         ,
    //         position.Position
    //     )

    // let steps = 
    //     walk
    //     |> List.length
    //     |> (fun steps ->
    //         steps - 1
    //     )


   
    
    // let regions = 
    //     adjustedToRemoveInvalidNeighbours
    //     |> extractAllRegions

    // let positionsPerElevation =
    //     regions
    //     |> List.collect 
    //         (fun positionsInRegion ->
    //             positionsInRegion
    //             |> List.groupBy 
    //                 (fun position -> 
    //                     position.Elevation
    //                 )
    //         )
    //     |> List.groupBy (fun (elevation, _) -> 
    //         elevation
    //     )
    //     |> List.map (fun (elevation, regions) -> 
    //         elevation
    //         , 
    //         regions 
    //         |> List.map 
    //             (fun (elevation, positions) -> 
    //                 let neighbours = 
    //                     positions
    //                     |> List.collect (fun position -> position.Neighbours)
    //                     |> List.distinct
    //                     |> List.filter (fun neighbour ->
    //                         input
    //                         |> Map.tryFind neighbour
    //                         |> Option.bind (fun neighbourPosition ->
    //                             match elevation with
    //                             | 'a' ->
    //                                 if (neighbourPosition.Elevation = 'S')
    //                                 then Some neighbourPosition
    //                                 else None
    //                             | 'E' ->
    //                                 if (neighbourPosition.Elevation = 'z')
    //                                 then Some neighbourPosition
    //                                 else None
    //                             | _ ->
    //                                 let current = (int)elevation
    //                                 let neighbour = (int)neighbourPosition.Elevation

    //                                 if neighbour = current - 1 then
    //                                     Some neighbourPosition
    //                                 else
    //                                     None
    //                         )
    //                         |> Option.isSome
    //                     )

    //                 {|
    //                     Positions = 
    //                         positions
    //                         |> List.map 
    //                             (fun position -> 
    //                                 position.Position
    //                             )
    //                     Neighbours = neighbours
    //                 |}
    //             )
    //     )




// let extractElevationConditionally 
//     (condition : ElevationWithPosition -> bool)
//     (position : Position) 
//     (map : Map<Position, ElevationWithPosition>) 
//     = 
//     map 
//     |> Map.tryFind position
//     |> Option.bind 
//         (fun elevation ->
//             if condition elevation then
//                 let newMap = 
//                     map 
//                     |> Map.remove position
//                 Some (elevation, newMap)
//             else
//                 None
//         )
    
// let rec extractSameElevations
//     (condition : ElevationWithPosition -> bool)
//     (position : Position) 
//     (map : Map<Position, ElevationWithPosition>)
//     =
//     let self' =
//         map
//         |> extractElevationConditionally condition position

//     match self' with
//     | None ->
//         [], map
//     | Some (self, mapWithSelfRemoved) ->
//         self.Neighbours
//         |> List.fold (fun (alreadyFound, mapWithFoundRemoved) positionOfNeighbour ->
//             let found', map' = 
//                 extractSameElevations 
//                     (fun neighbour -> neighbour.Elevation = self.Elevation) 
//                     positionOfNeighbour
//                     mapWithFoundRemoved
//             ((alreadyFound @ found') , map')
//         ) ([self], mapWithSelfRemoved)
        
// let rec extractAllRegions 
//     (map : Map<Position, ElevationWithPosition>) 
//     = 
//     let firstPosition = 
//         map
//         |> Map.tryFindKey (fun _ _ -> true)

//     match firstPosition with
//     | None ->
//         []
//     | Some position ->
//         let region, mapWithRegionRemoved = 
//             extractSameElevations
//                 (fun _ -> true) 
//                 position 
//                 map

//         region :: (extractAllRegions mapWithRegionRemoved)










// type Position = { X: int; Y: int }

// type Boundary =
//     {
//         fromPosition : Position
//         toPosition : Position
//     }

// type Region = 
//     {
//         id : char
//         entryPoints : Boundary list
//         exitPoints : Boundary list
//     }

// // Find the region that contains the same char as starting position
// let findRegion (elevationMap : char array array) (startingPosition : Position) : (Position list) =
    
//     let rec findRegion' (currentPosition : Position) (visitedPositions : Position list) : (Position list) =
//         let currentChar = elevationMap.[currentPosition.Y].[currentPosition.X]
        
//         let nextPositions =
//             [
//                 { X = currentPosition.X - 1; Y = currentPosition.Y }
//                 { X = currentPosition.X + 1; Y = currentPosition.Y }
//                 { X = currentPosition.X; Y = currentPosition.Y - 1 }
//                 { X = currentPosition.X; Y = currentPosition.Y + 1 }
//             ]

//         let positionsInMap =
//             nextPositions
//             |> List.filter 
//                 (fun p -> 
//                     p.X >= 0 && p.X < elevationMap.[0].Length
//                     && p.Y >= 0 && p.Y < elevationMap.Length
//                 )

//         let positionsWithSameChar =
//             positionsInMap
//             |> List.filter 
//                 (fun p -> 
//                     elevationMap.[p.Y].[p.X] = currentChar
//                 )

//         let positionsNotAlreadyFound = 
//             positionsWithSameChar
//             |> List.filter 
//                 (fun p -> 
//                     not (List.contains p visitedPositions)
//                 )
            
//         let nextVisitedPositions = 
//             visitedPositions 
//             |> List.append positionsNotAlreadyFound

//         let allPosition =
//             positionsNotAlreadyFound
//             |> List.map 
//                 (fun p -> 
//                     findRegion' p nextVisitedPositions
//                 )

//         match allPosition with
//         | [] -> visitedPositions
//         | _ ->
//             let reduced =
//                 allPosition
//                 |> List.reduce 
//                     (fun a b -> 
//                         a 
//                         |> List.append b
//                     )

//             reduced

//     findRegion' startingPosition [startingPosition]







// type Path = Position list

// type VisitedStatus =
//     | NotVisited of char
//     | Visited of char

// let findCharPosition elevationMap findChar =
//     elevationMap
//     |> Array.indexed
//     |> Array.pick (fun (y , row) -> 
//         row
//         |> Array.tryFindIndex 
//             (fun c -> 
//                 c = findChar
//             )
//         |> Option.bind 
//             (fun x -> 
//                 Some { X = x; Y = y }
//             )
//     )

// let nextValidChars currentChar =
//     match currentChar with
//     | 'S' -> ['a']
//     | 'z' -> ['z' ; 'E']
//     | x -> [x ; char (int x + 1)]

// let nextPositions 
//     (elevationMap : VisitedStatus array array) 
//     (position : Position) 
//     =
//     let newPosition (moveFunc : Position -> Position) = 
//         (
//             let nextPosition = 
//                 moveFunc position

//             let validIfPositionIsOnTheMap =
//                 elevationMap
//                 |> Array.tryItem nextPosition.Y
//                 |> Option.bind (Array.tryItem nextPosition.X)
//                 |> Option.bind (fun _ -> Some nextPosition)

//             let validIfNotVisited =
//                 validIfPositionIsOnTheMap
//                 |> Option.bind (fun nextPosition ->
//                     match elevationMap.[nextPosition.Y].[nextPosition.X]  with
//                     | NotVisited _ -> Some nextPosition
//                     | Visited _ -> None
//                 )
            
//             let validIfTheCharIsInRange =
//                 validIfNotVisited
//                 |> Option.bind (fun nextPosition ->
//                     let currentChar =
//                         elevationMap.[position.Y].[position.X]
//                         |> function
//                             | NotVisited c -> c
//                             | Visited c -> c

//                     let nextChar =
//                         elevationMap.[nextPosition.Y].[nextPosition.X]
//                         |> function
//                             | NotVisited c -> c
//                             | Visited c -> c

//                     if (nextValidChars currentChar) |> List.contains nextChar then
//                         Some nextPosition
//                     else
//                         None
//                 )
                
//             validIfTheCharIsInRange
//         )

//     [
//         newPosition (fun p -> { p with Y = p.Y - 1 }) // Up
//         newPosition (fun p -> { p with Y = p.Y + 1 }) // Down
//         newPosition (fun p -> { p with X = p.X - 1 }) // Left
//         newPosition (fun p -> { p with X = p.X + 1 }) // Right
//     ]
//     |> List.choose 
//         (Option.bind 
//             (fun position ->
//                 Some position
//             )
//         )

// let nextPaths 
//     (elevationMap : VisitedStatus array array) 
//     (path : Path)
//     =
//     let currentPosition = 
//         path 
//         |> List.head  

//     let nextPositions =
//         nextPositions 
//             elevationMap 
//             currentPosition

//     let newElevationMap =
//         nextPositions
//         |> List.fold 
//             (fun (elevationMap : VisitedStatus array array) (position : Position) -> 
//                 let charAtPosition =
//                     elevationMap.[position.Y].[position.X]
//                     |> function
//                         | NotVisited c -> c
//                         | Visited c -> c

//                 let row = 
//                     elevationMap.[position.Y]

//                 let newRow =
//                     row
//                     |> Array.updateAt position.X (Visited charAtPosition)

//                 elevationMap
//                 |> Array.updateAt position.Y newRow
//             ) elevationMap

//     newElevationMap
//     ,
//     nextPositions
//     |> List.map (fun position -> 
//         position :: path
//     )
    

// // let nextPaths (elevationMap : VisitedStatus array array)  (paths : Path list) = 
// //     let currentPosition = 
// //         path 
// //         |> List.head 
// //
// //     let currrentChar =
// //         elevationMap.[currentPosition.Y].[currentPosition.X]
// //         |> function
// //             | NotVisited c -> c
// //             | Visited c -> c
// //
// //     let nextPositions =
// //         nextPositions elevationMap currentPosition
// //    
// //     nextPositions
// //     |> List.filter (fun ((position , nextChar) as nextPosition) ->
// //         path |> List.contains nextPosition |> not &&
// //         currentChar = nextChar || 
// //         ((int currentChar) + 1) = (int nextChar) ||
// //         (currentChar = 'S' && nextChar = 'a') || 
// //         (nextChar = 'E')
// //     )
// //     |> List.map (fun nextPosition ->
// //         nextPosition :: path
// //     )
// //
// // let walkPath 
// //     (elevationMap : VisitedStatus array array)
// //     (paths : (Position * char) list list)
// //     =
// //  
// //
// //     paths
// //     |> List.collect nextPaths
// //
// // let rec walkPaths 
// //     (elevationMap : char array array)
// //     (paths : (Position * char) list list)
// //     =
// //     seq {
// //         let nextPaths =
// //             walkPath
// //                 elevationMap
// //                 paths
// //         yield nextPaths
// //         yield! walkPaths elevationMap nextPaths
// //     }