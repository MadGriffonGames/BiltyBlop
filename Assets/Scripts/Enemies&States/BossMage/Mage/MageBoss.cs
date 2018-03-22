using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class MageBoss : Boss
{
    [HideInInspector]
    public IMageBossState currentState;
    [HideInInspector]
    public IMageBossState lastState;
    [HideInInspector]
    public IMageBossState lastAttackState;
    Rigidbody2D MyRigidbody;
    [SerializeField]
    public FireballSpawner fireballSpawner;
    [SerializeField]
    public RectTransform healthbar;
    [SerializeField]
    public GameObject runeStone;
    [SerializeField]
    UnityEngine.Transform[] teleportPoints;
    [SerializeField]
    GameObject fireball0;
    [SerializeField]
    public Collider2D damageCollider;
    [SerializeField]
    public Collider2D mageCollider;
    [SerializeField]
    GameObject bossUi;
    [SerializeField]
    Collider2D platformColliderToIgnore;
    [SerializeField]
    GameObject[] spikes;
    int maxHealth;
    float firstHBScaleX;
    public bool isActive;
    public int currentPoint;
    

    void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platformColliderToIgnore, true);
        maxHealth = Health;
        isActive = false;
        firstHBScaleX = healthbar.localScale.x;
    }

    public override void Start()
    {
        base.Start();
        currentPoint = 0;
        
        ChangeState(new MageIdleState());
    }

    void Update()
    {
        currentState.Execute();
        if (currentState.GetType() != new MageTeleportState().GetType() && isActive)
        {
            LookAtTarget();
        }
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
        CameraEffect.Shake(0.4f, 0.1f);
        MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
        SoundManager.PlaySound("mage_boss_pain");
        int dmg = damageSource == "Sword" ? Player.Instance.meleeDamage : dmg = Player.Instance.throwDamage;

        for (int i = 1; i <= dmg; i++)
        {
            SetHealthbar();
        }

        if (Health <= 0)
        {
            ChangeState(new MageDeathState());
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
            bossUi.SetActive(false);
        }
    }

    public Vector3 GetTeleportPoint()
    {
        int rnd = GetRandomPoint();
        currentPoint = rnd;
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

    public void SpawnFireballs()
    {
        StartCoroutine(fireballSpawner.SpawnFireballs());
    }

    public void ThrowFireballs()
    {
        fireball0.SetActive(true);
        SoundManager.PlaySound("shaman_fire");
    }

    public IMageBossState GetRandomAttackState()
    {
        int rnd = UnityEngine.Random.Range(0, 3);
        switch (rnd)
        {
            case 0:
                return GenerateState(new MageAirAttackState());
            case 1:
                return GenerateState(new MageGroundAttackState());
            case 2:
                return GenerateState(new MageFireballAttackState());
            default:
                return GenerateState(new MageIdleState());
        }
    }

    IMageBossState GenerateState(IMageBossState _state)
    {
        if (lastAttackState != null)
        {
            if (lastAttackState.GetType() == _state.GetType())
            {
                return GetRandomAttackState();
            }
            else
            {
                return _state;
            }
        }
        else
        {
            return new MageAirAttackState();
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

    public void WakeUpMage()
    {
        StartCoroutine(WakeUp());
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(4);
        isActive = true;
        bossUi.SetActive(true);
    }

    public void EnableSpikes()
    {

        int rnd = GetRandomPoint();
        for (int i = 0; i < spikes.Length; i++)
        {
            if (i != currentPoint && i != rnd)
            {
                spikes[i].SetActive(true);
            }
        }
    }

    int GetRandomPoint()
    {
        int rnd = Random.Range(0, 7);
        if (rnd != currentPoint)
        {
            return rnd;
        }
        else
        {
            return GetRandomPoint();
        }
    }
}
