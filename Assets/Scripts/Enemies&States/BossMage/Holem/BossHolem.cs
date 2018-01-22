using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class BossHolem : MovingMeleeEnemy
{
    private IHolemState currentState;
    [SerializeField]
    public GameObject fireball;
    [SerializeField]
    private float shootingRange;
    bool damaged = false;
    public bool walk = false;
    [HideInInspector]
    public bool canAttack;
    [HideInInspector]
    public bool isTimerTick;
    [SerializeField]
    public GameObject spikes;
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
        ChangeState(new HolemPatrolState());
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
        Debug.Log(currentState);
    }

    public void ChangeState(IHolemState newState)
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
               
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.01f);
        damaged = false;
    }

    private void OnEnable()
    {
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

    public void EnableAttackCollider()
    {
        StartCoroutine(AttackColliderDelay());
    }

    IEnumerator AttackColliderDelay()
    {
        yield return new WaitForSeconds(0.42f);
        AttackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        AttackCollider.enabled = false;
    }

    public void ThrowFireball()
    {
        if (this.gameObject.transform.localScale.x > 0)
        {
            GameObject tmp = (GameObject)Instantiate(fireball, transform.position + new Vector3(-2.7f, 1.65f, -5), Quaternion.identity);
            tmp.GetComponent<Seed>().Initialize(Vector2.left);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(fireball, transform.position + new Vector3(2.7f, 1.65f, -5), Quaternion.Euler(0, 0, 180));
            tmp.GetComponent<Seed>().Initialize(Vector2.right);
        }
    }
}
