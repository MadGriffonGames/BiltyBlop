using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField]
    public Animator[] MyAnimator;

	void Start ()
    {
        MyAnimator = GetComponentsInChildren<Animator>();
        MyAnimator[0].enabled = false;
        MyAnimator[1].enabled = false;
    }

    private void OnBecameVisible()
    {
        MyAnimator[0].enabled = true;
        MyAnimator[1].enabled = true;
    }

    private void OnBecameInvisible()
    {
        MyAnimator[0].enabled = false;
        MyAnimator[1].enabled = false;
    }
}
