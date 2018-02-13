using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class EvilFlower : MeleeEnemy
{
    private IEvilFlowerState currentState;
    [SerializeField]
    GameObject slots;
    [SerializeField]
    private GameObject leafParticle;

    [SerializeField]
    GameObject enemySight;

    public bool isAttacked;
    public float timer;
    float attackCoolDown = 2;


    void Awake()
    {
		armature = GetComponent<UnityArmatureComponent> ();
        ResetCoinPack();
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new EvilFlowerIdleState());
		//SetHealthbar ();
        Physics2D.IgnoreCollision(enemySight.GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);

        isAttacked = false;
    }

    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage && !Attack)
            {
                currentState.Execute();
            }
            if (isAttacked)
            {
                timer += Time.deltaTime;
            }
            if (timer >= attackCoolDown)
            {
                timer = 0;
                isAttacked = false;
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
        
    }

    public override IEnumerator TakeDamage()
    {
        health -= actualDamage;
		SetHealthbar ();

        CameraEffect.Shake(0.2f, 0.3f);
        MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
        if (IsDead) 
        {
            AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
            Instantiate(leafParticle, this.gameObject.transform.position + new Vector3(0.5f, 1.8f, -3), Quaternion.identity);
            SpawnCoins(1, 2);
			SoundManager.PlaySound ("flower_death");
            GameManager.deadEnemies.Add(gameObject);
            gameObject.SetActive(false);
        }
        yield return null;
    }

    private void OnEnable()
    {
        if (Health <= 0)
        {
            ResetCoinPack();
			Health = maxHealth;
			SetHealthbar ();
        }

        Target = null;
        ChangeState(new EvilFlowerIdleState());
        attackCollider.enabled = false;
        Physics2D.IgnoreCollision(enemySight.GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }
}
