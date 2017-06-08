using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;


    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name);
            StartCoroutine(Player.Instance.TakeDamage());
            Player.Instance.transform.position = spawnPoint.position;
        }
    }
}
