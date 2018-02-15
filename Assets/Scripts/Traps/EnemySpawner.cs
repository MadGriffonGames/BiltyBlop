using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;



class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemies;
    [SerializeField]
    private GameObject SpawnedEnemies;

    private void Start()
    {
        Enemy[] ens = Enemies.GetComponentsInChildren<Enemy>(true);
        foreach(Enemy e in ens)
        {
            e.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public bool isEmpty()
    {
        Enemy enemy = Enemies.GetComponentInChildren<Enemy>(true);
        return (enemy==null);
    }
    public int getAliveEnemiesCount()
    {
        Enemy[] se = SpawnedEnemies.GetComponentsInChildren<Enemy>();
        return se.Length;
    }
    public IEnumerator SpawnEnemy(bool isDelayNeeded)
    {
        Enemy enemy = Enemies.GetComponentInChildren<Enemy>(true);
        enemy.gameObject.transform.parent.gameObject.SetActive(true);
        enemy.gameObject.transform.parent.SetParent(SpawnedEnemies.transform);
        Enemy[] arEnem = SpawnedEnemies.GetComponentsInChildren<Enemy>();
        foreach(Enemy e in arEnem)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), e.GetComponent<Collider2D>());
        }
        if(isDelayNeeded) yield return new WaitForSeconds(1.5f);
        enemy.ForceActivate();       
    }
}
