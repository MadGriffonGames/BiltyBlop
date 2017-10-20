using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class ArenaController : MonoBehaviour {


	[SerializeField]
	GameObject[] enemies;
	[SerializeField]
	UnityEngine.Transform[] spawnTransforms;
	[SerializeField]
	UnityEngine.Transform[] spawnItemsTransforms;
	[SerializeField]
	GameObject[] items;
	[SerializeField]
	UnityEngine.Transform leftEdge;
	[SerializeField]
	UnityEngine.Transform rightEdge;

	int enemiesCount;
	int spawnsCount;
	int waveNumber;

	float timeToEnd;
	float waveTime;
	float bonusTime;
	float deleteBonusTime;

	List<GameObject> activeBonuses;
	GameObject[] activeEnemies;

	// Use this for initialization
	void Start () 
	{
		ThrowingUI.Instance.SetThrowBar ();
		enemiesCount = enemies.Length;
		spawnsCount = spawnTransforms.Length;
		StartCoroutine (SpawnEnemies ());
		timeToEnd = 3f;
		waveNumber = 1;
		waveTime = 4;
		bonusTime = 10;
		deleteBonusTime = 20f;
		activeBonuses = new List<GameObject>();
		//SpawnRandomEnemy(3);

	}

	IEnumerator SpawnEnemies()
	{
		yield return new WaitForSeconds (3);
		SpawnRandomEnemy (5);
	}
		
	void Update () 
	{
		timeToEnd -= Time.deltaTime;
		waveTime -= Time.deltaTime;
		bonusTime -= Time.deltaTime;
		deleteBonusTime -= Time.deltaTime;

		if (deleteBonusTime <= 0) 
		{
			//Destroy( activeBonuses.FindLast(GameObject));
			foreach (GameObject bonus in activeBonuses) 
			{
				activeBonuses.Remove (bonus);
				Destroy (bonus);
			}
			deleteBonusTime = 15f;
		}
		if (bonusTime<=0) 
		{
			bonusTime = Random.Range (8f, 15f);
			SpawnItem ();
		}
		if (waveTime <= 0) {
			//GenerateNewWave ();
		}
	}

	private void SpawnRandomEnemy(int count)
	{
		for (int i = 0; i < count; i++) 
		{
			int enemyNum = Random.Range (0, enemiesCount);
			int spawnNum = Random.Range (0, spawnsCount);
			GameObject newEnemy = Instantiate (enemies [enemyNum]) as GameObject;
			newEnemy.transform.SetParent (spawnTransforms [spawnNum]);
			newEnemy.transform.localPosition = new Vector3 (0, 0, 0);
			newEnemy.gameObject.GetComponentInChildren<UnityArmatureComponent> ().gameObject.AddComponent<ArenaEnemy>();
			GameManager.DestroyDeadEnemies ();

		}
	}

	private void GenerateNewWave()
	{
		Debug.Log ("NEW WAVE");
		waveNumber++;
		waveTime = Random.Range (10f, 15f);
		SpawnRandomEnemy (Random.Range(4,6));
	}

	private void SpawnItem()
	{
		int num = Random.Range (0, 5);
//		int itemOrderNum = 0;
//		switch (num) 
//		{
//		case (num<10):
//			{
//				itemOrderNum = 0;
//				break;
//			}
//		case (num<20):
//			{
//				itemOrderNum = 1;
//				break;
//			}
//		case (num<30):
//			{
//				itemOrderNum = 2;
//				break;
//			}
//		case (num<45):
//			{
//				itemOrderNum = 3;
//				break;
//			}
//		case (num<60):
//			{
//				itemOrderNum = 4;
//				break;
//			}
//		case (num<=100):
//			{
//				itemOrderNum = 5;
//				break;
//			}
//		default:
//			break;
//		}
		GameObject newItem = Instantiate (items [num]) as GameObject;
		newItem.transform.position = spawnItemsTransforms [Random.Range (0, spawnItemsTransforms.Length)].position;
		activeBonuses.Add (newItem);
		Debug.Log (activeBonuses.Count);
	}
}
