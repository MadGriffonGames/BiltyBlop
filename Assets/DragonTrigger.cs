using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTrigger : MonoBehaviour {

    [SerializeField]
    GameObject dragon;
    [SerializeField]
    GameObject dragonShadow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dragon.gameObject.SetActive(true);
            dragonShadow.gameObject.SetActive(true);
        }
    }
}
