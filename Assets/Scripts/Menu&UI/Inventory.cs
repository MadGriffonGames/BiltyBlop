using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private static Inventory instance;
    public static Inventory Instance
    {
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<Inventory> ();
			return instance;
		}
	}

    /* Inventory Items Names */
    public static string hpPots = "HealthPot";
    public static string damageBonuses = "DamageBonus";
    public static string speedBonuses = "SpeedBonus";
    public static string timeBonuses = "TimeBonus";
    public static string immortalBonuses = "ImmortalBonus";
    public static string clips = "ClipsCount";

    public string[] items;

    const string max = "Max";
    const string count = "Count";
    const string costCoins = "CostCoins";
    const string costCrystal = "CostCrystal";

    const int upgradingValue = 5;

    private void Start()
    {
        items =new string[] { hpPots, damageBonuses, speedBonuses, timeBonuses, immortalBonuses, clips };

        /* SETTING STARTING PARAMS FOR ITEMS */
        SetStartingParamsForItem(hpPots, 10, 25, 2);
        SetStartingParamsForItem(damageBonuses, 5, 50, 4);
        SetStartingParamsForItem(speedBonuses, 5, 50, 4);
        SetStartingParamsForItem(timeBonuses, 5, 50, 4);
        SetStartingParamsForItem(immortalBonuses, 5, 50, 4);
        SetStartingParamsForItem(clips, 5, 50, 4);
    }

    public bool BuyItem(string itemName, int itemCount, bool coinPayment)
    {
        if (CanAddItemToInventory(itemName, itemCount))
        {
            /* 
             
                 ADD PAYMENT HERE 
             
                                     */

            AddItemToInventory(itemName, itemCount);
            return true;
        }
        else return false;
    }

    private bool CanAddItemToInventory(string itemName, int itemCount)
    {
        if (!PlayerPrefs.HasKey(itemName + count))
        {
            PlayerPrefs.SetInt(itemName + count, 0);
            return true;
        }
        else if (PlayerPrefs.GetInt(itemName + count) + itemCount < PlayerPrefs.GetInt(max + itemName))
        {
            return true;
        }
        return false;
    }

    public bool AddItemToInventory(string itemName, int itemCount) 
    {
        if (!PlayerPrefs.HasKey(itemName + count))
        {
            PlayerPrefs.SetInt(itemName + count, itemCount);
            return true;
        }
        else if (PlayerPrefs.GetInt(itemName + count) < PlayerPrefs.GetInt(max + itemName))
        {
            PlayerPrefs.SetInt(itemName + count, PlayerPrefs.GetInt(itemName + count) + itemCount);
            return true;
        }
        return false;
    }

    public bool RemoveItemFromInventory(string itemName)  // = UseItem
    {
        if (PlayerPrefs.GetInt(itemName + count) > 0)
        {
            PlayerPrefs.SetInt(itemName + count, PlayerPrefs.GetInt(itemName + count) - 1);
            return true;
        }
        return false;
    }

    public void UpgradeStorage(string itemName, int cost)
    {
        PlayerPrefs.SetInt(max + itemName + count, PlayerPrefs.GetInt(max + itemName + count) + upgradingValue);
        GameManager.CollectedCoins -= cost;
    }

    public int GetCoinCostOfItem(string itemName)
    {
        return PlayerPrefs.GetInt(itemName + costCoins);
    }

    public int GetCrystalCostOfItem(string itemName)
    {
        return PlayerPrefs.GetInt(itemName + costCrystal);
    }

    /* SETTING PLAYER PREFS METHODS */
    private void SetStartingParamsForItem(string itemName, int maxCount, int coinCost, int crystalCost)
    {
        // Start Count = 0
        if (!PlayerPrefs.HasKey(itemName + count))
        {
            PlayerPrefs.SetInt(itemName + count, 0);
        }
        // Max Count
        if (!PlayerPrefs.HasKey(max + itemName))
        {
            PlayerPrefs.SetInt(max + itemName, maxCount);
        }
        // Cost in Coins
        if (!PlayerPrefs.HasKey(itemName + costCoins))
        {
            PlayerPrefs.SetInt(itemName + costCoins, coinCost);
        }
        // Cost in Crystals
        if (!PlayerPrefs.HasKey(itemName + costCrystal))
        {
            PlayerPrefs.SetInt(itemName + costCrystal, crystalCost);
        }
    }

}
