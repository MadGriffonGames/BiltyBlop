using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Bat : MovingMeleeEnemy
{
    IBatState currentState;
    public Vector3 nextPos;
    [SerializeField]
    public UnityEngine.Transform batTransform;
    [SerializeField]
    GameObject batParticles;
    [SerializeField]
    public UnityEngine.Transform[] pathPoints;
    public Vector3[] pathCordinates;
    public int nextPosNum = 0;
    List<Slot> slots;
    
    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
        ResetCoinPack();
    }

    public override void Start()
    {
        base.Start();
        slots = armature.armature.GetSlots();
        SetIndexes();
        pathCordinates = new Vector3[pathPoints.Length];
        int i = 0;
        foreach (var point in pathPoints)
        {
            pathCordinates[i] = pathPoints[i].localPosition;
            i++;
        }
        ChangeState(new BatPatrolState());
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

    public void ChangeState(IBatState newState)
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
        health -= actualDamage;
		SetHealthbar();
        CameraEffect.Shake(0.2f, 0.3f);
        MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(0.7f, 0.7f, 1));
        if (IsDead)
        {
            AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.mobKiller);
            Player.Instance.monstersKilled++;
            SoundManager.PlaySound("bat_death");
            Instantiate(batParticles, gameObject.transform.position + new Vector3(0, 0.53f, -1f), Quaternion.identity);
            SpawnCoins(1, 2);
            GameManager.deadEnemies.Add(gameObject);
            gameObject.SetActive(false);

        }
        yield return null;
    }

    private void OnEnable()
    {
        
        if (health <= 0)
        {
            ResetCoinPack();
			Health = maxHealth;
			SetHealthbar();
        }
    }

    public void SetIndexes()
    {
        int tmp = health > 1 ? 1 : 0;
        foreach (Slot slot in slots)
        {
            slot.displayIndex = tmp;
            slot.displayController = "none";
        }
    }
}
