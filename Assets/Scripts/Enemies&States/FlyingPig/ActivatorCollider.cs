using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorCollider : MonoBehaviour
{
    [SerializeField]
    FlyingPig enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.isActive = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !enemy.isActive)
        {
            enemy.isActive = true;
        }
    }
}
