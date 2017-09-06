using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    GameObject throwing;
    public GameObject[] throwingClip;
    public int clipSize;
    public int throwingIterator;

    public Rigidbody2D myRigidbody;
    public MeshRenderer[] meshRenderer;
    [SerializeField]
    public GameObject target;
    [SerializeField]
    GameObject shadow;
    public bool bossFight = false;
    public IPlayerState currentState;

    /*
     * Game Managment vars
     */
    public Vector2 startPosition;
    public Vector2 checkpointPosition;
    public float lightIntencityCP;
    public int startCoinCount;
    public int lvlCoins;
    public int monstersKilled;
    public int stars;
    public float maxHealth;
    Dictionary<int, PlayerTimeState> recording = new Dictionary<int, PlayerTimeState>();
    public bool isRewinding = false;
    public int freeCheckpoints;

    /*
     * Action vars
     */
    const float MOVEMENT_SPEED = 8;
    const int JUMP_FORCE = 700;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    public GameObject secretIndication;
    bool isDoubleJumpAllowed;
    public bool Jump { get; set; }
    public bool DoubleJump { get; set; }
    public int jumpTaps = 0;
    public bool canJump;
    public bool Throw { get; set; }
    public bool takeHit = false;
    public float mobileInput = 0;
    private float playerAxis = 0;
    public bool GotKey { get; set; }
    public bool immortal = false;
    public float immortalTime;
    public int damage;
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
    [SerializeField]
    public GameObject timeControllerPrefab;
    [SerializeField]
    GameObject hitFX;

    /*
     * Bonus vars
     */
    public int speedBonusNum = 0;
    public int immortalBonusNum = 0;
    public int damageBonusNum = 0;
    public int jumpBonusNum = 0;
    public int timeBonusNum = 0;
    public float timeScaler = 1;
    public float timeScalerJump = 1;
    public float timeScalerMove = 1;

    /*
     * Skin Managment
     */
    int swordIndex;
    int skinIndex;
    Slot swordSlot;
    Slot[] skinSlots;
	/*
	 * Perk Managment params
	 */
	int dodgeChance; // in %
	float potionTimeScale;
	public float coinScale;
	public int maxClipSize;

	private void Awake()
	{
		SetPerkParams ();
	}

    private void OnEnable()
    {
		ThrowingUI.Instance.SetItems ();
        SetThrowing();

        //for (int i = 1; i < 21; i++)
        //{
        //    PlayerPrefs.DeleteKey("Level" + i + "_collects");
        //}
        //PlayerPrefs.DeleteKey("GeneralStarsCount");

        Debug.Log(PlayerPrefs.GetInt("GeneralStarsCount"));
    }

	private void SetPerkParams()
	{
		// DODRER
		int dodgerLvl = PlayerPrefs.GetInt ("Dodger");
		if (dodgerLvl == 0) 
		{
			dodgeChance = 0;
		} 
		else 
		{
			if (dodgerLvl > 0)
			{
				dodgeChance = (int) PlayerPrefs.GetFloat ("Dodger" + dodgerLvl.ToString ());
				Debug.Log("dodger " + dodgeChance);
			}
		}

		// POTION MANIAC
		int potionLvl = PlayerPrefs.GetInt ("PotionManiac");
		if (potionLvl == 0) 
		{
			potionTimeScale = 1;
		} 
		else 
		{
			if (potionLvl > 0)
			{
				potionTimeScale = PlayerPrefs.GetFloat ("PotionManiac" + potionLvl.ToString ());
				Debug.Log("potion " + potionTimeScale);
			}
		}

		// GREEDY
		int greedLvl = PlayerPrefs.GetInt ("Greedy");
		if (greedLvl == 0) 
		{
			coinScale = 1;
		} 
		else 
		{
			if (greedLvl > 0)
			{
				coinScale = PlayerPrefs.GetFloat ("Greedy" + greedLvl.ToString ());
				Debug.Log("Greedy " + coinScale);
			}
		}

		// AMMO MANIAC
		int clipsLvl = PlayerPrefs.GetInt ("AmmoManiac");
		if (clipsLvl == 0) 
		{
			maxClipSize = 3;
		} 
		else 
		{
			if (clipsLvl > 0)
			{
				maxClipSize = (int) PlayerPrefs.GetFloat ("AmmoManiac" + clipsLvl.ToString ());
				Debug.Log("AmmoManiac " + maxClipSize);
			}
		}
	}

    public override void Start () 
	{
        base.Start();
	

        skinSlots = new Slot [9];

		swordIndex = PlayerPrefs.GetInt("SwordDisplayIndex");
		skinIndex = PlayerPrefs.GetInt ("SkinDisplayIndex");

		health = PlayerPrefs.GetInt ("SkinArmorStat");
		damage = PlayerPrefs.GetInt ("SkinAttackStat");

        SetSlots();


        if (PlayerPrefs.HasKey("Level11"))
        {
            isDoubleJumpAllowed = PlayerPrefs.GetInt("Level11") > 0;
        }
        else
        {
            isDoubleJumpAllowed = false;
        }

        if (timeControllerPrefab != null)
        {
            Instantiate(timeControllerPrefab);
        }

        currentState = new PlayerIdleState();
		meshRenderer = myArmature.gameObject.GetComponentsInChildren<MeshRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();

        startPosition = transform.position;
        checkpointPosition = startPosition;
        lightIntencityCP = FindObjectOfType<Light>().intensity;

        GotKey = false;
        stars = 0;
        lvlCoins = 0;
        freeCheckpoints = 3;
        startCoinCount = GameManager.CollectedCoins;


        maxHealth = health;
        HealthUI.Instance.SetHealthbar();
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

            if (!Jump && OnGround)
            {
                jumpTaps = 0;
            }

            if ((PlayerPrefs.GetInt("SoundsIsOn") == 0) | (!OnGround || (Mathf.Abs(myRigidbody.velocity.x) <= 1)))
                SoundManager.MakeSteps(false);
            else if ((PlayerPrefs.GetInt("SoundsIsOn") == 1) | (((myRigidbody.velocity.x >= 1) || (myRigidbody.velocity.x <= -1)) && (OnGround)))
                SoundManager.MakeSteps(true);
        }
        if (isRewinding)
        {
            if (recording.ContainsKey(TimeController.internalTime))
            {
                PlayTimeState(recording[TimeController.internalTime]);
            }
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
            canJump = true;

            myRigidbody.velocity = new Vector2(horizontal * movementSpeed * timeScalerMove, myRigidbody.velocity.y);
        }
        else
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed * timeScalerMove, myRigidbody.velocity.y);
        if (OnGround && Jump &&  Mathf.Abs(myRigidbody.velocity.y) < 0.5 )
        {
            myRigidbody.AddForce(new Vector2(0, jumpForce * timeScalerJump));
            myRigidbody.velocity = new Vector2(0, 0);
        }
        else if (!OnGround && DoubleJump && canJump && myRigidbody.velocity.y < 6.5f)
        {
            if (myRigidbody.velocity.y < 0)
            {
                myRigidbody.velocity = new Vector2(0, 0);
            }
            myRigidbody.AddForce(new Vector2(0, (jumpForce * 0.45f) * timeScalerJump));
            canJump = false;
            DoubleJump = false;
        }
	}

	private void HandleInput()
	{
		if (!Jump && Input.GetKeyDown (KeyCode.Space)) 
		{
			Jump = true;
			if (Mathf.Abs (myRigidbody.velocity.y) <= 0.01f)
            {
				SoundManager.PlaySound ("player_jump");
			}
		}
        else if (Jump && canJump && Input.GetKeyDown(KeyCode.Space))
        {
            DoubleJump = true;
            SoundManager.PlaySound("double_jump");
            if (Mathf.Abs(myRigidbody.velocity.y) <= 0.01f)
            {
                SoundManager.PlaySound("player_jump");
            }
        }

        if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
            Attack = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Throw = true;
        }
	}

	public override void OnTriggerEnter2D(Collider2D other)
	{
		int tmpNumber = UnityEngine.Random.Range (0, 100);   // DODGER PERK DETECTION
		if (damageSources.Contains(other.tag) && tmpNumber > dodgeChance)
		{
			StartCoroutine(TakeDamage());
		}
        
        if (other.gameObject.CompareTag("DeathTrigger"))
        {
            health -= health - 1;
            if (immortal)
            {
                ParticleSystem tmp = GetComponentInChildren<ParticleSystem>();
                if (tmp != null)
                {
                    tmp.gameObject.SetActive(false);
                }
            }
            immortal = false;
            StartCoroutine(TakeDamage());
        }
    }

    void OnCollisionEnter2D(Collision2D other)//interaction with other colliders
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && myRigidbody.velocity.y != 0)
        {
            MakeFX.Instance.MakeDust();
        }
    }

    private bool IsGrounded()
	{
        int contactPoints = 0;
        foreach (UnityEngine.Transform ponint in groundPoints)
        {
			Collider2D[] colliders = Physics2D.OverlapCircleAll (ponint.position, groundRadius, whatIsGround);//making circle collider for each groundPoint(area to check ground) 
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)//if current collider isn't player(gameObject is player, cuz we are in player class)
                {
                    contactPoints++;
                }
                if (contactPoints >= 2)
                {
                    return true;//true if we colliding with smthing
                }
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
        if (!isRewinding)
        {
            StartCoroutine(AttackColliderDelay());
        }
    }

    IEnumerator AttackColliderDelay()
    {
        yield return new WaitForSeconds(0.11f / myArmature.animation.timeScale);
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
        if (!isRewinding && !IsDead)
        {

            CameraEffect camEffect = Camera.main.GetComponent<CameraEffect>();
            if (!immortal)
            {
                if (bossFight)
                    CameraEffect.Shake(0.5f, 0.4f);
                else
                    CameraEffect.Shake(0.5f, 0.4f);
                health -= 1;
                HealthUI.Instance.SetHealthbar();
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
                    MetricaManager.Instance.deaths++;
                    AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.idiot);
                    ChangeState(new PlayerDeathState());
                    myRigidbody.velocity = Vector2.zero;
                    UI.Instance.timeRewindUI.SetActive(true);
                }
                yield return null;
            }
        }
    }

    /*
     * Rewind time functions
     */

    public void SetRecording(Dictionary<int, PlayerTimeState> recording)
    {
        this.recording = new Dictionary<int, PlayerTimeState>(recording);
    }

    void PlayTimeState(PlayerTimeState playerTimeState)
    {
        this.gameObject.transform.position = playerTimeState.position;
        if (currentState.GetType() != playerTimeState.animationState.GetType())
        {
            if (playerTimeState.animationState.GetType() == new PlayerRunState().GetType())
            {
                if (myArmature.armature.animation.lastAnimationName != "run")
                {
                    myArmature.armature.animation.FadeIn("run", -1, -1);
                }
            }
            else
            {
                myArmature.armature.animation.Stop();
                ChangeState(playerTimeState.animationState);
            }
        }
        Vector3 localScale = transform.localScale;
        localScale.x = playerTimeState.direction ? Mathf.Abs(transform.localScale.x) : -1 * Mathf.Abs(transform.localScale.x);
        transform.localScale = localScale;
    }

    /*
     * Throwing weapon functions
     */

    void SetThrowing()
    {
        throwing = Resources.Load<GameObject>("Throwing/ThrowingKnife");
			clipSize = maxClipSize;
        throwingIterator = SceneManager.GetActiveScene().name == "Level1" ? -1 : clipSize - 1;
        ThrowingUI.Instance.SetThrowBar();
        throwingClip = new GameObject[clipSize];
        for (int i = 0; i < clipSize; i++)
        {
            throwingClip[i] = Instantiate(throwing);
            //disable spriterenderer and collider instead just disable gameobject, because I can't get collider for ignore collision from disabled object
            throwingClip[i].GetComponent<SpriteRenderer>().enabled = false;
            throwingClip[i].GetComponent<Collider2D>().enabled = false;
        }
    }

    public void ResetThrowing()
    {
        for (int i = 0; i < clipSize; i++)
        {
            throwingClip[i].GetComponent<Throwing>().speed = 14;
        }
        ThrowingUI.Instance.SetThrowBar();
    }

    public void ThrowWeapon()
    {
        if (!isRewinding)
        {
            StartCoroutine(ThrowWeaponDelay());
        }
    }

    IEnumerator ThrowWeaponDelay()
    {
        if (throwingIterator >= 0)
        {
            yield return new WaitForSeconds(0.11f / myArmature.animation.timeScale);

            throwingClip[throwingIterator].GetComponent<SpriteRenderer>().enabled = true;
            throwingClip[throwingIterator].GetComponent<Collider2D>().enabled = true;
            throwingClip[throwingIterator].GetComponent<Throwing>().speed = 14;
            SoundManager.PlaySound("kidarian_throw");

            if (this.gameObject.transform.localScale.x > 0)
            {
                throwingClip[throwingIterator].transform.position = this.transform.position + new Vector3(0.8f, 0.1f, -5);
                throwingClip[throwingIterator].transform.rotation = Quaternion.identity;
                throwingClip[throwingIterator].GetComponent<Throwing>().Initialize(Vector2.right);
            }
            else
            {
                throwingClip[throwingIterator].transform.position = this.transform.position + new Vector3(-0.8f, 0.1f, -5);
                throwingClip[throwingIterator].transform.rotation = Quaternion.Euler(0, 0, 180);
                throwingClip[throwingIterator].GetComponent<Throwing>().Initialize(Vector2.left);
            }

            --throwingIterator;
            ThrowingUI.Instance.SetThrowBar();
        }
    }

    /*
    * Mobile Input hendlers
    */

    public void ButtonJump()
	{
		Jump = true;

        if (isDoubleJumpAllowed)
        {
            jumpTaps++;

            if (Jump && canJump && jumpTaps == 2)
            {
                DoubleJump = true;
                jumpTaps = 0;
            }
        }
        
	}

    public void ButtonAttack()
	{
        Attack = true;
	}

	public void ButtonMove(float input)
	{
        playerAxis = input;
	}

    public void ButtonThrow()
    {
        Throw = true;
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
			yield return new WaitForSeconds(duration * potionTimeScale);
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
        damage *= 2;
			yield return new WaitForSeconds(duration * potionTimeScale);
        damageBonusNum--;
        if (damageBonusNum == 0)
        {
            damage /= 2;
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
			yield return new WaitForSeconds(duration * potionTimeScale);
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
        cef.StartBlur(0.35f);
			yield return new WaitForSeconds(duration * potionTimeScale);
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
        timeScalerMove = 1.8f;
        SoundManager.SetPitch(0.5f);
        myArmature.animation.timeScale = 2f;
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.01f;
        myRigidbody.gravityScale = 6;
			yield return new WaitForSeconds(duration * potionTimeScale);
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
        movementSpeed = MOVEMENT_SPEED;
        jumpForce = JUMP_FORCE;
        myArmature.animation.timeScale = 1;
        Time.fixedDeltaTime = 0.02000000f;
    }

    /*
     * Skin Managment
     */

    public void AddSlot(string slotName, ref int i)
    {
        skinSlots[i] = myArmature.armature.GetSlot(slotName);
        i++;
    }

    public void SetSlots()
    {
        int i = 0;

        myArmature.zSpace = 0.02f;

        swordSlot = myArmature.armature.GetSlot("Sword");

        AddSlot("r_hand_2", ref i);
        AddSlot("l_hand_2", ref i);
        AddSlot("leg", ref i);
        AddSlot("leg1", ref i);
        AddSlot("Shoulder_l", ref i);
        AddSlot("Shoulder_r", ref i);
        AddSlot("torso", ref i);
        AddSlot("mex", ref i);
        AddSlot("head", ref i);
    }

    public void SetIndexes()
    {
        swordSlot.displayIndex = swordIndex;

        for (int i = 0; i < skinSlots.Length; i++)
        {
            skinSlots[i].displayIndex = skinIndex;
        }
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
    }

    public void PlayerRevive()
    {
        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        foreach (MeshRenderer mesh in meshRenderer)
        {
            mesh.enabled = true;
        }
    }

    public void Heal()
    {
        MakeFX.Instance.MakeHeal();
    }

    public void ChangeCameraTarget(GameObject targetObject,Vector3 localPosition)
    {
        target.transform.SetParent(targetObject.gameObject.transform);
        target.transform.localPosition = localPosition;
    } 
}

