using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeContactCollider : MonoBehaviour {

    [SerializeField]
    Spider spider;

    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spider.shoopDaWhoopCollider.GetComponent<Collider2D>(), true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {
            spider.ChangeDirection();
        }
        if (collision.gameObject.CompareTag("Right Edge"))
        {
            spider.ChangeState(new SpiderJumpState());
            spider.ChangeDirection();
        }
    }
}
