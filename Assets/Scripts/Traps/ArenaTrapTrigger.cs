using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ArenaTrapTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject leftDoor;
    [SerializeField]
    private GameObject rightDoor;
    [SerializeField]
    private GameObject fightSign;
    [SerializeField]
    private GameObject SecretMushroom;
    [SerializeField]
    private GameObject enemySpawner;
    [SerializeField]
    private GameObject ArenaRewards;



    private EnemySpawner spawner;
    private DoorOpen LDoor;
    private DoorOpen RDoor;
    private DoorOpen fight;
    private DoorOpen mushroom;

    private bool isActivated;
    private bool isBlocked;

    private int MaxEnemies = 2; 


    private void Start()
    {
        LDoor = leftDoor.GetComponentInChildren<DoorOpen>();
        RDoor = rightDoor.GetComponentInChildren<DoorOpen>();
        fight = fightSign.GetComponentInChildren<DoorOpen>();
        mushroom = SecretMushroom.GetComponentInChildren<DoorOpen>();
        spawner = enemySpawner.GetComponent<EnemySpawner>();
    }

    private void Update()
    {
        if (isActivated)
        {
            int enemy_count = spawner.getAliveEnemiesCount();
            if (enemy_count < MaxEnemies)
            {
                if (!spawner.isEmpty())
                    StartCoroutine(spawner.SpawnEnemy(true));
                else if(enemy_count == 0) makeVictory();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isBlocked)
        if (other.gameObject.CompareTag("Player"))
        {
            LDoor.Activate();
            RDoor.Activate();
            fight.Activate();
            mushroom.Activate();
                isActivated = true;
                isBlocked = true;

                for(int i=0; i<MaxEnemies;i++)
                {
                    StartCoroutine(spawner.SpawnEnemy(false));
                }
        }
    }
    private void makeVictory()
    {
        LDoor.Activate();
        RDoor.Activate();
        fight.Activate();
        mushroom.Activate();
        ArenaRewards.SetActive(true);
        Collider2D [] colliders = ArenaRewards.GetComponentsInChildren<Collider2D>(true);
        foreach(Collider2D col in colliders)
        {
            if (!col.isTrigger)
            {
                Physics2D.IgnoreCollision(Player.Instance.gameObject.GetComponent<BoxCollider2D>(), col);
                Physics2D.IgnoreCollision(Player.Instance.gameObject.GetComponent<CapsuleCollider2D>(), col);
            }
        }
        isActivated = false;
    }

}
