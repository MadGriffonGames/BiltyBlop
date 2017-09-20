﻿using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class FlyingPig : MovingRangedEnemy
{
    private IFlyingPigState currentState;
    [SerializeField]
    private GameObject deathParticle;
    public bool canAttack;
    bool damaged = false;
    [SerializeField]
    GameObject fireball;

    Vector3 startPos;

    public bool isTimerTick;
    float timer;
    [SerializeField]
    float attackRefreshTime;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);

        canAttack = true;
        isTimerTick = false;
        timer = 0;
    }

    public override void Start()
    {
        base.Start();

        ChangeState(new FlyingPigPatrolState());

        startPos = transform.position;
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

        if (timer > attackRefreshTime)
        {
            canAttack = true;
            isTimerTick = false;
            timer = 0;
        }
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
                fireball.SetActive(false);
                AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
                SoundManager.PlaySound("penguin_death");
                //Instantiate(penguinParticle, gameObject.transform.position + new Vector3(0, 1f, -1f), Quaternion.identity);
                SpawnCoins(2, 5);
                GameManager.deadEnemies.Add(gameObject);
                gameObject.SetActive(false);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.01f);
        damaged = false;
    }

    public void ChangeState(IFlyingPigState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter2D(other);
        if (other.gameObject.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), true);
        }
    }

    public void ThrowFireball()
    {
        fireball.SetActive(true);
        fireball.transform.parent = null;
    }

    private void OnEnable()
    {
        ResetCoinPack();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        SetHealthbar();
        Target = null;
        damaged = false;

        if (Health <= 0)
        {
            transform.position = startPos;

            ChangeState(new FlyingPigPatrolState());

            fireball.GetComponent<PigFireball>().transform.parent = transform;
            fireball.GetComponent<PigFireball>().transform.localPosition = fireball.GetComponent<PigFireball>().startPosition;

            Health = 3;
            SetHealthbar();
        }
    }
}