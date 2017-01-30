using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Coin rigidCoin;

    private bool isEmpty = false;

	// Use this for initialization
	void Start ()
    {
        rigidCoin = GameObject.FindObjectOfType<Coin>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEmpty && other.gameObject.tag == "Player")
        {
            isEmpty = true;
            for (int i = 0; i < 10; i++)
                Instantiate(rigidCoin, (this.gameObject.transform.position + new Vector3(0, 0, 0) ), Quaternion.identity);
        }
    }
}
