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
            SoundManager.PlaySound("water splash");
            StartCoroutine(Player.Instance.TakeDamage());
            Player.Instance.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, -4);
        }
    }
}
