using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public Animator animator { get; private set; }

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
    }
}
