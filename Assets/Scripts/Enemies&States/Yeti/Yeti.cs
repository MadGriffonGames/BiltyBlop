using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Yeti : RangeEnemy
{
    bool damaged = false;

    private IYetiState currentState;
    [SerializeField]
    public Snowball snowball;
    [SerializeField]
    GameObject yetiParticle;
    [SerializeField]
    GameObject enemySight;
    [SerializeField]
    Vector2 power;



    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start ()
    {
        base.Start();
        ChangeState(new YetiIdleState());
    }
	
	void Update ()
    {
        if (!IsDead)
        {
            if (!TakingDamage && !Attack)
            {
                currentState.Execute();
            }
        }
    }

    public void ChangeState(IYetiState newState)
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

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), true);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!damaged)
        {
            health -= actualDamage;

            damaged = true;
            StartCoroutine(AnimationDelay());
            MakeFX.Instance.MakeHitFX(gameObject.transform.position + new Vector3(0, 0.3f), new Vector3(1, 1, 1));
            CameraEffect.Shake(0.2f, 0.3f);
            SetHealthbar();
            if (IsDead)
            {
                AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
                SoundManager.PlaySound("yeti_death");
                Instantiate(yetiParticle, gameObject.transform.position + new Vector3(0, 1f, -1f), Quaternion.identity);
                SpawnCoins(2, 4);
                GameManager.deadEnemies.Add(gameObject);
                gameObject.SetActive(false);
            }
            yield return null;
        }
        damaged = false;
    }

    public void ThrowSnowball()
    {
        Vector3 tmpVector = new Vector3(this.gameObject.transform.position.x - 1.2f, this.gameObject.transform.position.y + 3f, this.gameObject.transform.position.z);
        if (this.gameObject.transform.localScale.x > 0)
        {
            snowball.Throw(tmpVector, power);
        }
        else
        {
            snowball.Throw(tmpVector, (-1)*power);
        }
    }

    private void OnEnable()
    {
        ResetCoinPack();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        SetHealthbar();
        Target = null;
        if (Health <= 0)
        {
            ChangeState(new YetiIdleState());
            Health = 2;
            SetHealthbar();
        }
    }
}
