namespace FsScripts

open UnityEngine

type Manager () =
  inherit MonoBehaviour ()

  let mutable title : GameObject = null

  [<DefaultValue>]
  val mutable player : GameObject

  member this.Start () : unit =
    title <- GameObject.Find "Title"
  member this.Update () : unit =
    if not (this.IsPlaying ()) && Input.GetKeyDown KeyCode.X
      then this.GameStart ()
  member this.GameStart () : unit =
    title.SetActive false
    Object.Instantiate (this.player, this.player.transform.position, this.player.transform.rotation)
    ()
  member this.GameOver () : unit =
    title.SetActive true
  member this.IsPlaying () : bool =
    not title.activeSelf