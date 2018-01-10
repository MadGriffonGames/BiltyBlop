using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroInputCollider : MonoBehaviour {

    bool isInTrigger = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isInTrigger)
        {
            Player.Instance.mobileInput = 0;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInTrigger = true;
            Player.Instance.mobileInput = 0;
            Player.Instance.ChangeState(new PlayerIdleState());
        }
    }

}
