using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    public GameObject particle;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;

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
        if (other.gameObject.tag == "Player")
        {
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
