using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipsBonus : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Inventory.Instance.AddItem(Inventory.AMMO, 1);
			Inventory.Instance.UseAmmo ();
			Destroy (this.gameObject);
		}
	}
}
