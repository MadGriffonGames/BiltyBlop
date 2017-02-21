﻿using UnityEngine;
using System.Collections;
using System;

public class Player : Character
{

	private static Player instance;

	public static Player Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<Player> ();
			return instance;
		}
	}
    
    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    public bool IsFalling
    {
        get
        {
                return MyRigidbody.velocity.y < -0.1;
        }
    }

    [SerializeField]
	private Transform[] groundPoints = null;

	[SerializeField]
	private float groundRadius;

    [SerializeField]
	private LayerMask whatIsGround;
    
    public bool OnGround { get; set; }

    [SerializeField]
	private float jumpForce = 10f;

    public bool Jump { get; set; }

    private bool immortal = false;

    private SpriteRenderer[] spriteRenderer;

    [SerializeField]
    private float immortalTime;

	public Rigidbody2D MyRigidbody { get; set;}

    private float mobileInput = 0;

    public bool GotKey { get; set; }

    private Vector2 startPosition;

    // Use this for initialization
    public override void Start () 
	{
        base.Start();
		spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        startPosition = transform.position;
        MyRigidbody = GetComponent<Rigidbody2D> ();
        GotKey = false;
	}

	void Update()
	{
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -14f)
            {
                MyRigidbody.velocity = Vector2.zero;
                transform.position = startPosition;
            }
			HandleInput();
        }

	}
		
	// Update is called once per frame
	void FixedUpdate() 
	{
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            OnGround = IsGrounded();
            //HandleMovement(horizontal);
            //Flip(horizontal);
            HandleMovement(mobileInput);
            Flip(mobileInput);
            HandleLayers();
        }
	}

	private void HandleMovement(float horizontal)
	{
        if (IsFalling)
        {
            MyAniamtor.SetBool("fall", true);
            gameObject.layer = LayerMask.NameToLayer("Falling");
        }
		if (OnGround) 
		{
            if(!Attack)
                MyRigidbody.velocity = new Vector2 (horizontal * movementSpeed, MyRigidbody.velocity.y);
            else
                MyRigidbody.velocity = new Vector2(horizontal * movementSpeed * 0.85f , MyRigidbody.velocity.y);
        }
        else
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        if (Jump &&  Mathf.Abs(MyRigidbody.velocity.y) < 0.1 )
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
            MyRigidbody.velocity = new Vector2(0,0);
        }

        MyAniamtor.SetFloat ("speed", Mathf.Abs (horizontal));
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
            MyAniamtor.SetTrigger("jump");
		}
		
		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
            MyAniamtor.SetTrigger("attack");
        }
	}

	private void HandleLayers()
	{
		if (!OnGround)
			MyAniamtor.SetLayerWeight (1, 1);
		else
			MyAniamtor.SetLayerWeight (1, 0);
	}

	public override void OnTriggerEnter2D(Collider2D other)
	{
        base.OnTriggerEnter2D(other);
        if (other.gameObject.tag == "Door" && GotKey)
        {
            GotKey = false;
            KeyUI.Instance.KeyImage.enabled = false;
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)//interaction with other colliders
    {

        if (other.transform.tag == "movingPlatform")//if character colliding with platform
        {
            transform.parent = other.transform;//make character chil object of platform
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && MyRigidbody.velocity.y != 0 && OnGround)
        {
            MakeFX.Instance.MakeDust();
        }
    }

    void OnCollisionExit2D(Collision2D other)
	{
		if (other.transform.tag == "movingPlatform")//if character stop colliding with platform
		{
			transform.parent = null;//make charter object non child
		}
			
	}

    private bool IsGrounded()
	{
		//if (MyAniamtor.GetLayerWeight(2) == 0) 
		{
			foreach (Transform ponint in groundPoints) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (ponint.position, groundRadius, whatIsGround);//making circle collider for each groundPoint(area to check ground) 
				for (int i = 0; i < colliders.Length; i++) 
					if (colliders[i].gameObject != gameObject)//if current collider isn't player(gameObject is player, cuz we are in player class)
					{
						return true;//true if we colliding with smthing
					}
			}
		}
		return false;
	}


	private void Flip(float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) 
		{
            ChangeDirection();
		}
	}

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
			foreach (SpriteRenderer sprite in spriteRenderer) {
				sprite.enabled = false;
			}
			yield return new WaitForSeconds (.2f);
			foreach (SpriteRenderer sprite in spriteRenderer) {
				sprite.enabled = true;
			}
			yield return new WaitForSeconds (.2f);

        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            health -= 1;
            if (!IsDead)
            {
                MyAniamtor.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
            else
            {
                MyAniamtor.SetLayerWeight(1, 0);
                MyAniamtor.SetTrigger("death");
                MyRigidbody.velocity = Vector2.zero;
            }
            yield return null;
        }
    }

	public void ButtonJump()
	{
		MyAniamtor.SetTrigger("jump");
	}
	public void ButtonAttack()
	{
		 MyAniamtor.SetTrigger("attack");
	}
	public void ButtonMove(float input)
	{
		mobileInput = input;
	}
}
