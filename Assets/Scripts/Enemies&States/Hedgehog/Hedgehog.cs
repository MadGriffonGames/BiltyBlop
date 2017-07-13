using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Hedgehog : MovingMeleeEnemy
{
    private IHedgehogState currentState;

    [SerializeField]
    GameObject spikeParticle;
    [SerializeField]
    GameObject healthBar;


    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new HedgehogIdleState());
        facingRight = true;
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
        CameraEffect.Shake(0.2f, 0.3f);
        MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
        SetHealthbar();
        if (IsDead)
        {
            Player.Instance.monstersKilled++;
            SoundManager.PlaySound("hedgehog_death");
            Instantiate(spikeParticle, gameObject.transform.position + new Vector3(0, 0.53f, -1f), Quaternion.identity);
            SpawnCoins(2, 4);
            GameManager.deadEnemies.Add(gameObject);
            gameObject.SetActive(false);

        }
        yield return null;
    }

    private void OnEnable()
    {
        ResetCoinPack();

        if (Health <= 0)
        {
            Health = 2;
        }
        ChangeState(new HedgehogIdleState());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        movementSpeed = 10;
        SetHealthbar();

    }

    public void StartIgnore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, true);
        for (int i = 0; i < Player.Instance.clipSize; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.throwingClip[i].GetComponent<Collider2D>(), true);
        }
    }

    public void StopIgnore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, false);
        for (int i = 0; i < Player.Instance.clipSize; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.throwingClip[i].GetComponent<Collider2D>(), false);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter2D(other);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), true);
        }
    }

    public void EnableHealthbar (int enable)
    {
        if (enable == 1) healthBar.SetActive(true);
        else healthBar.SetActive(false);
    } 

}
