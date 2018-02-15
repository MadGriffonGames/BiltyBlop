using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public GameObject blow;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;

    Quaternion rotation;

	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
    }

    public void Initialize(Vector2 dir)
    {
        this.direction = dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(blow, this.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Instantiate(blow, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
