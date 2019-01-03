namespace TicTacToe


type Player =
    | X
    | O
    member this.Swap = match this with X -> O | O -> X
    member this.Name = match this with X -> "X" | O -> "O"

type GameCell =
    | Empty
    | Full of Player
    member this.CanPlay = this = Empty

type GameResult =
    | StillPlaying
    | Win of Player
    | Draw

type Pos = int * int

type Message = 
    | Play of Pos
    | Restart

type Board = Map<Pos, GameCell>

type Row = GameCell list

type Model =
    { NextUp : Player
      Board : Board
      Result : GameResult }

module Game =
    
    let positions = 
        [ for x in 0 .. 2 do
            for y in 0 .. 2 do
              yield x, y ]

    let initialBoard =
        Map.ofList [ for p in positions -> p, Empty ]

    let init () =
        { NextUp = X
          Board = initialBoard
          Result = StillPlaying }

    let anyMoreMoves x = x.Board |> Map.exists (fun _ c -> c = Empty)

    let lines = 
        [ for row in 0 .. 2 do yield [ (row, 0); (row, 1); (row, 2) ]
          for col in 0 .. 2 do yield [ (0, col); (1, col); (2, col) ]
          yield [ (0, 0); (1, 1); (2, 2) ]
          yield [ (0, 2); (1, 1); (2, 0) ] ]

    let getLine (board : Board) line =
        line |> List.map (fun x -> board.[x])

    let getLineWinner line =
        if line |> List.forall (function Full X -> true | _ -> false) then Some X
        elif line |> List.forall (function Full O -> true | _ -> false) then Some O
        else None

    let getGameResult model =
        match lines |> Seq.tryPick (getLine model.Board >> getLineWinner) with
        | Some x -> Win x
        | _ ->
            if anyMoreMoves model then StillPlaying
            else Draw

    let update msg model = 
        let newModel =
            match msg with
            | Play pos ->
                { model with Board = model.Board.Add (pos, Full model.NextUp)
                             NextUp = model.NextUp.Swap }
            | Restart ->
                { model with NextUp = X; Board = initialBoard }
        let result = getGameResult newModel
        
        { newModel with Result = result }

