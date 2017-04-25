using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.Health -= 1;
            StartCoroutine(Player.Instance.TakeDamage());
            Player.Instance.transform.position = spawnPoint.position;
        }
    }
}
