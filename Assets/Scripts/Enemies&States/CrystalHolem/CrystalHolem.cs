using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class CrystalHolem : MovingMeleeEnemy
{

    ICrystalState currentState;
    [SerializeField]
    GameObject crystalParticle;
    [SerializeField]
    public CrystalTrigger crystals;


    bool damaged = false;
    public bool walk = false;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        ResetCoinPack();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        isAttacking = false;
        ChangeState(new CrystalIdleState());
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

    public void ChangeState(ICrystalState newState)
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
        if (!damaged)
        {
            damaged = true;
            CameraEffect.Shake(0.2f, 0.3f);
            SetHealthbar();
            MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
            if (IsDead)
            {
                AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
                SoundManager.PlaySound("holem_sound");
                Instantiate(crystalParticle, gameObject.transform.position + new Vector3(0, 1f, -1f), Quaternion.identity);
                SpawnCoins(3, 5);
                GameManager.deadEnemies.Add(gameObject);
                gameObject.SetActive(false);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.02f);
        damaged = false;
    }

    private void OnEnable()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);

        Target = null;
        damaged = false;
        isAttacking = false;

        if (Health <= 0)
        {
            ResetCoinPack();

            crystals.Disable();
            ChangeState(new CrystalIdleState());
            Health = 3;
        }

        SetHealthbar();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter2D(other);
        if (other.gameObject.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), true);
        }
    }

    public void EnableAttackCollider()
    {
        StartCoroutine(AttackColliderDelay());
    }

    IEnumerator AttackColliderDelay()
    {
        yield return new WaitForSeconds(0.5f);
        AttackCollider.enabled = true;
    }

    public void LocalMove()
    {
        if (!walk)
        {
            walk = true;
            armature.animation.FadeIn("walk", -1, -1);
        }
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

}
