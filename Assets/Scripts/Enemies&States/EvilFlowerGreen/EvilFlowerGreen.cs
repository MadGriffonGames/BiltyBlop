﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EvilFlowerGreen : RangeEnemy
{
    UnityArmatureComponent armature;

    private IEFGreenState currentState;

    [SerializeField]
    private GameObject seed;

    [SerializeField]
    private GameObject leafParticle;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new EFGreenIdleState());
    }

    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage && !Attack)
            {
                currentState.Execute();
            }
        }
    }

    public void ChangeState(IEFGreenState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter2D(other);
    }

    public override IEnumerator TakeDamage()
    {
        health -= 1;
        CameraEffect.Shake(0.5f, 0.4f);
        if (IsDead)
        {
            MyAniamtor.SetTrigger("death");
            Instantiate(leafParticle, this.gameObject.transform.position + new Vector3(0.3f, 0.4f, -1f), Quaternion.identity);
            Destroy(gameObject);
        }
        yield return null;
    }

    public void ThrowSeed()
    {
        if (this.gameObject.transform.localScale.x > 0)
        {
            GameObject tmp = (GameObject)Instantiate(seed, transform.position + new Vector3(0, 0.55f, 0), Quaternion.identity);
            tmp.GetComponent<Seed>().Initialize(Vector2.left);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(seed, transform.position + new Vector3(0, 0.55f, 0), Quaternion.Euler(0, 0, 180));
            tmp.GetComponent<Seed>().Initialize(Vector2.right);
        }
    }

    public void AnimIdle()
    {
        armature.animation.timeScale = 1f;
        armature.animation.Play("IDLE");
    }

    public void AnimAttack()
    {
        armature.animation.timeScale = 1.5f;
        ThrowSeed();
        armature.animation.Play("ATTACK");
    }

    public void AnimPreparation()
    {
        armature.animation.timeScale = 2f;
        armature.animation.Play("PREPARATION");
    }
}