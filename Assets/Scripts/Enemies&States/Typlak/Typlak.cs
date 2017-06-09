﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Typlak : MovingMeleeEnemy
{
    private ITyplakState currentState;
    [SerializeField]
    private GameObject typlakParticle;
    public bool attack;
    bool damaged = false;
    public bool walk = false;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        attack = false;
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new TyplakPatrolState());
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
    }

    public void ChangeState(ITyplakState newState)
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
            damaged = true;
            health -= Player.Instance.damage;
            StartCoroutine(AnimationDelay());
            CameraEffect.Shake(0.2f, 0.3f);
            SetHealthbar();
            if (IsDead)
            {
                SoundManager.PlaySound("enemyher loud");
                Player.Instance.monstersKilled++;
                Instantiate(typlakParticle, gameObject.transform.position + new Vector3(0, 1f, -1f), Quaternion.identity);
                Destroy(transform.parent.gameObject);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        damaged = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter2D(other);
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

    public void AnimIdle()
    {
        armature.animation.timeScale = 1f;
        armature.animation.Play("idle");
    }

    public void PlaySound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    public void AnimPreattack()
    {
        armature.animation.timeScale = 1.5f;
        armature.animation.Play("preattack");

    }

    public void AnimAttack()
    {
        armature.animation.timeScale = 1.5f;
        AttackCollider.enabled = true;
        armature.animation.Play("Attack");

    }

    public void AnimWalk()
    {
        armature.animation.timeScale = 1f;
        armature.animation.Play("walk");
    }
}
