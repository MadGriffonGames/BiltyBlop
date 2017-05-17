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
    private int playerKills;


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
            if (Player.Instance.monstersKilled > playerKills)
            {
                if (!spawner.isEmpty())
                {
                    spawner.SpawnEnemy();
                    playerKills = Player.Instance.monstersKilled;
                }
                else if (Player.Instance.monstersKilled > playerKills+1) makeVictory();
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
               
            playerKills = Player.Instance.monstersKilled;
                spawner.SpawnEnemy();
                spawner.SpawnEnemy();
                isActivated = true;
                isBlocked = true;
        }
    }
    private void makeVictory()
    {
        LDoor.Activate();
        RDoor.Activate();
        fight.Activate();
        mushroom.Activate();
        ArenaRewards.SetActive(true);
        Collider2D [] colliders = ArenaRewards.GetComponentsInChildren<Collider2D>();
        foreach(Collider2D col in colliders)
        {
            if(!col.isTrigger)
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), col);
        }
        isActivated = false;
    }

}
