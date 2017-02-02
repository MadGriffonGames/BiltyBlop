using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private Coin coin;

    private bool isEmpty = false;

	// Use this for initialization
	void Start ()
    {
       
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
                Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
