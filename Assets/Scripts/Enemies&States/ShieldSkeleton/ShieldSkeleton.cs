﻿using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class ShieldSkeleton : MovingMeleeEnemy
{
    private IShieldSkeletonState currentState;
    [SerializeField]
    private GameObject deathParticle;
    [SerializeField]
    GameObject shieldFx;

    public bool isTimerTick;
    bool damaged = false;
    public bool walk = false;
    bool isTurningAround;

    float timer;

    const float ATTACK_REFRESH = 1.2f;
    const float TURN_AROUND_TIME = 0.95f;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        isSkeleton = true;

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start()
    {
        base.Start();

        timer = 0;

        armature.armature.GetSlot("r_hand_1").offset.x = 100;

        isAttacking = false;
        isTimerTick = false;

        armature.armature.cacheFrameRate = 60;

        ChangeState(new ShieldSkeletonPatrolState());
   }

    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage && !Attack)
            {
                currentState.Execute();
            }
            LookAtKid();
        }

        if (isTimerTick)
        {
            timer += Time.deltaTime;
        }

        if (timer >= ATTACK_REFRESH)
        {
            timer = 0;
            isTimerTick = false;
        }

        armature.armature.GetSlot("r_hand_1").globalTransformMatrix.a = 100;
    }

    public void ChangeState(IShieldSkeletonState newState)
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

        float posDiference;

        if (transform.localScale.x > 0)
        {
            posDiference = Mathf.Abs(Player.Instance.transform.position.x) - Mathf.Abs(transform.position.x);
        }
        else
        {
            posDiference = Mathf.Abs(transform.position.x) - Mathf.Abs(Player.Instance.transform.position.x);
        }

        if (!damaged && posDiference < 0)
        {
            health -= actualDamage;
            IndicateDamage();
            if (health != 0)
                SoundManager.PlaySound("skeleton_pain");
            damaged = true;
            MakeFX.Instance.MakeHitFX(gameObject.transform.position + new Vector3(0, 0.3f), new Vector3(1, 1, 1));
            StartCoroutine(AnimationDelay());
            CameraEffect.Shake(0.2f, 0.3f);

            SetHealthbar();

            if (IsDead)
            {
                AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
                Instantiate(deathParticle, gameObject.transform.position + new Vector3(0, 1f, -1f), Quaternion.identity);
                SoundManager.PlaySound("skeleton_death");
                SpawnCoins(3, 5);
                GameManager.deadEnemies.Add(gameObject);
                gameObject.SetActive(false);
            }
            yield return null;
        }
        else
        {
            shieldFx.SetActive(true);
        }
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
            ChangeState(new ShieldSkeletonPatrolState());
            armature.animation.timeScale = 1;
            Health = 4;
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
    }

    public void LocalMove()
    {
        if (!walk)
        {
            walk = true;
            armature.animation.FadeIn("walk", -1, -1);
        }
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

    IEnumerator ChangeDir()
    {
        if (!isTurningAround)
        {
            isTurningAround = true;
            yield return new WaitForSeconds(TURN_AROUND_TIME);
            ChangeDirection();
            isTurningAround = false;
        }
    }

    void LookAtKid()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;//xDir shows target from left or right side 

            if ((xDir < 0 && facingRight) || (xDir > 0 && !facingRight))
            {
                StartCoroutine(ChangeDir());
            }
        }
    }
}
