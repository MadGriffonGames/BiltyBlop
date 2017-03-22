using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Hedgehog : MovingMeleeEnemy
{
    UnityArmatureComponent armature;

    private IHedgehogState currentState;

    [SerializeField]
    GameObject spikeParticle;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), FindObjectOfType<Player>().GetComponent<Collider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        enabled = true;
        ChangeState(new HedgehogIdleState());
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

    public void ChangeState(IHedgehogState newState)
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
        health -= Player.Instance.damage;
        CameraEffect.Shake(0.5f, 0.4f);
        SetHealthbar();
        if (IsDead)
        {
            Player.Instance.monstersKilled++;
            SoundManager.PlaySound("hedgehog_death");
            Instantiate(spikeParticle, gameObject.transform.position + new Vector3(0, 0.53f, -1f), Quaternion.identity);
            Destroy(transform.parent.gameObject);

        }
        yield return null;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter2D(other);
        if (other.gameObject.tag == "Edge")
        {
            ChangeDirection();
        }
    }

    public void AnimIdle()
    {
        armature.animation.timeScale = 1f;
        armature.animation.Play("Idle");
    }

    public void AnimPreattack()
    {
        armature.animation.timeScale = 2f;
        armature.animation.Play("Pre attack");
    }

    public void AnimAttack()
    {
        armature.animation.timeScale = 3f;
        armature.animation.Play("Attack");
    }
}
