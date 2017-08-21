namespace FsScripts

open System.Collections.Generic
open UnityEngine

type Emmiter () as this =
  inherit MonoBehaviour ()

  let runWaves : unit -> WaitForEndOfFrame IEnumerator = fun _ ->
    (seq {
      while not (Array.isEmpty this.waves) do
        let manager = Object.FindObjectOfType<Manager>() in
        while not (manager.IsPlaying ()) do
          yield WaitForEndOfFrame ()
        for wave' in this.waves do
          let wave =
            Object.Instantiate
              (wave', this.transform.position, Quaternion.identity)
                :?> GameObject in
          wave.transform.parent <- this.transform
          while wave.transform.childCount <> 0 do
            yield WaitForEndOfFrame ()
          Object.Destroy wave
    }).GetEnumerator ()

  [<DefaultValue>]
  val mutable waves : GameObject array

  member this.Start () : unit =
    this.StartCoroutine (runWaves ()) |> ignore