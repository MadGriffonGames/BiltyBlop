using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    [SerializeField]
    GameObject crashedEgg;
    [SerializeField]
    GameObject eggParticle;
    [SerializeField]
    public GameObject coinPack;
    [SerializeField]
    public int coinPackSize;

    GameObject[] coins;

    // Use this for initialization
    void Start () {
        ResetCoinPack();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            gameObject.SetActive(false);
            Instantiate(eggParticle, this.gameObject.transform.position + new Vector3(0, 1f), Quaternion.identity);
            crashedEgg.gameObject.SetActive(true);
            SoundManager.PlaySound("egg_crack");
            SpawnCoins(1, 2);
        }
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
}
