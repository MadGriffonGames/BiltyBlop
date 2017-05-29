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
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
    }

    public void ResetBooster()
    {
        MyAnimator.SetBool("Boost", false);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
			SoundManager.PlaySound ("mushroom jump 2");
            MyAnimator.SetBool("Boost", true);
            Player.Instance.myRigidbody.velocity = new Vector2(Player.Instance.myRigidbody.velocity.x, 0);
            Player.Instance.myRigidbody.AddForce(new Vector2(0, force * Player.Instance.timeScalerJump));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), false);
        }
    }
}
