using UnityEngine;
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

    [SerializeField]
	private Transform[] groundPoints = null;

	[SerializeField]
	private float groundRadius = 0.01f;

	[SerializeField]
	private LayerMask whatIsGround;

	[SerializeField]
	private float jumpForce = 10f;

    private bool immortal = false;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float immortalTime;

	public Rigidbody2D MyRigidbody { get; set;}
	public bool Jump { get; set;}
	public bool OnGround { get; set;}

    [SerializeField]
   private Vector2 startPosition;
   private float mobileInput = 0;

	// Use this for initialization
	public override void Start () 
	{
        spriteRenderer = GetComponent<SpriteRenderer> ();
        startPosition = transform.position;
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D> ();
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
            //Debug.Log (horizontal); it's for print value on unity console 
            OnGround = IsGrounded();
            HandleMovement(horizontal);
            Flip(horizontal);
            HandleLayers();
        }
		/* handling mobile input. unlock this code in case of building on device;
		HandleMovement(mobileInput);
		Flip(mobileInput);
		*/
	}

	private void HandleMovement(float horizontal)
	{
		if (MyRigidbody.velocity.y < 0)
		{
			MyAniamtor.SetBool ("fall", true);
		}
		if (!Attack) 
		{
			MyRigidbody.velocity = new Vector2 (horizontal * movementSpeed, MyRigidbody.velocity.y);//we can move if we are not attacking now
		}
		if (Jump && MyRigidbody.velocity.y == 0)
		{
			MyRigidbody.AddForce (new Vector2(0, jumpForce));
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

	void OnCollisionEnter2D(Collision2D other)//interaction with other colliders
	{
		if (other.transform.tag == "movingPlatform")//if character colliding with platform
		{
			transform.parent = other.transform;//make character chil object of platform
		}
        if (other.gameObject.tag == "Trap")
        {
            MyRigidbody.velocity = Vector2.zero;
            transform.position = startPosition;
        }
        if (other.gameObject.tag == "Coin") 
		{
			Destroy (other.gameObject);
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
		if (MyRigidbody.velocity.y <= 0) 
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
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
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
