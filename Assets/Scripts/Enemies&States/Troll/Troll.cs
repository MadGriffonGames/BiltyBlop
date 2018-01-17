using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Troll : MovingMeleeEnemy
{
    private ITrollState currentState;
    [SerializeField]
    private GameObject deathParticle;
    [SerializeField]
    public GameObject blow;
    [SerializeField]
    public GameObject bomb;
    [SerializeField]
    private float shootingRange;
    bool damaged = false;
    public bool walk = false;
    [HideInInspector]
    public bool canAttack;
    [HideInInspector]
    public bool isTimerTick;
    float timer;

    public bool InShootingRange
    {
        get
        {
            if (Target != null)//if enemy has a target
            {
                //return distance between enemy and target <= meleeRange (true or false)
                return Vector2.Distance(transform.position, Target.transform.position) <= shootingRange;
            }
            return false;
        }
    }

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start()
    {
        base.Start();

        canAttack = true;
        isAttacking = false;
        isTimerTick = false;
        timer = 0;
        ChangeState(new TrollPatrolState());
    }

    void Update()
    {
        if (!IsDead)
        {           
            LookAtTarget();

            if (isTimerTick)
            {
                timer += Time.deltaTime;
            }
            if (timer >= 2f)
            {
                isTimerTick = false;
                timer = 0;
                canAttack = true;
            }
        }
        if (!TakingDamage && !Attack)
        {
            currentState.Execute();
        }
    }

    public void ChangeState(ITrollState newState)
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
            MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
            CameraEffect.Shake(0.2f, 0.3f);
            SetHealthbar();
            if (IsDead)
            {
                AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
                SoundManager.PlaySound("enemyher loud");

                GetComponent<SwordIgnoreCollision>().enabled = true;

                ChangeState(new TrollSelfDestroyState());
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.01f);
        damaged = false;
    }

    public void OnBlow()
    {
        health = 0;

        SpawnCoins(2, 5);
        GameManager.deadEnemies.Add(gameObject);

        walk = false;

        GameObject tmp1 = Instantiate(bomb, transform.position + new Vector3(1, 1f, 0), Quaternion.identity);
        tmp1.transform.parent = null;
        tmp1.GetComponent<Rigidbody2D>().velocity = new Vector2(3,4);

        GameObject tmp2 = Instantiate(bomb, transform.position + new Vector3(-1, 1f, 0), Quaternion.identity);
        tmp2.transform.parent = null;
        tmp2.GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 4);

        GameObject tmp3 = Instantiate(bomb, transform.position + new Vector3(1.5f, 1.5f, 0), Quaternion.identity);
        tmp3.transform.parent = null;
        tmp3.GetComponent<TrollBomb>().blowTime = 1.5f;
        tmp3.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 5);

        GameObject tmp4 = Instantiate(bomb, transform.position + new Vector3(-1.5f, 1.5f, 0), Quaternion.identity);
        tmp4.transform.parent = null;
        tmp4.GetComponent<TrollBomb>().blowTime = 1.5f;
        tmp4.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 5);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GetComponent<SwordIgnoreCollision>().enabled = false;

        ResetCoinPack();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, false);
        for (int i = 0; i < Player.Instance.clipSize; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.throwingClip[i].GetComponent<Collider2D>(), false);
        }

        Target = null;
        damaged = false;
       
        if (Health <= 0)
        {
            ChangeState(new TrollPatrolState());

            Health = maxHealth;
        }
        
        SetHealthbar();
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
            armature.animation.FadeIn("Walk", -1, -1);
        }
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }
}
