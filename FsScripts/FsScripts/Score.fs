namespace FsScripts

open UnityEngine
open UnityEngine.UI

type Score () as this =
  inherit MonoBehaviour ()

  let highScoreKey : string = "highScore"

  let mutable score : int = 0
  let mutable highScore : int = 0

  let initialize : unit -> unit = fun _ ->
    score <- 0
    highScore <- PlayerPrefs.GetInt (highScoreKey, 0)

  [<DefaultValue>]
  val mutable scoreUIText : Text
  [<DefaultValue>]
  val mutable highScoreUIText : Text

  member this.Start () : unit =
    initialize ()

  member this.Update () : unit =
    if score > highScore
      then highScore <- score
    
    this.scoreUIText.text <- string score
    this.highScoreUIText.text <- "HighScore : " ^ string highScore

  member this.AddPoint (point : int) : unit =
    score <- score + point

  member this.Save () : unit =
    PlayerPrefs.SetInt (highScoreKey, highScore)
    PlayerPrefs.Save ()
    initialize ()