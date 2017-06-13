using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Player.Instance.TakeDamage());
            Player.Instance.transform.position = spawnPoint.position;
        }
    }
}
