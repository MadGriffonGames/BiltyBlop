using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIce : MonoBehaviour {


    [SerializeField]
    GameObject iceBreakParticle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            this.gameObject.SetActive(false);
            Instantiate(iceBreakParticle, this.gameObject.transform.position + new Vector3(0, 1f), Quaternion.identity);
            SoundManager.PlaySound("ice_crack");
        }
    }
}
