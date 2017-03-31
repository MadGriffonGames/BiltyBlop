using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EvilFlower : MeleeEnemy
{
    private IEvilFlowerState currentState;
    [SerializeField]
    private GameObject leafParticle;

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

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("stay");
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        ChangeState(new EvilFlowerIdleState());
    }

    public override IEnumerator TakeDamage()
    {
        health -= Player.Instance.damage;
        CameraEffect.Shake(0.5f, 0.4f);

        if(IsDead)
        {
            Player.Instance.monstersKilled++;
            Instantiate(leafParticle, this.gameObject.transform.position + new Vector3(-0.4f, 0, 0), Quaternion.identity);
			SoundManager.PlaySound ("flower_death");
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
