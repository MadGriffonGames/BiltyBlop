using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractiveObject
{
    [SerializeField]
    private Coin coin;

    private bool isEmpty;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        isEmpty = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEmpty && other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
            isEmpty = true;
            for (int i = 0; i < 10; i++)
                Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
