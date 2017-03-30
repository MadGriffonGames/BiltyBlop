using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public Animator MyAnimator { get; private set; }

    public virtual void Start ()
    {
        MyAnimator = GetComponent<Animator>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }
}
