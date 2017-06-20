using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    /* Inventory Items Names */
    public static string hpPots = "HealthPotCount";
    public static string damageBonuses = "DamageBonusCount";
    public static string speedBonuses = "SpeedBonusCount";
    public static string timeBonuses = "TimeBonusCount";
    public static string immortalBonuses = "ImmortalBonusCount";
    public static string clips = "ClipsCount";

    private static int upgradingValue = 5;


    public bool AddItemToInventory(string itemName)
    {
        if (!PlayerPrefs.HasKey(itemName))
        {
            PlayerPrefs.SetInt(itemName, 1);
            return true;
        }
        else if (PlayerPrefs.GetInt(itemName) < PlayerPrefs.GetInt("Max" + itemName))
        {
            PlayerPrefs.SetInt(itemName, PlayerPrefs.GetInt(itemName) + 1);
            return true;
        }
        return false;
    }

    public bool RemoveItemToInventory(string itemName)
    {
        if (PlayerPrefs.GetInt(itemName) > 0)
        {
            PlayerPrefs.SetInt(itemName, PlayerPrefs.GetInt(itemName) - 1);
            return true;
        }
        return false;
    }

    public void UpgradeStorage(string storageName, int cost)
    {
        PlayerPrefs.SetInt(storageName, PlayerPrefs.GetInt(storageName) + upgradingValue);
        GameManager.CollectedCoins -= cost;
    }

}
