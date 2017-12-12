using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManekenCollider : MonoBehaviour {

    [SerializeField]
    //GameObject[] throwing;


	// Use this for initialization
	void Start () {
        for (int i = 0; i < Player.Instance.throwingClip.Length; i++)
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), Player.Instance.throwingClip[i].GetComponent<Collider2D>(), true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


}
