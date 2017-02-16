using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EvilFlower : Enemy
{
	UnityArmatureComponent armature;

    private IEvilFlowerState currentState;

    [SerializeField]
    public float idleDuration;

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    private GameObject leafParticle;

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)//if enemy has a target
            {
                //return distance between enemy and target <= meleeRange (true or false)
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

    void Awake()
    {
		armature = GetComponent<UnityArmatureComponent> ();
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new EvilFlowerIdleState());
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

    public void ChangeState(IEvilFlowerState newState)
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
        if (!IsDead)
            MyAniamtor.SetTrigger("damage");
        else
        {
            MyAniamtor.SetTrigger("death");
            Instantiate(leafParticle, this.gameObject.transform.position + new Vector3(-0.4f, 0, 0), Quaternion.identity);
            Destroy(gameObject);
        }
        yield return null;
    }

    public void AnimIdle()
	{
        armature.animation.timeScale = 1f;
        armature.animation.Play ("IDLE");
	}

	public void AnimAttack()
	{
		armature.animation.timeScale = 1.5f;
		armature.animation.Play ("ATTACK");

	}

    public void AnimPreparation()
    {
        armature.animation.timeScale = 2f;
        armature.animation.Play("PREPARATION");
    }
}
