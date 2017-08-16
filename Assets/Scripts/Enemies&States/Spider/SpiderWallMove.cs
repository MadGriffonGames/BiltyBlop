using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWallMove : MonoBehaviour {

    bool isGrounded = false;
    bool facingRight = false;
    float movementSpeed = 15f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isGrounded)
        {
            Move();
        }
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            this.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
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
