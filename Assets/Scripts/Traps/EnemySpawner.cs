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


    public bool isEmpty()
    {
        Enemy enemy = Enemies.GetComponentInChildren<Enemy>(true);
        return (enemy==null);
    }
    public void SpawnEnemy()
    {
        Enemy enemy = Enemies.GetComponentInChildren<Enemy>(true);
        enemy.gameObject.transform.parent.gameObject.SetActive(true);

        Enemy[] arEnem = SpawnedEnemies.GetComponentsInChildren<Enemy>();
        foreach(Enemy e in arEnem)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), e.GetComponent<Collider2D>());
        }

        enemy.gameObject.transform.parent.SetParent(SpawnedEnemies.transform);

        enemy.enabled = true;
        if (enemy.MyAniamtor != null) enemy.MyAniamtor.enabled = true;
        UnityArmatureComponent armature = enemy.gameObject.GetComponent<UnityArmatureComponent>();
        armature.enabled = true;
 
     
        
       
    }
}
