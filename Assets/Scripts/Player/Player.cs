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


    [SerializeField]
    private GameObject grave;
    public Rigidbody2D MyRigidbody { get; set; }
    private SpriteRenderer[] spriteRenderer;

    /*
     * Game Managment vars
     */
    public Vector2 StartPosition { get; set; }
    public Vector2 CheckpointPosition { get; set; }
    public int startCoinCount;
    public int monstersKilled;
    public int collectables;

    /*
     * Action vars
     */
    [SerializeField]
    private float jumpForce;
    public bool Jump { get; set; }
    private float mobileInput = 0;
    public bool GotKey { get; set; }
    public bool immortal = false;
    public float immortalTime;
    public int damage = 1;
    public override bool IsDead
    {
        get { return health <= 0; }
    }

    /*
     * Ground Check vars
     */
    public bool IsFalling
    {
        get { return MyRigidbody.velocity.y < -0.1; }
    }
    [SerializeField]
    private Transform[] groundPoints = null;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    public bool OnGround { get; set; }

    /*
     * Bonus triggers
     */
    public bool jumpBonus = false;
    public bool damageBonus = false;
    public bool immortalBonus = false;
    public float timeScaler = 1;
    public float timeScalerJump = 1;
    public float timeScalerMove = 1;

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
                MyRigidbody.velocity = new Vector2 (horizontal * movementSpeed * timeScaler, MyRigidbody.velocity.y);
            else
                MyRigidbody.velocity = new Vector2(horizontal * movementSpeed * 0.85f * timeScalerMove, MyRigidbody.velocity.y);
        }
        else
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed * timeScalerMove, MyRigidbody.velocity.y);
        if (OnGround && Jump &&  Mathf.Abs(MyRigidbody.velocity.y) < 0.1 )
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce * timeScalerJump));
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
        if (other.CompareTag("DeathTrigger"))
        {
            health -= health - 1;
            StartCoroutine(TakeDamage());
        }
    }

    void OnCollisionEnter2D(Collision2D other)//interaction with other colliders
    {

        if (other.transform.tag == "movingPlatform")//if character colliding with platform
        {
            transform.parent = other.transform;//make character chil object of platform
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && MyRigidbody.velocity.y != 0)
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
				SoundManager.PlayMusic ("player_death",true);
				MyAniamtor.SetTrigger("death");
				MyRigidbody.velocity = Vector2.zero;
				UI.Instance.DeathUI.SetActive(true);
			}
			yield return null;
		}
    }

    /*
    * Mobile input hendlers
    */

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

    /*
     * Bonus functions
     */

    public void ExecBonusImmortal(float duration)
    {
        StartCoroutine(ImmortalBonus(duration));
        MakeFX.Instance.MakeImmortalBonus(duration);
    }

    public IEnumerator ImmortalBonus(float duration)
    {
        immortal = true;
        yield return new WaitForSeconds(duration);
        immortal = false;
    }

    public void ExecBonusDamage(float duration)
    {
        StartCoroutine(DamageBonus(duration));
        MakeFX.Instance.MakeDamageBonus(duration);
    }

    public IEnumerator DamageBonus(float duration)
    {
        damage = 2;
        yield return new WaitForSeconds(duration);
        damage = 1;
    }

    public void ExecBonusJump(float duration, float force)
    {
        StartCoroutine(JumpBonus(duration, force));
        MakeFX.Instance.MakeJumpBonus(duration);
    }

    public IEnumerator JumpBonus(float duration, float force)
    {
        float tmp = jumpForce;
        jumpForce += force;
        yield return new WaitForSeconds(duration);
        jumpForce = tmp;
    }

    public void ExecBonusSpeed(float duration)
    {
        StartCoroutine(SpeedBonus(duration));
        MakeFX.Instance.MakeSpeedBonus(duration);
    }

    public IEnumerator SpeedBonus(float duration)
    {
        
        movementSpeed *= 2;
        MyAniamtor.speed *= 2;
        MyRigidbody.gravityScale *= 1.4f;
        yield return new WaitForSeconds(duration);
        MyRigidbody.gravityScale /= 1.4f;
        movementSpeed /= 2;
        MyAniamtor.speed /= 2;
    }

    public void ExecBonusTime(float duration)
    {
        StartCoroutine(TimeBonus(duration));
        MakeFX.Instance.MakeTimeBonus(duration);
    }

    public IEnumerator TimeBonus(float duration)
    {
        timeScaler = 1.6f;
        timeScalerJump = 2.8f;
        timeScalerMove = 1.3f;
        SoundManager.SetPitch(0.5f);
        MyAniamtor.speed *= timeScaler;
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime /= 2;
        MyRigidbody.gravityScale *= 2f;
        yield return new WaitForSeconds(duration);
        SoundManager.SetPitch(1f);
        timeScaler = 1;
        timeScalerJump = 1;
        timeScalerMove = 1;
        MyAniamtor.speed /= timeScaler;
        Time.timeScale = 1;
        Time.fixedDeltaTime *= 2;
        MyRigidbody.gravityScale = 3;
    }

    /*
     * Other
     */

    public void InstantiateDeathParticles()
    {
        MakeFX.Instance.MakeDeath();
    }

    public void InstantiateGrave()
    {
        Instantiate(grave, new Vector3(transform.position.x, transform.position.y + 0.19f, transform.position.z + 4.5f), Quaternion.identity);
        instance.MyRigidbody.bodyType = RigidbodyType2D.Static;
        BoxCollider2D boxCollider = instance.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    public void PlayerRevive()
    {
        instance.MyRigidbody.bodyType = RigidbodyType2D.Dynamic;
        BoxCollider2D boxCollider = instance.GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
    }

    public void Heal()
    {
        MakeFX.Instance.MakeHeal();
    }
}

