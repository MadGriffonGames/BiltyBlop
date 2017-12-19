using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    public GameObject typlak;
    [SerializeField]
    public GameObject shootFlower;
    [SerializeField]
    public GameObject flower;
    [SerializeField]
    public UnityEngine.Transform flowerPos1;
    [SerializeField]
    public UnityEngine.Transform flowerPos2;
    [SerializeField]
    public UnityEngine.Transform typlakPos1;
    [SerializeField]
    public UnityEngine.Transform typlakPos2;
    [SerializeField]
    public GameObject puf1;
    [SerializeField]
    public GameObject puf2;

    GameObject spawnedEnemy1;
    GameObject spawnedEnemy2;

    public void InstantiateEnemies()
    {
        GameObject enemy1 = null;
        GameObject enemy2 = null;
        Transform pos1 = null;
        Transform pos2 = null;

        enemy1 = shootFlower;
        enemy2 = shootFlower;
        pos1 = flowerPos1;
        pos2 = flowerPos2;

        puf1.SetActive(true);
        puf2.SetActive(true);

        spawnedEnemy1 = Instantiate(enemy1, pos1.position, Quaternion.identity);
        spawnedEnemy2 = Instantiate(enemy2, pos2.position, Quaternion.identity);

        Vector3 tmp = spawnedEnemy1.transform.localScale;
        tmp.x *= -1;
        spawnedEnemy1.transform.localScale = tmp;
    }

    public void DestroyEnemies()
    {
        if (spawnedEnemy1 != null || spawnedEnemy2 != null)
        {
            if (spawnedEnemy1.activeInHierarchy)
            {
                puf1.SetActive(true);
            }
            if (spawnedEnemy2.activeInHierarchy)
            {
                puf2.SetActive(true);
            }

            Destroy(spawnedEnemy1);
            Destroy(spawnedEnemy2);
        }
    }
}
