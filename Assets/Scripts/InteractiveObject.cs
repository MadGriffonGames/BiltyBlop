using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{

    public Animator animator { get; private set; }

    // Use this for initialization
    public virtual void Start ()
    {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
