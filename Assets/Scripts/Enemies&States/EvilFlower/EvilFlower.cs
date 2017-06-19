using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EvilFlower : MeleeEnemy
{
    private IEvilFlowerState currentState;
    [SerializeField]
    private GameObject leafParticle;

    [SerializeField]
    GameObject enemySight;

    void Awake()
    {
		armature = GetComponent<UnityArmatureComponent> ();
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new EvilFlowerIdleState());
        Physics2D.IgnoreCollision(enemySight.GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
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

    public void OnTriggerExit2D(Collider2D collision)
    {
        ChangeState(new EvilFlowerIdleState());
    }

    public override IEnumerator TakeDamage()
    {
        health -= Player.Instance.damage;
        CameraEffect.Shake(0.2f, 0.3f);
        if (IsDead)
        {
            Instantiate(leafParticle, this.gameObject.transform.position + new Vector3(-0.4f, 0, -3), Quaternion.identity);
			SoundManager.PlaySound ("flower_death");
            GameManager.deadEnemies.Add(gameObject);
            gameObject.SetActive(false);
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
        SoundManager.PlaySound("bite");

    }

    public void AnimPreparation()
    {
        armature.animation.timeScale = 2f;
        
        armature.animation.Play("PREPARATION");
    }

    private void OnEnable()
    {
        Health = 1;
        Target = null;
        if (Health <= 0)
        {
            ChangeState(new EvilFlowerIdleState());
        }
        Physics2D.IgnoreCollision(enemySight.GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }
        public void StartIgnore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, true);
    }

    public void StopIgnore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, false);
    }
}
