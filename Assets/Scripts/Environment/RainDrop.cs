using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour
{
    Animator MyAnimator;
	
	void Start ()
    {
        MyAnimator = GetComponent<Animator>();
        MyAnimator.enabled = false;
        enabled = false;
	}

    public void ChangePosition()
    {
        transform.localPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.1f), -4);
    }

    private void OnBecameVisible()
    {
        MyAnimator.enabled = true;
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        MyAnimator.enabled = false;
        enabled = false;
    }
}
