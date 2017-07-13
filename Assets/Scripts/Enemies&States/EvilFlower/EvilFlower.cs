using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EvilFlower : MeleeEnemy
{
    private IEvilFlowerState currentState;
    [SerializeField]
    private GameObject leafParticle;

    [SerializeField]
    GameObject enemySight;


    void Awake()
    {
		armature = GetComponent<UnityArmatureComponent> ();
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new EvilFlowerIdleState());
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
        ChangeState(new EvilFlowerIdleState());
    }

    public override IEnumerator TakeDamage()
    {
        health -= Player.Instance.damage;
        CameraEffect.Shake(0.2f, 0.3f);
        MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
        if (IsDead) 
        {
            Instantiate(leafParticle, this.gameObject.transform.position + new Vector3(-0.4f, 0, -3), Quaternion.identity);
            SpawnCoins(1, 2);
			SoundManager.PlaySound ("flower_death");
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
        ChangeState(new EvilFlowerIdleState());
        attackCollider.enabled = false;
        Physics2D.IgnoreCollision(enemySight.GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }
}
