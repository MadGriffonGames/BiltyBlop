using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{

    public Animator animator { get; private set; }

    [SerializeField]
    public Player player;

    // Use this for initialization
    public virtual void Start ()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
