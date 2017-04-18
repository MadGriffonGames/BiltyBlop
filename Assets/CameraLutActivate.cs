using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLutActivate : MonoBehaviour {

    CameraLutActivate lut;
    // Use this for initialization
    void Start () {
        GetComponent<CameraEffect>();
        lut = GetComponent<CameraLutActivate>();
        lut.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("Player"))
            lut.enabled = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
