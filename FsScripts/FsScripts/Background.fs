namespace FsScripts

open UnityEngine

type Background () as this =
  inherit MonoBehaviour ()

  do this.speed <- 0.1

  [<DefaultValue>]
  val mutable speed : float

  member this.Update () : unit =
    let y = Mathf.Repeat (Time.time * float32 this.speed, float32 1) in
    let offset = Vector2 (float32 0.0, y) in
    (this.GetComponent<Renderer> ())
      .sharedMaterial.SetTextureOffset ("_MainTex", offset)

