using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HealTutorial : InAppTutorial
{
    [SerializeField]
    GameObject hpPot;
    [SerializeField]
    GameObject bonusLight;

    bool isActivated;
    bool isCollected;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Level2") != 0 && !GameManager.developmentBuild)
        {
            gameObject.SetActive(false);
        }
        isActivated = false;
        isCollected = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        EnableControls(false);
        Player.Instance.mobileInput = 0;
        inventoryFade.SetActive(true);

        string currentLevel = SceneManager.GetActiveScene().name;
        if (other.gameObject.CompareTag("Player") && currentLevel == "Level1" && (PlayerPrefs.GetInt("Level2") == 0 || GameManager.developmentBuild))
        {
            Player.Instance.Health -= 1;
            HealthUI.Instance.SetHealthbar();
            int currentCount = Inventory.Instance.GetItemCount(Inventory.HEAL);
            currentCount = currentCount > 3 ? 3 : currentCount;
            Inventory.Instance.AddItem(Inventory.HEAL, 3 - currentCount);
            DevToDev.Analytics.Tutorial(1);
        }
    }
}
