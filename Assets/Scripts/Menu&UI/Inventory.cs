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
    public const string hpPots = "HealthPot";
    public const string damageBonuses = "DamageBonus";
    public const string speedBonuses = "SpeedBonus";
    public const string timeBonuses = "TimeBonus";
    public const string immortalBonuses = "ImmortalBonus";
    public const string clips = "Clips";

    public string[] items;

    const string max = "Max";
    const string count = "Count";
    const string costCoins = "CostCoins";
    const string costCrystal = "CostCrystal";

    private Dictionary<string, string> inGameItemNames;

    const int upgradingValue = 5;

    private void Start()
    {
        items =new string[] { hpPots, damageBonuses, speedBonuses, timeBonuses, immortalBonuses, clips };

        inGameItemNames = new Dictionary<string, string>(items.Length);

        inGameItemNames.Add(hpPots, "Heal Pot");
        inGameItemNames.Add(damageBonuses, "Damage Pot");
        inGameItemNames.Add(speedBonuses, "Speed Pot");
        inGameItemNames.Add(timeBonuses, "Time Pot");
        inGameItemNames.Add(immortalBonuses, "Immortal Pot");
        inGameItemNames.Add(clips, "Clips");


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

    public string GetInGameItemName(string itemName)
    {
        return inGameItemNames[itemName];
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
        if (!PlayerPrefs.HasKey(max + itemName + count))
        {
            PlayerPrefs.SetInt(max + itemName + count, maxCount);
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
