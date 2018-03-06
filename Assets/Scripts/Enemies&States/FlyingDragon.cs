using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDragon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
        Destroy(this);
    }
}
