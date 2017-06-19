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
    [SerializeField]
    public GameObject acidFx;
    [SerializeField]
    GameObject enemySight;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new EFGreenIdleState());
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
        CameraEffect.Shake(0.2f, 0.3f);
        if (IsDead)
        {
            Player.Instance.monstersKilled++;
            Instantiate(leafParticle, this.gameObject.transform.position + new Vector3(0.3f, 0.4f, -1f), Quaternion.identity);
			SoundManager.PlaySound ("green flower");
            GameManager.deadEnemies.Add(gameObject);
            gameObject.SetActive(false);
        }
        yield return null;
    }

    public void ThrowSeed()
    {
        if (this.gameObject.transform.localScale.x > 0)
        {
            GameObject tmp = (GameObject)Instantiate(seed, transform.position + new Vector3(0, 0.8f, -5), Quaternion.identity);
            tmp.GetComponent<Seed>().Initialize(Vector2.left);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(seed, transform.position + new Vector3(0, 0.8f, -5), Quaternion.Euler(0, 0, 180));
            tmp.GetComponent<Seed>().Initialize(Vector2.right);
        }
    }

    private void OnEnable()
    {
        Health = 1;
        Target = null;
        if (Health <= 0)
        {
            ChangeState(new EFGreenIdleState());
        }
        Physics2D.IgnoreCollision(enemySight.GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public void AnimIdle()
    {
        armature.animation.timeScale = 1.2f;
        armature.animation.Play("IDLE");

    }

    public void AnimAttack()
    {
        SoundManager.PlaySound("spit");
        armature.animation.timeScale = 1.5f;
        ThrowSeed();
        armature.animation.Play("ATTACK");
        acidFx.SetActive(true);
    }

    public void AnimPreparation()
    {
        armature.animation.timeScale = 2f;
        armature.animation.Play("PREPARATION");
    }
}
