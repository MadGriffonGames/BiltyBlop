using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
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

    const int IMMORTAL_DURATION = 5;
    const int DAMAGE_DURATION = 10;
    const int SPEED_DURATION = 3;
    const int TIME_DURATION = 7;

    /* Inventory Items Names */
    public const string HEAL = "HealthPot";
    public const string DAMAGE_BONUS = "DamageBonus";
    public const string SPEED_BONUS = "SpeedBonus";
    public const string TIME_BONUS = "TimeBonus";
    public const string IMMORTAL_BONUS = "ImmortalBonus";
    public const string AMMO = "ClipsCount";

    public const string MAX = "Max";
    public const string COUNT = "Count";
    public const string COST_COINS = "CostCoins";
    public const string COST_CRYSTALS = "CostCrystal";

    public Dictionary<string, int> boosters;
    public string[] items;

    private void Awake()
    {
        boosters = new Dictionary<string, int>();
        items = new string[] { HEAL, DAMAGE_BONUS, SPEED_BONUS, TIME_BONUS, IMMORTAL_BONUS, AMMO };

        SetStartingParamsForItem(HEAL, 3, 25, 2);
        SetStartingParamsForItem(DAMAGE_BONUS, 3, 50, 4);
        SetStartingParamsForItem(SPEED_BONUS, 3, 50, 4);
        SetStartingParamsForItem(TIME_BONUS, 3, 50, 4);
        SetStartingParamsForItem(IMMORTAL_BONUS, 3, 50, 4);
        SetStartingParamsForItem(AMMO, 3, 50, 4);
    }

    public bool BuyItem(string itemName, int itemCount, bool coinPayment)
    {
        if (CanAddItem(itemName, itemCount))
        {
            /* 
             
                 ADD PAYMENT HERE 
             
                                     */

            AddItem(itemName, itemCount);
            return true;
        }
        else return false;
    }

    public bool CanAddItem(string itemName, int itemCount)
    {
        if (!PlayerPrefs.HasKey(itemName + COUNT))
        {
            PlayerPrefs.SetInt(itemName + COUNT, 0);
            return true;
        }
        else if (PlayerPrefs.GetInt(itemName + COUNT) + itemCount < PlayerPrefs.GetInt(MAX + itemName))
        {
            return true;
        }
        return false;
    }

    public void AddItem(string itemName, int itemCount) 
    {
        if (!PlayerPrefs.HasKey(itemName + COUNT))
        {
            PlayerPrefs.SetInt(itemName + COUNT, itemCount);
            UpdateItemValue(itemName);
        }
        else if (PlayerPrefs.GetInt(itemName + COUNT) + itemCount <= PlayerPrefs.GetInt(MAX + itemName))
        {
            PlayerPrefs.SetInt(itemName + COUNT, PlayerPrefs.GetInt(itemName + COUNT) + itemCount);
            UpdateItemValue(itemName);
        }
    }

    public void RemoveItem(string itemName)
    {
        if (PlayerPrefs.GetInt(itemName + COUNT) > 0)
        {
            PlayerPrefs.SetInt(itemName + COUNT, PlayerPrefs.GetInt(itemName + COUNT) - 1);
            UpdateItemValue(itemName);
        }
    }

    public int GetCoinCost(string itemName)
    {
        return PlayerPrefs.GetInt(itemName + COST_COINS);
    }

    public int GetCrystalCost(string itemName)
    {
        return PlayerPrefs.GetInt(itemName + COST_CRYSTALS);
    }

    public int GetItemCount(string itemName)
    {
        return boosters[itemName];
    }

    public void UpdateItemValue(string itemName)
    {
        boosters[itemName] = PlayerPrefs.GetInt(itemName + COUNT);
    }

    /* SETTING PLAYER PREFS METHODS */
    private void SetStartingParamsForItem(string itemName, int maxCount, int coinCost, int crystalCost)
    {
        // Start Count = 0
        if (!PlayerPrefs.HasKey(itemName + COUNT))
        {
            PlayerPrefs.SetInt(itemName + COUNT, 0);
        }
        // Max Count
        if (!PlayerPrefs.HasKey(MAX + itemName))
        {
            PlayerPrefs.SetInt(MAX + itemName, maxCount);
        }
        // Cost in Coins
        if (!PlayerPrefs.HasKey(itemName + COST_COINS))
        {
            PlayerPrefs.SetInt(itemName + COST_COINS, coinCost);
        }
        // Cost in Crystals
        if (!PlayerPrefs.HasKey(itemName + COST_CRYSTALS))
        {
            PlayerPrefs.SetInt(itemName + COST_CRYSTALS, crystalCost);
        }

        boosters.Add(itemName, PlayerPrefs.GetInt(itemName + COUNT));
    }

    public void UseHP()
    {
        Player.Instance.Health++;
        HealthUI.Instance.SetHealthbar();
        MakeFX.Instance.MakeHeal();

        RemoveItem(HEAL);
    }

    public void UseAmmo()
    {
        Player.Instance.throwingIterator = Player.Instance.clipSize - 1;
        Player.Instance.ResetThrowing();

        RemoveItem(AMMO);
    }

    public void UseBonus(string bonusType)
    {
        if (bonusType == IMMORTAL_BONUS)
        {
            Player.Instance.ExecBonusImmortal(IMMORTAL_DURATION);
            RemoveItem(IMMORTAL_BONUS);
        }
        if (bonusType == DAMAGE_BONUS)
        {
            Player.Instance.ExecBonusDamage(DAMAGE_DURATION);
            RemoveItem(DAMAGE_BONUS);
        }
        if (bonusType == TIME_BONUS)
        {
            Player.Instance.ExecBonusTime(TIME_DURATION);
            RemoveItem(TIME_BONUS);
        }
        if (bonusType == SPEED_BONUS)
        {
            Player.Instance.ExecBonusSpeed(SPEED_DURATION);
            RemoveItem(SPEED_BONUS);
        }
    }

}
