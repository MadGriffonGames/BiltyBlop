using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWall : InteractiveObject {
    [SerializeField]
    public GameObject groundParticle;
    [SerializeField]
    Collider2D damageCollider;
    float movementSpeed = 3f;
    bool facingRight = false;

    bool isGrounded = false;

    // Use this for initialization
    void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Sword") || other.transform.CompareTag("Throwing"))
        {
            CameraEffect.Shake(0.2f, 0.2f);
            Instantiate(groundParticle, this.gameObject.transform.position + new Vector3(0, 0.5f, -3), Quaternion.identity);
            Instantiate(groundParticle, this.gameObject.transform.position + new Vector3(0, 2.5f, -3), Quaternion.identity);
            Instantiate(groundParticle, this.gameObject.transform.position + new Vector3(0, -2.5f, -3), Quaternion.identity);
            SoundManager.PlaySound("wooden_box1");
            Destroy(this.gameObject);

            Destroy(damageCollider);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }

        if (collision.transform.CompareTag("Column Stone"))
        {
            CameraEffect.Shake(0.2f, 0.2f);
            Instantiate(groundParticle, this.gameObject.transform.position + new Vector3(0, 0.5f, -3), Quaternion.identity);
            SoundManager.PlaySound("wooden_box1");
            Destroy(this.gameObject);
            Destroy(damageCollider);
        }

    }

    public void Move()
    {
        this.transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }
}
