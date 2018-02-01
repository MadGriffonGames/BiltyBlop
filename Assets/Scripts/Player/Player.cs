using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DragonBones;
using System;
using UnityEngine.UI;
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

    [HideInInspector]
    GameObject throwing;
    [HideInInspector]
    public GameObject[] throwingClip;
    [HideInInspector]
    public int clipSize;
    [HideInInspector]
    public int throwingIterator;
    [HideInInspector]
    public Rigidbody2D myRigidbody;
    [HideInInspector]
    public MeshRenderer[] meshRenderer;
    [SerializeField]
    public GameObject target;
    [SerializeField]
    GameObject shadow;
    [HideInInspector]
    public bool bossFight = false;
    [HideInInspector]
    public bool invertedControls = false;
    [HideInInspector]
    public IPlayerState currentState;

    /*
     * Game Managment vars
     */
    [HideInInspector]
    public Vector2 startPosition;
    [HideInInspector]
    public Vector2 checkpointPosition;
    [HideInInspector]
    public float lightIntencityCP;
    [HideInInspector]
    public int startCoinCount;
    [HideInInspector]
    public int lvlCoins;
    [HideInInspector]
    public int monstersKilled;
    public int stars;
    public float maxHealth;
    Dictionary<int, PlayerTimeState> recording = new Dictionary<int, PlayerTimeState>();
    [HideInInspector]
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
    [HideInInspector]
    public bool Jump { get; set; }
    [HideInInspector]
    public bool DoubleJump { get; set; }
    [HideInInspector]
    public int jumpTaps = 0;
    [HideInInspector]
    public bool canJump;
    [HideInInspector]
    public bool Throw { get; set; }
    [HideInInspector]
    public bool takeHit = false;
    [HideInInspector]
    public float mobileInput = 0;
    [HideInInspector]
    private float playerAxis = 0;
    [HideInInspector]
    public bool GotKey { get; set; }
    public bool immortal = false;
    [HideInInspector]
    public float immortalTime;
    [HideInInspector]
    public int meleeDamage;
    [HideInInspector]
    public int throwDamage;
    [HideInInspector]
    public override bool IsDead
    {
        get { return health <= 0; }
    }
    [SerializeField]
    GameObject dodgeFx;

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
    [HideInInspector]
    public int speedBonusNum = 0;
    [HideInInspector]
    public int immortalBonusNum = 0;
    [HideInInspector]
    public int damageBonusNum = 0;
    [HideInInspector]
    public int jumpBonusNum = 0;
    [HideInInspector]
    public int timeBonusNum = 0;
    [HideInInspector]
    public float timeScaler = 1;
    [HideInInspector]
    public float timeScalerJump = 1;
    [HideInInspector]
    public float timeScalerMove = 1;
	[SerializeField]
	public GameObject bonusFXObject;
    [HideInInspector]
    public Animator bonusFX;

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
    [HideInInspector]
    public float coinScale;
    [HideInInspector]
    public int maxClipSize;

	private void Awake()
	{
        SetPerkParams ();
        health = PlayerPrefs.GetInt("SkinArmorStat");
        maxHealth = health;
        meleeDamage = PlayerPrefs.GetInt("SwordAttackStat");
        throwDamage = PlayerPrefs.GetInt("ThrowAttackStat");

        HealthUI.Instance.SetHealthbar();
    }

    private void OnEnable()
    {
		ThrowingUI.Instance.SetItems ();
        SetThrowing();
    }

    public override void Start () 
	{
        base.Start();
		bonusFX = bonusFXObject.GetComponent<Animator> ();
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level3")
        {
            DevToDev.Analytics.Tutorial(-1);
        }

        bonusFX = GetComponentInChildren<Animator>();

        skinSlots = new Slot [9];

		swordIndex = PlayerPrefs.GetInt("SwordDisplayIndex");
		skinIndex = PlayerPrefs.GetInt ("SkinDisplayIndex");
		

        SetSlots();
        SetIndexes();

        if (PlayerPrefs.HasKey("Level11"))
        {
            isDoubleJumpAllowed = PlayerPrefs.GetInt("Level11") > 0;
        }

		#if UNITY_EDITOR
			isDoubleJumpAllowed = true;
		#endif

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
            if (Mathf.Abs(mobileInput + playerAxis / 10) <= 1 && playerAxis != 0)
                mobileInput += playerAxis / 10;
            else mobileInput = playerAxis;
            if (!UI.Instance.controlsUI.activeInHierarchy)
            {
                mobileInput = 0;
                playerAxis = 0;
            }

#if UNITY_EDITOR
            float horizontal;
            if (!invertedControls)
            {
                horizontal = Input.GetAxis("Horizontal");
            }
            else
            {
                horizontal = -Input.GetAxis("Horizontal");
            }
            HandleMovement(horizontal);
            Flip(horizontal);

#elif UNITY_ANDROID
            if (invertedControls)
	        {
                HandleMovement(-mobileInput);
                Flip(-mobileInput);
	        }
            else
            {
                HandleMovement(mobileInput);
                Flip(mobileInput);
            }

#elif UNITY_IOS
            if (invertedControls)
	        {
                HandleMovement(-mobileInput);
                Flip(-mobileInput);
	        }
            else
            {
                HandleMovement(mobileInput);
                Flip(mobileInput);
            }
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

        if (!TakingDamage)
        {
            currentState.Execute();
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
        else if (!OnGround && DoubleJump && canJump && myRigidbody.velocity.y < 6.5f && isDoubleJumpAllowed)
        {
            if (myRigidbody.velocity.y < 0)
            {
                myRigidbody.velocity = new Vector2(0, 0);
            }
            myRigidbody.AddForce(new Vector2(0, (jumpForce * 0.45f) * timeScalerJump));
            myArmature.animation.FadeIn("double_jump_start", -1, 1);
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
            if (isDoubleJumpAllowed)
            {
                SoundManager.PlaySound("double_jump");
            }
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
  		if (damageSources.Contains(other.tag))
		{
            int tmpNumber = UnityEngine.Random.Range(0, 100);   // DODGER PERK DETECTION
            
            if (tmpNumber > dodgeChance)
            {
                if (!AttackCollider.IsTouching(other))
                    StartCoroutine(TakeDamage());
            }
            else
            {
                dodgeFx.SetActive(true);
            }
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
                    AppMetrica.Instance.ReportEvent("#DEATH in " + GameManager.currentLvl);
                    DevToDev.Analytics.CustomEvent("#DEATH in " + GameManager.currentLvl);
                    ChangeState(new PlayerDeathState());
                    myRigidbody.velocity = Vector2.zero;
					bonusFX.SetTrigger ("reset");
					bonusFXObject.SetActive (false);            
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
        throwing = Resources.Load<GameObject>("Throwing/ThrowingObject");
		clipSize = maxClipSize;
		Sprite[] throwSprites = Resources.LoadAll<Sprite> ("Throw/ThrowSprites");
		string throwName = PlayerPrefs.GetString ("Throw");
		for(int i=0; i <= throwSprites.Length; i++)
		{
			if (throwSprites [i].name == throwName) 
			{
				throwing.GetComponent<SpriteRenderer> ().sprite = throwSprites [i];
				break;
			}
		}
		throwing.GetComponent<Throwing> ().damage = PlayerPrefs.GetInt ("ThrowAttackStat");
		throwDamage = throwing.GetComponent<Throwing> ().damage;
		throwing.GetComponent<Throwing> ().speed = PlayerPrefs.GetFloat ("ThrowSpeedStat");

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
			throwingClip[i].GetComponent<Throwing>().speed = PlayerPrefs.GetFloat ("ThrowSpeedStat");
			throwing.GetComponent<Throwing> ().damage = PlayerPrefs.GetInt ("ThrowAttackStat");
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
            yield return new WaitForSeconds(0.05f / myArmature.animation.timeScale);

            throwingClip[throwingIterator].GetComponent<SpriteRenderer>().enabled = true;
            throwingClip[throwingIterator].GetComponent<Collider2D>().enabled = true;
            //throwingClip[throwingIterator].GetComponent<Throwing>().speed = 14;
            SoundManager.PlaySound("kidarian_throw");

            if (this.gameObject.transform.localScale.x > 0)
            {
                throwingClip[throwingIterator].transform.position = this.transform.position + new Vector3(1.5f, 0.1f, -5);
				throwingClip [throwingIterator].transform.rotation = Quaternion.Euler (0, 0, -90);
                throwingClip[throwingIterator].GetComponent<Throwing>().Initialize(Vector2.right);
            }
            else
            {
                throwingClip[throwingIterator].transform.position = this.transform.position + new Vector3(-1.5f, 0.1f, -5);
                throwingClip[throwingIterator].transform.rotation = Quaternion.Euler(0, 0, 90);
				throwingClip [throwingIterator].transform.localScale = new Vector3 (throwingClip [throwingIterator].transform.localScale.x * -1, throwingClip [throwingIterator].transform.localScale.y, throwingClip [throwingIterator].transform.localScale.z);
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
		bonusFXObject.SetActive (true);
        StartCoroutine(ImmortalBonus(duration));
		MakeFX.Instance.MakeImmortalBonus(duration * potionTimeScale);
		bonusFX.SetTrigger ("immortal");
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
			bonusFX.SetTrigger ("reset");
			bonusFXObject.SetActive (false);
        }
    }

    public void ExecBonusDamage(float duration)
    {
        StartCoroutine(DamageBonus(duration));
		MakeFX.Instance.MakeDamageBonus(duration * potionTimeScale);
		bonusFX.SetTrigger ("damage");

    }

    public IEnumerator DamageBonus(float duration)
    {
        damageBonusNum++;
        meleeDamage *= 2;
		yield return new WaitForSeconds(duration * potionTimeScale);
        damageBonusNum--;
        if (damageBonusNum == 0)
        {
            meleeDamage /= 2;
			bonusFX.SetTrigger ("reset");
			bonusFXObject.SetActive (false);
        }
    }

    public void ExecBonusJump(float duration)
    {
		bonusFXObject.SetActive (true);
        StartCoroutine(JumpBonus(duration));
		MakeFX.Instance.MakeJumpBonus(duration * potionTimeScale);
		bonusFX.SetTrigger ("jump");
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
			bonusFX.SetTrigger ("reset");
			bonusFXObject.SetActive (false);
        }
    }

    public void ExecBonusSpeed(float duration)
    {
		bonusFXObject.SetActive (true);
        StartCoroutine(SpeedBonus(duration));
		MakeFX.Instance.MakeSpeedBonus(duration * potionTimeScale);
		bonusFX.SetTrigger ("speed");
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
			bonusFX.SetTrigger ("reset");
			bonusFXObject.SetActive (false);
        }
    }

    public void ExecBonusTime(float duration)
    {
		bonusFXObject.SetActive (true);
        StartCoroutine(TimeBonus(duration));
		MakeFX.Instance.MakeTimeBonus(duration * potionTimeScale);
	    bonusFX.SetTrigger ("time");
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
            bonusFX.SetTrigger("reset");
			bonusFXObject.SetActive (false);
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
        meleeDamage = PlayerPrefs.GetInt("SwordAttackStat"); ;
        movementSpeed = MOVEMENT_SPEED;
        jumpForce = JUMP_FORCE;
        myArmature.animation.timeScale = 1;
        Time.fixedDeltaTime = 0.02000000f;
        bonusFX.enabled = false;
    }

    public bool IsBonusUsed()
    {
        int tmp = damageBonusNum + immortalBonusNum + speedBonusNum + timeBonusNum;
        if (tmp > 0)
        {
            return true;
        }
        return false;
    }

    /*
     * Skin Managment
     */

    public void AddSlot(string slotName, ref int i)
    {
        skinSlots[i] = myArmature.armature.GetSlot(slotName);
        skinSlots[i].displayController = "none";
        i++;
    }

    public void SetSlots()
    {
        int i = 0;

        myArmature.zSpace = 0.02f;

        swordSlot = myArmature.armature.GetSlot("Sword");
        swordSlot.displayController = "none";

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

    private void SetPerkParams()
    {
        // DODRER
        int dodgerLvl = PlayerPrefs.GetInt("Dodger");
        if (dodgerLvl == 0)
        {
            dodgeChance = 0;
        }
        else
        {
            if (dodgerLvl > 0)
            {
                dodgeChance = (int)PlayerPrefs.GetFloat("Dodger" + dodgerLvl.ToString());
            }
        }

        // POTION MANIAC
        int potionLvl = PlayerPrefs.GetInt("PotionManiac");
        if (potionLvl == 0)
        {
            potionTimeScale = 1;
        }
        else
        {
            if (potionLvl > 0)
            {
                potionTimeScale = PlayerPrefs.GetFloat("PotionManiac" + potionLvl.ToString());
            }
        }

        // GREEDY
        int greedLvl = PlayerPrefs.GetInt("Greedy");
        if (greedLvl == 0)
        {
            coinScale = 1;
        }
        else
        {
            if (greedLvl > 0)
            {
                coinScale = PlayerPrefs.GetFloat("Greedy" + greedLvl.ToString());
            }
        }

        // AMMO MANIAC
        int clipsLvl = PlayerPrefs.GetInt("AmmoManiac");
        if (clipsLvl == 0)
        {
            maxClipSize = 3;
        }
        else
        {
            if (clipsLvl > 0)
            {
                maxClipSize = (int)PlayerPrefs.GetFloat("AmmoManiac" + clipsLvl.ToString());
            }
        }
    }

    IEnumerator Invert()
    {
        if (!invertedControls)
        {
            invertedControls = true;
            CameraEffect.changeColors = true;
            Time.timeScale = 0.75f;
            yield return new WaitForSeconds(3.5f);
            invertedControls = false;
            Time.timeScale = 1f;
            CameraEffect.changeColors = false;
            FindObjectOfType<Camera>().GetComponent<CameraEffect>().ResetColors();
        }        
    }

    public void InvertControls()
    {
        if (!immortal)
        {
            StartCoroutine(Invert());
        }
    }
}

