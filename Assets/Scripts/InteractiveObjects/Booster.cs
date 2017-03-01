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
        if (force == 0)
        {
            force = 1000;
        }
    }

    void Update()
    {

    }

    public void ResetBooster()
    {
        animator.SetBool("Boost", false);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
			SoundManager.PlaySound ("mushroom_boing");
            animator.SetBool("Boost", true);
            player.MyRigidbody.velocity = new Vector2(player.MyRigidbody.velocity.x, 0);
            player.MyRigidbody.AddForce(new Vector2(0, force));
        }
    }
}
