using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class IceMage : RangeEnemy
{
    public bool isTakingDamage = false;
    public bool isDead = false;
    bool visible = false;
    bool isScaled = true;

    private IceMageState currentState;
    [SerializeField]
    public GameObject fireball;
    [SerializeField]
    GameObject leafParticle;
    [SerializeField]
    GameObject enemySight;
    [SerializeField]
    public GameObject deathPic;
    [SerializeField]
    GameObject crystall;


    Vector3 scaling = new Vector3(0.01f, 0.01f, 0);

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
    }


    public override void Start ()
    {
        deathPic.gameObject.SetActive(false);
        isTakingDamage = false;
        base.Start();
        ChangeState(new IceMageIdleState());
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

    private void FixedUpdate()
    {
        if (isTakingDamage)
            FireballFadeOut();
    }

    public void ChangeState(IceMageState newState)
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
        SetHealthbar();
        Target = null;
        isTakingDamage = true;
        MakeFX.Instance.MakeHitFX(gameObject.transform.position + new Vector3(0.5f, 2f, 0), new Vector3(0.8f, 0.8f, 1));
        gameObject.GetComponent<Collider2D>().enabled = false;
        health -= Player.Instance.damage;
        CameraEffect.Shake(0.4f, 0.5f);
        SpawnCoins(2, 3);
        if (IsDead)
        {
            AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
            Player.Instance.monstersKilled++;
            isDead = true;
            SoundManager.PlaySound("crystal_break");
            GameManager.deadEnemies.Add(gameObject);
            ChangeState(new IceMageDeathState());
        }
        yield return null;
    }

    private void OnEnable()
    {
        deathPic.gameObject.SetActive(false);
        gameObject.SetActive(true);
        isTakingDamage = false;
        ResetCoinPack();
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        Health = 1;
        Target = null;
        ChangeState(new IceMageIdleState());
        Physics2D.IgnoreCollision(enemySight.GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    void FireballFadeOut()
    {
        if (fireball.gameObject.activeInHierarchy)
        {
            fireball.gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.25f);
            if (isScaled)
            {
                fireball.gameObject.transform.localScale -= scaling;
            }
            if (fireball.gameObject.transform.localScale.x <= 0.05)
            {
                isScaled = false;
                fireball.gameObject.SetActive(false);
            }
        }
    }
}
