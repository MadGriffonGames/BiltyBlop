using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : InteractiveObject
{
    [SerializeField]
    float force = 1000;

    public override void Start()
    {
        base.Start();
    }

    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("boost");
            player.MyRigidbody.velocity = new Vector2(player.MyRigidbody.velocity.x, 0);
            player.MyRigidbody.AddForce(new Vector2(0, force));
            animator.SetTrigger("boost");
        }
    }
}
