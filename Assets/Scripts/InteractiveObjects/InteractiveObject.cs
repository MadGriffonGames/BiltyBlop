using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public Animator animator { get; private set; }

    public virtual void Start ()
    {
        animator = GetComponent<Animator>();
    }
}
