using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!this.isActiveAndEnabled) {
			Destroy(this.transform.parent.gameObject);
		}		
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), other.gameObject.GetComponent<Collider2D> (), true);
		}
		if (other.gameObject.CompareTag ("Enemy")) 
		{
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), other.gameObject.GetComponent<Collider2D> (), true);
		}
	}

}
