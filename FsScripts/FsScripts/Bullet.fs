namespace FsScripts

open UnityEngine

type Bullet () as this =
  inherit MonoBehaviour ()

  do
    this.speed <- 10
    this.lifeTime <- 5.0

  [<DefaultValue>]
  val mutable speed : int
  [<DefaultValue>]
  val mutable lifeTime : float
  [<DefaultValue>]
  val mutable power : int

  do
    this.power <- 1

  member this.Start () : unit =
    let
      vec =
        this.transform.up.normalized * float32 this.speed in
    this.GetComponent<Rigidbody2D>()
      .velocity <- new Vector2 (vec.x, vec.y)
    Object.Destroy (this.gameObject, float32 this.lifeTime)