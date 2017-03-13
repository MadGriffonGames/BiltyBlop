using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractiveObject
{
    [SerializeField]
    private Coin coin;

    private bool isEmpty;

	private Animator animator;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        isEmpty = false;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEmpty && other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
			animator = GetComponent<Animator> ();
			animator.SetTrigger ("open");
            isEmpty = true;

        }
    }
}
