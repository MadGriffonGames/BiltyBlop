using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInChest : InteractiveObject
{
    Rigidbody2D MyRigidbody;
    bool isCollectable = false;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(IgnoreCollision());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && isCollectable)
        {
            MyRigidbody.simulated = false;
            MyAnimator.SetTrigger("collected");
            GameManager.CollectedCoins++;
            SoundManager.PlaySound("coin_collect2");
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.collider, true);
        }
    }

    IEnumerator IgnoreCollision()
    {
        yield return new WaitForSeconds(0.2f);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), false);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), false);
        isCollectable = true;
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
