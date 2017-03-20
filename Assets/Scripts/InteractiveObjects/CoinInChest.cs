using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInChest : InteractiveObject
{
    Rigidbody2D MyRigidbody;

    public override void Start()
    {
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
            MyRigidbody.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger("collected");
            GameManager.CollectedCoins++;
            SoundManager.PlaySound("coin_collect");
        }
        if (other.transform.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.collider, true);
        }
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
