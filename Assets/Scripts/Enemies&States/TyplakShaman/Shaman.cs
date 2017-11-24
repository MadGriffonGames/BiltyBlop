using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Shaman : MovingRangedEnemy
{
    private IShamanState currentState;
    [SerializeField]
    GameObject fireball;
    [SerializeField]
    private GameObject typlakParticle;
    bool damaged = false;
    public bool walk = false;
    public bool isAttacking ;
    public bool isIdle;
    public bool isTimerTick;
    public float timer;

    List<Slot> slots;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        slots = armature.armature.GetSlots();
        SetIndexes();
        ChangeState(new ShamanPatrolState());
        isTimerTick = false;
        isAttacking = false;
        isIdle = false;
        timer = 0;
    }

    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage && !Attack)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }

        if (isTimerTick)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 3.5f)
        {
            timer = 0;
            isTimerTick = false;
            isAttacking = false;
            isIdle = false;
        }
    }

    public void ChangeState(IShamanState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public override IEnumerator TakeDamage()
    {
        if (!damaged)
        {
            health -= actualDamage;
            
            damaged = true;
            StartCoroutine(AnimationDelay());
            MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
            CameraEffect.Shake(0.2f, 0.3f);
            SetHealthbar();
            if (IsDead)
            {
                AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
                SoundManager.PlaySound("enemyher loud");
                //Instantiate(typlakParticle, gameObject.transform.position + new Vector3(0, 1f, -1f), Quaternion.identity);
                SpawnCoins(2, 4);
                GameManager.deadEnemies.Add(gameObject);
                gameObject.SetActive(false);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        damaged = false;
    }

    private void OnEnable()
    {
        ResetCoinPack();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);

        Target = null;
        damaged = false;

        if (Health <= 0)
        {
            ChangeState(new ShamanPatrolState());
            Health = maxHealth;
            SetHealthbar();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter2D(other);
        if (other.gameObject.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), true);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), true);
        }
    }

    public void LocalMove()
    {
        if (!walk)
        {
            walk = true;
            armature.animation.FadeIn("walk1_slow", -1, -1);
        }
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

    public void SetIndexes()
    {
        int tmp = 3;
        foreach (Slot slot in slots)
        {
            slot.displayIndex = tmp;
            slot.displayController = "none";
        }
    }

    public void ThrowFireball()
    {
        if (this.gameObject.transform.localScale.x < 0)
        {
            GameObject tmp = (GameObject)Instantiate(fireball, transform.position + new Vector3(2.5f, 1.85f, -5), Quaternion.identity);
            tmp.GetComponent<ShamanFireball>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(fireball, transform.position + new Vector3(-2.5f, 1.85f, -5), Quaternion.Euler(0, 0, 180));
            tmp.GetComponent<ShamanFireball>().Initialize(Vector2.left);
        }
    }
}
