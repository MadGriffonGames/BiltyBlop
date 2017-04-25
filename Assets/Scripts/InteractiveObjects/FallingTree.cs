using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTree : MonoBehaviour
{
    HingeJoint2D MyHingeJoint;
    Rigidbody2D MyRigidbody;

    private void Start()
    {
        MyHingeJoint = GetComponent<HingeJoint2D>();
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            MyRigidbody.bodyType = RigidbodyType2D.Dynamic;
            MyRigidbody.velocity = new Vector2(5,-2);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            MyRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
            MyRigidbody.velocity = new Vector2(0, -5);
            MyHingeJoint.enabled = false;
        }
    }
}
