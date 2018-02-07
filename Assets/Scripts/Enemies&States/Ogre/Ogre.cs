using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class Ogre : MovingMeleeEnemy
{
    private IOgreState currentState;
    [SerializeField]
    private GameObject deathParticles;
    bool damaged = false;
    [HideInInspector]
    public bool walk = false;
    [HideInInspector]
    public bool canAttack;
    [HideInInspector]
    public bool isTimerTick;
    float timer;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        isAttacking = false;
        ChangeState(new OgrePatrolState());
        canAttack = true;
        isTimerTick = false;
        timer = 0;
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

            if (isTimerTick)
            {
                timer += Time.deltaTime;
            }
            if (timer >= 0.5f)
            {
                isTimerTick = false;
                timer = 0;
                canAttack = true;
            }
        }
    }

    public void ChangeState(IOgreState newState)
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
            health -= actualDamage;
            SoundManager.PlaySound("ogre_pain");
            damaged = true;
            StartCoroutine(OgreAnimationDelay());
            MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
            CameraEffect.Shake(0.2f, 0.3f);
            SetHealthbar();
            if (IsDead)
            {
                AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
                Instantiate(deathParticles, gameObject.transform.position + new Vector3(0, 1f, -1f), Quaternion.identity);
                SoundManager.PlaySound("ogre_death");
                SpawnCoins(4, 6);
                GameManager.deadEnemies.Add(gameObject);
                gameObject.SetActive(false);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        damaged = false;
    }

    private void OnEnable()
    {
        ResetCoinPack();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);

        Target = null;
        damaged = false;
        isAttacking = false;
        AttackCollider.enabled = false;

        if (Health <= 0)
        {
            ChangeState(new OgrePatrolState());
            Health = 5;
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), true);
        }
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

    IEnumerator OgreAnimationDelay()
    {
        float tmp = armature.animation.timeScale;

        armature.animation.timeScale = 0.6f;
        yield return new WaitForSeconds(0.1f);
        armature.animation.timeScale = tmp;
        yield return null;
    }
}
