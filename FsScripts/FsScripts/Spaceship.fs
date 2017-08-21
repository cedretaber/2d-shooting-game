namespace FsScripts

open UnityEngine

[<RequireComponent(typeof<Rigidbody2D>); AbstractClass>]
type Spaceship () =
  inherit MonoBehaviour ()

  let mutable animator : Animator option = None

  [<DefaultValue>]
  val mutable speed : float
  [<DefaultValue>]
  val mutable shotDelay : float
  [<DefaultValue>]
  val mutable bullet : GameObject
  [<DefaultValue>]
  val mutable canShot : bool
  [<DefaultValue>]
  val mutable explosion: GameObject

  member this.Explosion () : unit =
    Object.Instantiate
      (this.explosion, this.transform.position, this.transform.rotation)
      |> ignore

  member this.Shot (origin : Transform) : unit =
    Object.Instantiate
      (this.bullet, origin.position, origin.rotation)
      |> ignore

  member this.GetAnimator () : Animator option =
    if Option.isNone animator
      then animator <- Option.ofObj (this.GetComponent<Animator> ())
    animator