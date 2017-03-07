using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class StoneHolem : MovingMeleeEnemy
{
    UnityArmatureComponent armature;

    private IStoneHolemState currentState;

    [SerializeField]
    private GameObject stoneParticle;

    [SerializeField]
    private GameObject attackParticle;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), FindObjectOfType<Player> ().GetComponent<Collider2D> (), true);
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new StoneHolemIdleState());
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

    public void ChangeState(IStoneHolemState newState)
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
        MyAniamtor.SetTrigger("damage");
		SoundManager.PlaySound ("holem_takingdamage");
        Instantiate(stoneParticle, gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 2f, -1f), Quaternion.identity);
        if (IsDead)
        {
            Player.Instance.monstersKilled++;
            SoundManager.PlaySound ("holem_death");
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

    public void HolemMeleeAttack()
    {
        MeleeAttack();
		SoundManager.PlaySound ("holem_smash");
        CameraEffect.Shake(0.7f, 0.4f);
        if (transform.localScale.x > 0)
            Instantiate(attackParticle, gameObject.transform.position + new Vector3(-3, 0, -2), Quaternion.identity);
        else
            Instantiate(attackParticle, gameObject.transform.position + new Vector3(3, 0, -2), Quaternion.identity);
    }

    public void AnimIdle()
    {
        armature.animation.timeScale = 1f;
        armature.animation.Play("IDLE");
    }

    public void AnimPreattack()
    {
        armature.animation.timeScale = 1f;
        armature.animation.Play("PREATTACK");

    }

    public void AnimAttack()
    {
        armature.animation.timeScale = 1f;
        armature.animation.Play("ATTACK_SHORT");

    }

    public void AnimWalk()
    {
        armature.animation.timeScale = 4f;
        armature.animation.Play("WALK");
    }

    public void AnimGetHit()
    {
        armature.animation.timeScale = 4f;
        armature.animation.Play("GETHIT");
    }

}
