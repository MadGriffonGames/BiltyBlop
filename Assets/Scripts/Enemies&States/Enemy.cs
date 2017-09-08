using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Enemy : MonoBehaviour
{
    public bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public UnityArmatureComponent armature;

    [SerializeField]
    protected GameObject[] healthbar;

    [SerializeField]
    protected int health;

    [SerializeField]
    public List<string> damageSources;

    [SerializeField]
    public int coinPackSize;

    [SerializeField]
    public GameObject coinPack;

    GameObject[] coins;

    public bool facingRight;//check direction(true if we look right)

    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public GameObject Target { get; set; }

    private void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
    }

    // Use this for initialization
    public virtual void Start()
    {

        if (coinPackSize == 0)
        {
            Debug.Log("WARNING: You don't assign size of coinPack in " + gameObject.name);
        }

        healthbar[Health - 1].SetActive(true);
        
        facingRight = false;
        enabled = false;
        armature.enabled = false;
        armature.armature.cacheFrameRate = 40;

    }

    public virtual IEnumerator TakeDamage()
    {
        yield return null;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }

    public void SetHealthbar()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i + 1 == health)
            {
                healthbar[i].SetActive(true);
            }
            else
            {
                healthbar[i].SetActive(false);
            }
        }
    }

    private void OnBecameVisible()
    {
        enabled = true;
        armature.enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
        armature.enabled = false;
    }

    public void ResetCoinPack()
    {
        coins = new GameObject[coinPackSize];
        if (coinPackSize != 0)
        {
            for (int i = 0; i < coinPack.transform.childCount; i++)
            {
                coins[i] = coinPack.transform.GetChild(i).gameObject;
            }
        }
    }

    public void ForceActivate()
    {
        enabled = true;
        armature.enabled = true;
    }

    public void SpawnCoins(int min, int max)
    {
        if (coinPackSize != 0)
        {
            int spawnCount = UnityEngine.Random.Range(min, max);
            spawnCount = coinPackSize > spawnCount ? spawnCount : coinPackSize;
            coinPackSize -= spawnCount;
            for (int i = 0; i < spawnCount; i++)
            {
                coins[i].SetActive(true);
                coins[i].GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2f, 2f), 3.5f);
                coins[i].transform.parent = null;
                coins[i].transform.localScale = new Vector2(0.78f, 0.78f);
            }
        }
    }

    public IEnumerator AnimationDelay()
    {
        float tmp = armature.animation.timeScale;

        armature.animation.timeScale = 0.1f;
        yield return new WaitForSeconds(0.3f);
        armature.animation.timeScale = tmp;
        yield return null;
    }
}
