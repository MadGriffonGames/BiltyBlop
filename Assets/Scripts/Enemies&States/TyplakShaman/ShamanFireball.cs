using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanFireball : MonoBehaviour
{

    [SerializeField]
    public float speed;

    Rigidbody2D myRigidbody;

    SpriteRenderer mySR;

    Vector2 direction;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        mySR.enabled = true;
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
        if (myRigidbody.bodyType == RigidbodyType2D.Dynamic && transform.localScale.x < 1)
        {
            transform.localScale += new Vector3(0.2f, 0.2f, 0);
        }
    }

    public void Initialize(Vector2 dir)
    {
        this.direction = dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Player.Instance.isRewinding)
            {
                Player.Instance.InvertControls();
            }
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Player.Instance.InvertControls();
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
