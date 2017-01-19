using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public Animator MyAniamtor { get; private set; }

    [SerializeField]
    protected float movementSpeed = 3.0f;

    protected bool facingRight;//chek direction(true if we look right)

    public bool Attack { get; set; }

    // Use this for initialization
    public virtual void Start ()
    {
        facingRight = true;
        MyAniamtor = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
