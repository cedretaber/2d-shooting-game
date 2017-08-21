namespace FsScripts

open UnityEngine

type DestroyArea () =
  inherit MonoBehaviour ()

  member this.OnTriggerExit2D (c : Collider2D) : unit =
    Object.Destroy c.gameObject

