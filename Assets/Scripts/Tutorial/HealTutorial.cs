using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTutorial : InAppTutorial
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("Level2") == 0)
        {
            Inventory.Instance.AddItem(Inventory.HEAL, 3);
        }
    }
}
