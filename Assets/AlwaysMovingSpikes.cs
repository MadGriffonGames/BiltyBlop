using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysMovingSpikes : MonoBehaviour
{
    Animator MyAnimator;
	// Use this for initialization
	void Start () {
        MyAnimator = GetComponent<Animator>();
        MyAnimator.enabled = false;
    }

	
    private void OnBecameVisible()
    {
        MyAnimator.enabled = true;
    }

    private void OnBecameInvisible()
    {
        MyAnimator.enabled = false;
    
    }
}
