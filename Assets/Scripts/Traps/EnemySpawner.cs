using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemies;

    private Enemy[] arr;

    private void Start()
    {
        arr = Enemies.GetComponentsInChildren<Enemy>();
    }
}
