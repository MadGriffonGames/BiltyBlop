using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEdge : MonoBehaviour
{

    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.gameObject.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.gameObject.GetComponent<CapsuleCollider2D>(), true);

    }
    private void OnEnable()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.gameObject.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.gameObject.GetComponent<CapsuleCollider2D>(), true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spider Wall"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
        }
    }
}
