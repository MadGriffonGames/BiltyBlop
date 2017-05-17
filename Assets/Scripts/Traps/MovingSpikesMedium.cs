using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikesMedium : InteractiveObject {

	// Use this for initialization
	public override void Start () {

        base.Start();
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            MyAnimator.SetTrigger("enter");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            MyAnimator.SetTrigger("exit");
            MyAnimator.ResetTrigger("enter");
        }
    }

}
