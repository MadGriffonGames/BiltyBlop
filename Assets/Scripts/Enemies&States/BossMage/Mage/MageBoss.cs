using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class MageBoss : Boss
{
    private IMageBossState currentState;
    public IMageBossState lastState;
    Rigidbody2D MyRigidbody;
    [SerializeField]
    public FireballSpawner fireballSpawner;
    [SerializeField]
    public RectTransform healthbar;
    [SerializeField]
    public GameObject bossUI;
    [SerializeField]
    GameObject runeStone;
    [SerializeField]
    UnityEngine.Transform[] teleportPoints;
    [SerializeField]
    GameObject fireball0;
    [SerializeField]
    GameObject fireball1;
    int maxHealth;
    float firstHBScaleX;
    public bool isActive;
    int currentPoint;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
        maxHealth = Health;
        isActive = false;
        firstHBScaleX = healthbar.localScale.x;
    }

    public override void Start()
    {
        base.Start();
        currentPoint = 0;
        MyRigidbody = GetComponent<Rigidbody2D>();
        bossUI.SetActive(true);
        ChangeState(new MageIdleState());
    }

    void Update()
    {
        currentState.Execute();
        LookAtTarget();
    }

    public void ChangeState(IMageBossState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
            lastState = currentState;
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void ChangeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public override IEnumerator TakeDamage()
    {
        CameraEffect.Shake(0.2f, 0.1f);

        int dmg;

        if (damageSource == "Sword")
        {
            dmg = Player.Instance.meleeDamage;
        }
        else
        {
            dmg = Player.Instance.throwDamage;
        }

        for (int i = 1; i <= dmg; i++)
        {
            SetHealthbar();
        }

        health -= dmg;

        if (Health <= 0)
        {
            runeStone.SetActive(true);
        }
        yield return null;
    }

    public void SetHealthbar()
    {
        if (Health > 0)
        {
            healthbar.localScale = new Vector3(healthbar.localScale.x - firstHBScaleX * 1 / maxHealth,
                                               healthbar.localScale.y,
                                               healthbar.localScale.z);
        }
        else
        {
            bossUI.SetActive(false);
        }
    }

    public Vector3 GetTeleportPoint()
    {
        int rnd = UnityEngine.Random.Range(0, 6);
        if (rnd == currentPoint)
        {
            currentPoint = (rnd != 0 && rnd != 6) ? (rnd + 1) : 4;
        }
        else
        {
            currentPoint = rnd;
        }
        return teleportPoints[currentPoint].position;
    }

    public void Move()
    {
        int dir = transform.localScale.x > 0 ? 1 : -1;
        MyRigidbody.velocity = new Vector2(speed * dir, 0);
    }

    public void Stop()
    {
        MyRigidbody.velocity = Vector2.zero;
    }

    public void SpawnFireballs(bool reverse)
    {
        StartCoroutine(fireballSpawner.SpawnFireballs(reverse));
    }

    public void ThrowFireballs()
    {
        fireball0.SetActive(true);
        fireball1.SetActive(true);
    }

    public IMageBossState GetRandomState()
    {
        int rnd = UnityEngine.Random.Range(0, 3);
        switch (rnd)
        {
            case 0:
                return new MageAirAttackState();
            case 1:
                return new MageGroundAttackState();
            case 2:
                return new MageFireballAttackState();
            default:
                return new MageIdleState();
        }
    }

    public void LookAtTarget()
    {
        float xDir = Player.Instance.transform.position.x - transform.position.x;//xDir shows target from left or right side 
        if ((xDir < 0 && IsFacingRight()) || (xDir > 0 && !IsFacingRight()))
        {
            ChangeDirection();
        }
    }

    public Vector2 GetDirection()
    {
        return IsFacingRight() ? Vector2.right : Vector2.left;
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}
