using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdSound : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.PlaySound("birds_sound");
            Destroy(this);
        }
    }
}
