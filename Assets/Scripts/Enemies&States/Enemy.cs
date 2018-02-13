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
    protected int health;

    public List<MeshRenderer> enemyParts;

    [SerializeField]
	public GameObject healthBarNew;

	[SerializeField]
	public UnityEngine.Transform healthbar;

    [SerializeField]
    public List<string> damageSources;

    public int actualDamage = 0;

    [SerializeField]
    public int coinPackSize;

    [SerializeField]
    public GameObject coinPack;

	[SerializeField]
	public int maxHealth;

    [SerializeField]
    bool boss;

    GameObject[] coins;

    public bool facingRight;//check direction(true if we look right)

    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public GameObject Target { get; set; }

	public float firstHBScaleX;

    protected bool isSkeleton = false;

    private void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
    }

    // Use this for initialization
    public virtual void Start()
    {
		health = maxHealth;
		firstHBScaleX = healthbar.localScale.x;
        if (gameObject.transform.localScale.x < 0) 
		{
			ChangeHealtBarDirection ();
		}
        if (coinPackSize == 0)
        {
            Debug.Log("WARNING: You don't assign size of coinPack in " + gameObject.name);
        }
		SetHealthbar ();
        
        facingRight = false;
        enabled = false;
        armature.enabled = false;
        if (!boss)
        {
            armature.armature.cacheFrameRate = 30;
        }
       

    }

    public virtual IEnumerator TakeDamage()
    {
        yield return null;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            CheckDamageSource(other.tag);
            StartCoroutine(TakeDamage());
            if (!isSkeleton)
            {
                IndicateDamage();
            }
        }
    }

    public void SetHealthbar()
    {
        if (health < 0)
        {
            health = 0;
        }
        healthBarNew.GetComponentInChildren<TextMesh> ().text = health.ToString () + "/" + maxHealth.ToString ();

		if (health > 0)
		{
			healthbar.localScale = new Vector3(firstHBScaleX * health / maxHealth,
				                               healthbar.localScale.y,
				                               healthbar.localScale.z);
		}
        else
        {
            healthbar.localScale = new Vector3(firstHBScaleX * 0,
                                               healthbar.localScale.y,
                                               healthbar.localScale.z);
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

        armature.animation.timeScale = 0.5f;
        yield return new WaitForSeconds(0.1f);
        armature.animation.timeScale = tmp;
        yield return null;
    }

    public void CheckDamageSource(string damageSourceName)
    {
        if (damageSourceName == "Sword")
        {
            actualDamage = Player.Instance.meleeDamage;
        }
        else
        {
            actualDamage = Player.Instance.throwDamage;
        }
    }
	public void ChangeHealtBarDirection()
	{
		GameObject hpText = healthBarNew.gameObject.GetComponentInChildren<TextMesh> ().gameObject;
		hpText.transform.localScale = new Vector3 (hpText.transform.localScale.x *-1 ,hpText.transform.localScale.y, hpText.transform.localScale.z);
		//healthBarNew.transform.localScale = new Vector3(healthBarNew.transform.localScale.x * -1, healthBarNew.transform.localScale.y, healthBarNew.transform.localScale.z);
	}

    protected void IndicateDamage()
    {
        if (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < enemyParts.Count; i++)
            {
                StartCoroutine(TurnToRed(enemyParts[i]));
            }
        }
    }

    IEnumerator TurnToRed(MeshRenderer part)
    {
        part.material.SetColor("_Color", new Color(1, 0.3f, 0.3f, 1));
        yield return new WaitForSeconds(0.1f);
        part.material.SetColor("_Color", new Color(1, 1, 1, 1));
    }

    [ContextMenu("SetMeshRenderer")]
    void SetMeshRenderer()
    {
        MeshRenderer[] tmp;
        tmp = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i].gameObject.transform.parent.name == "Slots")
            {
                enemyParts.Add(tmp[i]);
            }
        }
        Debug.Log(enemyParts.Count);
    }
}
