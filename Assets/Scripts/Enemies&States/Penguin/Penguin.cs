using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Penguin : MovingRangedEnemy
{
    private IPenguinState currentState;
    [SerializeField]
    private GameObject penguinParticle;
    public bool walk = false;
    public bool attack;
    bool damaged = false;
    [SerializeField]
    GameObject threezubets;


    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        attack = false;
    }

    public override void Start ()
    {
        base.Start();
        ChangeState(new PenguinPatrolState());
    }
	
	void Update ()
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

    public override IEnumerator TakeDamage()
    {
        if (!damaged)
        {
            health -= actualDamage;

            damaged = true;
            StartCoroutine(AnimationDelay());
            MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
            CameraEffect.Shake(0.2f, 0.3f);
            SetHealthbar();
            if (IsDead)
            {
                AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
                SoundManager.PlaySound("penguin_death");
                Instantiate(penguinParticle, gameObject.transform.position + new Vector3(0, 1f, -1f), Quaternion.identity);
                SpawnCoins(2, 4);
                GameManager.deadEnemies.Add(gameObject);
                gameObject.SetActive(false);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        damaged = false;
    }

    public void ChangeState(IPenguinState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
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

    public void StopMoving()
    {
        walk = false;
        transform.Translate(GetDirection() * 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter2D(other);
        if (other.gameObject.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), true);
        }
    }

    public void ThrowThreezubets()
    {
        if (this.gameObject.transform.localScale.x > 0)
        {
            GameObject tmp = (GameObject)Instantiate(threezubets, transform.position + new Vector3(0, 0.8f, -5), Quaternion.Euler(0, 0, 72));
            tmp.GetComponent<Threezubets>().Initialize(Vector2.left);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(threezubets, transform.position + new Vector3(0, 0.8f, -5), Quaternion.Euler(0, 0, -108));
            tmp.GetComponent<Threezubets>().Initialize(Vector2.right);
        }
    }

    private void OnEnable()
    {
        ResetCoinPack();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        Target = null;
        damaged = false;

        if (Health <= 0)
        {
            ChangeState(new PenguinPatrolState());
			Health = maxHealth;
			SetHealthbar();   
        }
    }
}
