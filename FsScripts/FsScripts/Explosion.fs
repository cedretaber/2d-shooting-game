namespace FsScripts

open UnityEngine

type Explosion () =
  inherit MonoBehaviour ()

  member this.OnAnimationFinish () : unit =
    Object.Destroy this.gameObject

