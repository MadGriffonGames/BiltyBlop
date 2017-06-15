using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedEnvironment : MonoBehaviour
{
    Animator myAnimator;

	void Start ()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.enabled = false;
	}

    private void OnBecameVisible()
    {
        myAnimator.enabled = true;
    }

    private void OnBecameInvisible()
    {
        myAnimator.enabled = false;
    }
}
