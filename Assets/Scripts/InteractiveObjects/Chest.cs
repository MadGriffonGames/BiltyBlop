using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractiveObject
{

    private bool isEmpty;

	[SerializeField]
	private GameObject[] coins;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        isEmpty = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEmpty && other.transform.CompareTag("Sword"))
        {
			animator.SetTrigger ("open");
			foreach (GameObject coin in coins)
			{
				coin.SetActive(true);				
				Rigidbody2D rb = coin.GetComponent<Rigidbody2D> ();
				rb.velocity =  new Vector2 (UnityEngine.Random.Range(-2f,2f), UnityEngine.Random.Range(7f,9f));
			}
            isEmpty = true;
        }
    }
}
