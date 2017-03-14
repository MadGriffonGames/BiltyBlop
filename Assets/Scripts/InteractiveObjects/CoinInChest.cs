using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInChest : InteractiveObject {

	// Use this for initialization
	void Start () {
		
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
		{
			Rigidbody2D rb = this.GetComponent<Rigidbody2D> ();
			rb.gravityScale = 0;
			animator.SetTrigger("collected");
			GameManager.CollectedCoins++;
			SoundManager.PlaySound ("coin_collect");
			Destroy(this.gameObject);
		}  
		if (other.transform.CompareTag ("Coin") && !other.transform.CompareTag ("Sword")) 
		{
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), other, true);
		}

	}
}
