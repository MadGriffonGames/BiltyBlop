using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainScript : MonoBehaviour {

    [SerializeField]
    private Transform chainTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //other.rigidbody.gravityScale = 0;
            other.gameObject.layer = 9;//9 - is layer called "Platform"
            other.transform.SetParent(chainTransform);
            Collider2D collider = GetComponent<Collider2D>();
            //collider.enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
