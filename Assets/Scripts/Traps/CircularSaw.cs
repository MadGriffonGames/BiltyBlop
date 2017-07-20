using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSaw : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float delay;
    Animator myAnimator;

    void Start ()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.speed = speed;
	}

    IEnumerator Delay()
    {
        myAnimator.enabled = false;
        yield return new WaitForSeconds(delay);
        myAnimator.enabled = true;
    }
}
