using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Hedgehog : MovingMeleeEnemy
{
    UnityArmatureComponent armature;

    private IHedgehogState currentState;

    [SerializeField]
    private GameObject spikeParticle;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), FindObjectOfType<Player>().GetComponent<Collider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
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
        health -= 1;
        CameraEffect.Shake(0.5f, 0.4f);
        //Instantiate(spikeParticle, gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 2f, -1f), Quaternion.identity);
        if (IsDead)
        {
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
