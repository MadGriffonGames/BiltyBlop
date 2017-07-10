using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class IceMage : RangeEnemy {


    public bool isTakingDamage = false;
    public bool isDead = false;

    private IceMageState currentState;
    [SerializeField]
    public GameObject fireball;
    [SerializeField]
    GameObject leafParticle;
    [SerializeField]
    GameObject enemySight;

    [SerializeField]
    GameObject crystall;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
    }


    public override void Start () {
        base.Start();
        ChangeState(new IceMageIdleState());
        Physics2D.IgnoreCollision(enemySight.GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);

    }

    // Update is called once per frame
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
        isTakingDamage = true;
        //this.armature.animation.timeScale = 2f;
        //this.armature.animation.FadeIn("death", 1, 1);
        gameObject.GetComponent<Collider2D>().enabled = false;
        //health -= Player.Instance.damage;
        CameraEffect.Shake(0.4f, 0.5f);
        if (IsDead)
        {
            Player.Instance.monstersKilled++;
            isDead = true;
            //Instantiate(leafParticle, this.gameObject.transform.position + new Vector3(0.3f, 0.4f, -1f), Quaternion.identity);
            SpawnCoins(1, 2);
            SoundManager.PlaySound("green flower");
            GameManager.deadEnemies.Add(gameObject);
            gameObject.SetActive(false);
        }
        yield return null;
    }

    private void OnEnable()
    {
        ResetCoinPack();

        Health = 1;
        Target = null;
        ChangeState(new IceMageIdleState());
        Physics2D.IgnoreCollision(enemySight.GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }
}
