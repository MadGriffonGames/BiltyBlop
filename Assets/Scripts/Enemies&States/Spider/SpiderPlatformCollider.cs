using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPlatformCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
