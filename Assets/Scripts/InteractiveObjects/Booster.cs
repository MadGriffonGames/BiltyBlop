using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : InteractiveObject
{
    [SerializeField]
    float force;

    public override void Start()
    {
        base.Start();
    }

    public void ResetBooster()
    {
        MyAnimator.SetBool("Boost", false);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
			SoundManager.PlaySound ("mushroom_boing");
            MyAnimator.SetBool("Boost", true);
            Player.Instance.MyRigidbody.velocity = new Vector2(Player.Instance.MyRigidbody.velocity.x, 0);
            Player.Instance.MyRigidbody.AddForce(new Vector2(0, force * Player.Instance.timeScalerJump));
        }
    }
}
