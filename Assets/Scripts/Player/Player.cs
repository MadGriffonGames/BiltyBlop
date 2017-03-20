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
    public Rigidbody2D MyRigidbody { get; set; }
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    public float immortalTime;
    [SerializeField]
    GameObject deathUI;
    public bool OnGround { get; set; }
    public bool Jump { get; set; }
    public bool immortal = false;
    private SpriteRenderer[] spriteRenderer;
    private HingeJoint2D hingeJoint;
    private float mobileInput = 0;
    public bool GotKey { get; set; }
    public Vector2 StartPosition { get; set; }
    public Vector2 CheckpointPosition { get; set; }
    public int startCoinCount;
    public int monstersKilled;
    public int collectables;

    public override void Start () 
	{
        base.Start();
		spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        StartPosition = transform.position;
        MyRigidbody = GetComponent<Rigidbody2D> ();
        GotKey = false;
        CheckpointPosition = StartPosition;
        startCoinCount = GameManager.CollectedCoins;
        monstersKilled = 0;
        collectables = 0;
        hingeJoint = GetComponent<HingeJoint2D>();
        hingeJoint.enabled = false;
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
        if (OnGround && Jump &&  Mathf.Abs(MyRigidbody.velocity.y) < 0.1 )
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
            if (hingeJoint.enabled)
            {
                this.TakeOfJoint();
                MyRigidbody.AddForce(new Vector2(0, jumpForce));
                MyRigidbody.velocity = new Vector2(0, 0);
            }
			Jump = true;
			if (Mathf.Abs (MyRigidbody.velocity.y) <= 0.01f)
            {
				SoundManager.PlaySound ("player_jump");
			}
        }
		
		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
            MyAniamtor.SetTrigger("attack");
        }
	}

    private void TakeOfJoint()
    {
        hingeJoint.enabled = false;
        hingeJoint.connectedBody = null;
        MyRigidbody.mass = 1;
        MyRigidbody.gravityScale = 3;
    }

    private void JoinToHinge(Collision2D other)
    {
        hingeJoint.enabled = true;
        hingeJoint.connectedBody = other.rigidbody;
        MyRigidbody.mass = 10;
        MyRigidbody.velocity = new Vector2(0, 0);
    }

	public override void OnTriggerEnter2D(Collider2D other)
	{
        base.OnTriggerEnter2D(other);
        if (other.tag == "Chain")
        {
            hingeJoint.enabled = true;
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            hingeJoint.connectedBody = rb;
            MyRigidbody.gravityScale = 3;
            MyRigidbody.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Chain")
        {
           // MyRigidbody.velocity = new Vector2(0, 0);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Chain")
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 0;
        }
    }


    void OnCollisionEnter2D(Collision2D other)//interaction with other colliders
    {

        if (other.transform.tag == "movingPlatform")//if character colliding with platform
        {
            transform.parent = other.transform;//make character chil object of platform
        }

        if (other.transform.tag == "Chain")
        {
            //JoinToHinge(other);            
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

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAniamtor.SetLayerWeight(1, 1);
            MyAniamtor.SetLayerWeight(2, 0);
        }
        else
        {
            MyAniamtor.SetLayerWeight(1, 0);
            MyAniamtor.SetLayerWeight(2, 1);
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

    public IEnumerator IndicateImmortal()
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
        if (!immortal)
		{
            CameraEffect.Shake(0.5f, 0.4f);
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
				SoundManager.PlaySound ("player_takehit1");
				immortal = true;
				StartCoroutine(IndicateImmortal());
				yield return new WaitForSeconds(immortalTime);
				immortal = false;
			}
			else
			{
				MyAniamtor.SetLayerWeight(1, 0);
				MyAniamtor.SetLayerWeight(2, 1);
				SoundManager.PlaySound ("player_death");
				MyAniamtor.SetTrigger("death");
				MyRigidbody.velocity = Vector2.zero;
				deathUI.gameObject.SetActive(true); 
			}
			yield return null;
		}
    }

	public void ButtonJump()
	{
        if (hingeJoint.enabled)
        {
            this.TakeOfJoint();
        }
        Jump = true;
		MyAniamtor.SetTrigger("jump");
        if (Mathf.Abs(MyRigidbody.velocity.y) <= 0.01f)
        {
            SoundManager.PlaySound("player_jump");
        }
        
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
		Instantiate (grave, new Vector3(transform.position.x, transform.position.y + 0.19f, transform.position.z), Quaternion.identity);
	}

    public void Heal()
    {
        MakeFX.Instance.MakeHeal();
    }
}

