using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTutorial : InAppTutorial
{
    [SerializeField]
    GameObject maneken;
    [SerializeField]
    GameObject tutorialTrigger;

    private void Start()
    {
        //if (PlayerPrefs.GetInt("Level2") != 0)
        //{
        //    maneken.SetActive(false);
        //    gameObject.SetActive(false);
        //    tutorialTrigger.SetActive(false);
        //}
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("Level2") == 0)
        {
            Inventory.Instance.AddItem(Inventory.AMMO, 3);
        }
    }
}
