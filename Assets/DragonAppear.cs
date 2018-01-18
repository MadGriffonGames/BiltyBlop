using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAppear : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SoundManager.PlaySound("breathing_fire1");
        }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(18* transform.localScale.x / 1.9f, 1.1f * transform.localScale.y);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(this);
    }

}
