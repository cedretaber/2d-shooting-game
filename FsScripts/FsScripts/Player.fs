namespace FsScripts

open System.Collections.Generic
open UnityEngine

type Player () as this =
  inherit Spaceship ()

  let shoot : unit -> WaitForSeconds IEnumerator = fun _ ->
    (seq {
      while true do
        this.Shot this.transform
        (this.GetComponent<AudioSource> ()).Play ()
        yield WaitForSeconds (float32 this.shotDelay)
    }).GetEnumerator ()

  let move : Vector2 -> unit = fun direction ->
    let f0 = float32 0 in
    let f1 = float32 1 in
    let min = Camera.main.ViewportToWorldPoint (Vector3 (f0, f0, f0)) in
    let max = Camera.main.ViewportToWorldPoint (Vector3 (f1, f1, f0)) in
    let direction' = Vector3 (direction.x, direction.y, (float32 0)) in
    let pos = this.transform.position + direction' * (float32 this.speed) * Time.deltaTime in
    let pos' = Vector3 (Mathf.Clamp (pos.x, min.x, max.x), Mathf.Clamp (pos.y, min.y, max.y), f0) in
    this.transform.position <- pos'
    ()

  member this.Start () : unit =
    ()
    |> shoot
    |> this.StartCoroutine
    |> ignore
        
  member this.Update () : unit =
    let x = Input.GetAxisRaw "Horizontal" in
    let y = Input.GetAxisRaw "Vertical" in
    let direction = (new Vector2 (x, y)).normalized in
    move (direction)
    
  member this.OnTriggerEnter2D (c : Collider2D) : unit =
    let layerName = LayerMask.LayerToName c.gameObject.layer in
    if layerName = "Bullet (Enemy)"
      then Object.Destroy c.gameObject
    if layerName = "Bullet (Enemy)" || layerName = "Enemy" then
        (Object.FindObjectOfType<Manager> ()).GameOver ()
        this.Explosion ()
        Object.Destroy this.gameObject