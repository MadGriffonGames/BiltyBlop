using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneColumn : MonoBehaviour
{
    [SerializeField]
    Transform stopPoint;
    [SerializeField]
    GameObject spider;

	void FixedUpdate ()
    {
        if (transform.position.y <= stopPoint.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 4, -1), 0.1f);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spider"))
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.collider, true);
        }
    }
}
