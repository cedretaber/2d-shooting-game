namespace FsScripts

open System.Collections.Generic
open UnityEngine

type Enemy () as this =
  inherit Spaceship ()

  let shoot : unit -> WaitForSeconds IEnumerator = fun _ ->
    (seq {
      while this.canShot do
        let transform = this.transform in
        for i in 0..(transform.childCount-1) do
          let shotPosition = transform.GetChild i in
          this.Shot shotPosition
        yield WaitForSeconds (float32 this.shotDelay)
    }).GetEnumerator ()
  
  let move : Vector2 -> unit = fun direction ->
    (this.GetComponent<Rigidbody2D> ())
      .velocity <- direction * float32 this.speed

  [<DefaultValue>]
  val mutable hp : int

  do
    this.hp <- 1

  member this.Start () : unit =
    let vec = this.transform.up * float32 -1 in
    move (Vector2 (vec.x, vec.y))

    ()
    |> shoot
    |> this.StartCoroutine
    |> ignore

  member this.OnTriggerEnter2D (c : Collider2D) : unit =
    let layerName = LayerMask.LayerToName c.gameObject.layer in
    if layerName = "Bullet (Player)"
      then
        Object.Destroy c.gameObject
        let playerBulletTransform  = c.transform.parent in
        let bullet = playerBulletTransform.GetComponent<Bullet> () in
        this.hp <- this.hp - bullet.power
        if this.hp <= 0
          then
            this.Explosion () 
            Object.Destroy this.gameObject
          else
            Option.map
              (fun (animator : Animator) -> animator.SetTrigger "Damage")
              (this.GetAnimator ()) |> ignore