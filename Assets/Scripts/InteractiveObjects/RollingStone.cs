using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : MonoBehaviour {

    public Rigidbody2D MyRigidbody;

	// Use this for initialization
	void Start () {

        MyRigidbody = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            MyRigidbody.AddForce(new Vector2(100, 10));
        }
    }
}
