using UnityEngine;
using System.Collections;
using DragonBones;
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
    public Rigidbody2D myRigidbody;
    public MeshRenderer[] meshRenderer;
    [SerializeField]
    public GameObject target;
    [SerializeField]
    GameObject shadow;
    public bool bossFight = false;
    IPlayerState currentState;

    /*
     * Game Managment vars
     */
    public Vector2 startPosition;
    public Vector2 checkpointPosition;
    public float lightIntencityCP;
    public int startCoinCount;
    public int lvlCoins;
    public int monstersKilled;
    public int collectables;

    /*
     * Action vars
     */
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    public GameObject secretHalo;
    public bool Jump { get; set; }
    public bool takeHit = false;
    public float mobileInput = 0;
    private float playerAxis = 0;
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
        get { return myRigidbody.velocity.y < -0.1; }
    }
    [SerializeField]
    private UnityEngine.Transform[] groundPoints = null;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    public bool OnGround { get; set; }

    /*
     * Bonus vars
     */
    int speedBonusNum = 0;
    int immortalBonusNum = 0;
    int damageBonusNum = 0;
    int jumpBonusNum = 0;
    int timeBonusNum = 0;
    public float timeScaler = 1;
    public float timeScalerJump = 1;
    public float timeScalerMove = 1;

    public override void Start () 
	{
        base.Start();
        currentState = new PlayerIdleState();
		meshRenderer = GetComponentsInChildren<MeshRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        GotKey = false;
        checkpointPosition = startPosition;
        lightIntencityCP = FindObjectOfType<Light>().intensity;
        startCoinCount = GameManager.CollectedCoins;
        monstersKilled = 0;
        collectables = 0;
        lvlCoins = 0;
    }

	void Update()
	{
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -140f)
            {
                myRigidbody.velocity = Vector2.zero;
                transform.position = startPosition;
            }
			HandleInput();
        }
        if (OnGround && !shadow.activeInHierarchy)
        {
            shadow.SetActive(true);
        }
        if (!OnGround && shadow.activeInHierarchy)
        {
            shadow.SetActive(false);
        }

    }

    void FixedUpdate() 
	{
        if (!TakingDamage && !IsDead)
        {
            currentState.Execute();

            if (Mathf.Abs(mobileInput + playerAxis / 10) <= 1 && playerAxis != 0)
                mobileInput += playerAxis / 10;
            else mobileInput = playerAxis;
            
#if UNITY_EDITOR
            float horizontal = Input.GetAxis("Horizontal");
            HandleMovement(horizontal);
            Flip(horizontal);

#elif UNITY_ANDROID
            HandleMovement(mobileInput);
            Flip(mobileInput);

#elif UNITY_IOS
            HandleMovement(mobileInput);
            Flip(mobileInput);
#endif
            OnGround = IsGrounded();

            if (!OnGround || (Mathf.Abs(myRigidbody.velocity.x) <= 1))
                SoundManager.MakeSteps(false);
            else if (((myRigidbody.velocity.x >= 1) || (myRigidbody.velocity.x <= -1)) && (OnGround))
                SoundManager.MakeSteps(true);
        }
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    private void HandleMovement(float horizontal)
	{
        if (IsFalling)
        {
            gameObject.layer = LayerMask.NameToLayer("Falling");
        }
		if (OnGround) 
		{
            if(!Attack)
                myRigidbody.velocity = new Vector2 (horizontal * movementSpeed * timeScaler, myRigidbody.velocity.y);
            else
                myRigidbody.velocity = new Vector2(horizontal * movementSpeed * 0.85f * timeScalerMove, myRigidbody.velocity.y);
        }
        else
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed * timeScalerMove, myRigidbody.velocity.y);
        if (OnGround && Jump &&  Mathf.Abs(myRigidbody.velocity.y) < 0.1 )
        {
            myRigidbody.AddForce(new Vector2(0, jumpForce * timeScalerJump));
            myRigidbody.velocity = new Vector2(0,0);
        }
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			Jump = true;
			if (Mathf.Abs (myRigidbody.velocity.y) <= 0.01f)
            {
				SoundManager.PlaySound ("player_jump");
			}
		}
		
		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
            Attack = true;
        }
	}

	public override void OnTriggerEnter2D(Collider2D other)
	{
        base.OnTriggerEnter2D(other);
        if (other.CompareTag("DeathTrigger"))
        {
            health -= health;
            if (immortal)
            {
                ParticleSystem tmp = GetComponentInChildren<ParticleSystem>();
                tmp.gameObject.SetActive(false);
            }
            immortal = false;
            StartCoroutine(TakeDamage());
        }
    }

    void OnCollisionEnter2D(Collision2D other)//interaction with other colliders
    {

        if (other.transform.tag == "movingPlatform")//if character colliding with platform
        {
            transform.parent = other.transform;//make character chil object of platform
            target.transform.SetParent(other.gameObject.transform);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && myRigidbody.velocity.y != 0)
        {
            MakeFX.Instance.MakeDust();
        }
    }

    void OnCollisionExit2D(Collision2D other)
	{
		if (other.transform.tag == "movingPlatform")//if character stop colliding with platform
		{
			transform.parent = null;//make charter object non child
            target.transform.SetParent(this.gameObject.transform);
		}
			
	}

    private bool IsGrounded()
	{
			foreach (UnityEngine.Transform ponint in groundPoints) 
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

    public void PlayRandomSound(string sound1, string sound2)
    {
        System.Random soundCount = new System.Random();
        if (soundCount.Next(0, 1) == 0)
            SoundManager.PlaySound(sound1);
        else
            SoundManager.PlaySound(sound2);
    }

    public void EnableAttackCollider()
    {
        StartCoroutine(AttackColliderDelay());
    }

    IEnumerator AttackColliderDelay()
    {
        yield return new WaitForSeconds(0.1f);
        AttackCollider.enabled = true;
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
			foreach (MeshRenderer sprite in meshRenderer)
            {
				sprite.enabled = false;
			}
			yield return new WaitForSeconds (.1f);
			foreach (MeshRenderer sprite in meshRenderer)
            {
				sprite.enabled = true;
			}
			yield return new WaitForSeconds (.2f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        CameraEffect camEffect = Camera.main.GetComponent<CameraEffect>();
        if (!immortal)
        {
            CameraEffect.Shake(0.5f, 0.4f);
            health -= 1;
            if (!IsDead)
            {
                takeHit = true;

                if (IsFalling || !OnGround)
                {
                    Jump = false;
                }
                camEffect.ShowBlood(0.5f);
                System.Random soundFlag = new System.Random();
                if (soundFlag.Next(0, 2) == 0)
                    SoundManager.PlaySound("player_damage1");
                else
                    SoundManager.PlaySound("player_damage2");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
            else
            {
                ChangeState(new PlayerDeathState());
                myRigidbody.velocity = Vector2.zero;
                UI.Instance.DeathUI.SetActive(true);
            }
            yield return null;
        }
    }

    /*
    * Input hendlers
    */

    public void ButtonJump()
	{
		Jump = true;
	}

    public void ButtonAttack()
	{
        Attack = true;
	}

	public void ButtonMove(float input)
	{
        playerAxis = input;
		
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
        immortalBonusNum++;
        immortal = true;
        yield return new WaitForSeconds(duration);
        immortalBonusNum--;
        if (immortalBonusNum == 0)
        {
            immortal = false;
        }
    }

    public void ExecBonusDamage(float duration)
    {
        StartCoroutine(DamageBonus(duration));
        MakeFX.Instance.MakeDamageBonus(duration);
    }

    public IEnumerator DamageBonus(float duration)
    {
        damageBonusNum++;
        damage = 2;
        yield return new WaitForSeconds(duration);
        damageBonusNum--;
        if (damageBonusNum == 0)
        {
            damage = 1;
        }
    }

    public void ExecBonusJump(float duration)
    {
        StartCoroutine(JumpBonus(duration));
        MakeFX.Instance.MakeJumpBonus(duration);
    }

    public IEnumerator JumpBonus(float duration)
    {
        jumpBonusNum++;
        jumpForce = 1200;
        yield return new WaitForSeconds(duration);
        jumpBonusNum--;
        if (jumpBonusNum == 0)
        {
            jumpForce = 700;
        }
    }

    public void ExecBonusSpeed(float duration)
    {
        StartCoroutine(SpeedBonus(duration));
        MakeFX.Instance.MakeSpeedBonus(duration);
    }

    public IEnumerator SpeedBonus(float duration)
    {
        speedBonusNum++;
        movementSpeed = 16;
        myArmature.animation.timeScale = 2;
        timeScalerMove = 0.7f;
        Camera cam = Camera.main;
        CameraEffect cef = cam.GetComponent<CameraEffect>();
        cef.StartBlur();
        yield return new WaitForSeconds(duration);
        speedBonusNum--;

        if (speedBonusNum == 0)
        {
            myRigidbody.gravityScale = 3;
            movementSpeed = 8;
            myArmature.animation.timeScale = 1;
            timeScalerMove = 1;
            cef.StopBlur();
        }
    }

    public void ExecBonusTime(float duration)
    {
        StartCoroutine(TimeBonus(duration));
        MakeFX.Instance.MakeTimeBonus(duration);
    }

    public IEnumerator TimeBonus(float duration)
    {
        timeBonusNum++;
        timeScaler = 1.6f;
        timeScalerJump = 3f;
        timeScalerMove = 1.3f;
        SoundManager.SetPitch(0.5f);
        myArmature.animation.timeScale = 1.6f;
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.01f;
        myRigidbody.gravityScale = 6;
        yield return new WaitForSeconds(duration);
        timeBonusNum--;

        if (timeBonusNum == 0)
        {
            SoundManager.SetPitch(1f);
            timeScaler = 1;
            timeScalerJump = 1;
            timeScalerMove = 1;
            myArmature.animation.timeScale = 1;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            myRigidbody.gravityScale = 3;
        }
    }

    public void ResetBonusValues()
    {        
        SoundManager.SetPitch(1f);
        timeScaler = 1;
        timeScalerJump = 1;
        timeScalerMove = 1;
        Time.timeScale = 1;
        myRigidbody.gravityScale = 3;
        immortal = false;
        damage = 1;
        movementSpeed = 7;
        jumpForce = 700;
        myArmature.animation.timeScale = 1;
        Time.fixedDeltaTime = 0.02000000f;
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
        myRigidbody.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void PlayerRevive()
    {
        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        foreach (MeshRenderer sprite in meshRenderer)
        {
            sprite.enabled = true;
        }
    }

    public void Heal()
    {
        MakeFX.Instance.MakeHeal();
    }
}

