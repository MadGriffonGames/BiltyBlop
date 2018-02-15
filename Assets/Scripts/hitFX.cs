using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitFX : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AnimationEnd()
    {
        this.gameObject.SetActive(false);
    }
}
