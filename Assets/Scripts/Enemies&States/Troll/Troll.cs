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
    bool damaged = false;
    public bool walk = false;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        isAttacking = false;
        ChangeState(new TrollPatrolState());
    }

    void Update()
    {
        if (!IsDead)
        {           
            LookAtTarget();
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
        
        gameObject.SetActive(false);
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
            if (armature.animation.lastAnimationName != "charge")
            {
                armature.animation.FadeIn("Walk", -1, -1);
            }
        }
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }
}
