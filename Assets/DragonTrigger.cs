using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject dragon;

    private void Awake()
    {
        dragon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dragon.gameObject.SetActive(true);
        }
    }
}
