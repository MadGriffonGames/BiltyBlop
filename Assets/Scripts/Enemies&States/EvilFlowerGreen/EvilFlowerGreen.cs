using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EvilFlowerGreen : RangeEnemy
{
    private IEFGreenState currentState;
    [SerializeField]
    GameObject seed;
    [SerializeField]
    GameObject leafParticle;

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
        health -= Player.Instance.damage;
        CameraEffect.Shake(0.5f, 0.4f);
        if (IsDead)
        {
            Player.Instance.monstersKilled++;
            Instantiate(leafParticle, this.gameObject.transform.position + new Vector3(0.3f, 0.4f, -1f), Quaternion.identity);
			SoundManager.PlaySound ("flower_death");
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
        armature.animation.timeScale = 1.2f;
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
