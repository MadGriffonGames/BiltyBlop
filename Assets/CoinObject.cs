using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour {
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
		{
			Destroy(this.gameObject);
		}   
	}
}
