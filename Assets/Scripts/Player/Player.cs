using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

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
	private GameObject grave;

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

    public Vector2 StartPosition { get; set; }

    public Vector2 CheckpointPosition { get; set; }

    [SerializeField]
    public GameObject deathUI;

    public int startCoinCount;

    public override void Start () 
	{
        base.Start();
		spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        StartPosition = transform.position;
        MyRigidbody = GetComponent<Rigidbody2D> ();
        GotKey = false;
        CheckpointPosition = StartPosition;
        startCoinCount = GameManager.CollectedCoins;
	}

	void Update()
	{
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -14f)
            {
                MyRigidbody.velocity = Vector2.zero;
                transform.position = StartPosition;
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
			Jump = true;
			if (Mathf.Abs (MyRigidbody.velocity.y) <= 0.01f) {
				SoundManager.PlaySound ("player_jump");
			}
		}
		
		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
            MyAniamtor.SetTrigger("attack");
        }
	}

	private void HandleLayers()
	{
		if (!OnGround) {
			MyAniamtor.SetLayerWeight (1, 1);
			MyAniamtor.SetLayerWeight (2, 0);
		} else {
			MyAniamtor.SetLayerWeight (1, 0);
			MyAniamtor.SetLayerWeight (2, 1);
		}
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
			foreach (Transform ponint in groundPoints) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (ponint.position, groundRadius, whatIsGround);//making circle collider for each groundPoint(area to check ground) 
				for (int i = 0; i < colliders.Length; i++) 
					if (colliders[i].gameObject != gameObject)//if current collider isn't player(gameObject is player, cuz we are in player class)
					{
						return true;//true if we colliding with smthing
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
			foreach (SpriteRenderer sprite in spriteRenderer)
            {
				sprite.enabled = false;
			}
			yield return new WaitForSeconds (.2f);
			foreach (SpriteRenderer sprite in spriteRenderer)
            {
				sprite.enabled = true;
			}
			yield return new WaitForSeconds (.2f);

        }
    }

    public override IEnumerator TakeDamage()
    {
<<<<<<< HEAD
        if (!immortal)
        {
            health -= 1;
            if (!IsDead)
            {
                if(IsFalling || !OnGround)
                    MyAniamtor.SetLayerWeight(1, 0);
                MyAniamtor.SetLayerWeight(2, 1);
                MyAniamtor.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
            else
            {
                MyAniamtor.SetLayerWeight(1, 0);
                MyAniamtor.SetLayerWeight(2, 1);
                MyAniamtor.SetTrigger("death");
                MyRigidbody.velocity = Vector2.zero;
                deathUI.SetActive(true);
            }
            yield return null;
        }
=======
		if (!immortal)
		{
			health -= 1;
			if (!IsDead)
			{
				if (IsFalling || !OnGround) 
				{
					MyAniamtor.SetLayerWeight (1, 0);
					Jump = false;
				}
				MyAniamtor.SetLayerWeight(2, 1);
				MyAniamtor.SetTrigger("damage");
				immortal = true;
				StartCoroutine(IndicateImmortal());
				yield return new WaitForSeconds(immortalTime);
				immortal = false;
			}
			else
			{
				MyAniamtor.SetLayerWeight(1, 0);
				MyAniamtor.SetLayerWeight(2, 1);
				MyAniamtor.SetTrigger("death");
				MyRigidbody.velocity = Vector2.zero;
				deathUI.SetActive(true);
			}
			yield return null;
		}
>>>>>>> origin/DevG
    }

	public void ButtonJump()
	{
		Jump = true;
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
		
	public void InstantiateDeathParticles()
	{
		MakeFX.Instance.MakeDeath();
	}

	public void InstantiateGrave()
	{
		Instantiate (grave, new Vector3(transform.position.x, transform.position.y + 0.21f, transform.position.z), Quaternion.identity);
	}
}

