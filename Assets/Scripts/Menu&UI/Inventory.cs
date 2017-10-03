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


    const int IMMORTAL_DURATION = 7;
    const int DAMAGE_DURATION = 10;
    const int SPEED_DURATION = 5;
    const int TIME_DURATION = 5;


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
    public const string SHOP_NAME = "ShopName";

    public Dictionary<string, int> boostersCount; // contains current Boosters Count
    private Dictionary<string, string> shopNames; // contains Actual Shop Names for item cards

    public string[] itemsNames;     // to create Item Shop Spacer with item Cards (indexation dependings)

    private void Awake()
    {
        shopNames = new Dictionary<string, string>(itemsNames.Length);
        boostersCount = new Dictionary<string, int>();

        // ADDING ITEMS // 
        itemsNames = new string[] { HEAL, DAMAGE_BONUS, SPEED_BONUS, TIME_BONUS, IMMORTAL_BONUS, AMMO }; // ADD NEW GOOD TO THE SHOP

        SetStartingParamsForItem(HEAL, "Heal Pot", 3, 180, 4);
        SetStartingParamsForItem(DAMAGE_BONUS, "Damage Pot", 3, 270, 6);
        SetStartingParamsForItem(SPEED_BONUS, "Speed Pot", 3, 135, 3);
        SetStartingParamsForItem(TIME_BONUS, "Time Pot", 3, 225, 5);
        SetStartingParamsForItem(IMMORTAL_BONUS, "Immortal Pot", 3, 360, 8);
        SetStartingParamsForItem(AMMO, "Ammo", 3, 120, 2);
        /*
        SetStartingParamsForItem(ITEMCONST, "Shop name", 4,4,5);
         */

    }

    public void BuyItem(string itemName, int itemCount, string moneyType, int price)
    {
            if (moneyType == "Coins")
            {
                if (PlayerPrefs.GetInt("Coins") >= price)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - price);
                    AddItem(itemName, itemCount);

                    AppMetrica.Instance.ReportEvent("#BONUS_BOUGHT " + itemName + " bought for " + moneyType);
                }
            }
            if (moneyType == "Crystals")
            {
                if (PlayerPrefs.GetInt("Crystals") >= price)
                {
                    PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") - price);
                    AddItem(itemName, itemCount);

                    AppMetrica.Instance.ReportEvent("#BONUS_BOUGHT " + itemName + " bought for " + moneyType);
                    AppMetrica.Instance.ReportEvent("#BONUS_BOUGHT " + itemName + " bought before " + MetricaManager.Instance.lastUnlockedLevel);
                }
            }
    }

    //public bool CanAddItem(string itemName, int itemCount)
    //{
    //    if (!PlayerPrefs.HasKey(itemName + COUNT))
    //    {
    //        PlayerPrefs.SetInt(itemName + COUNT, 0);
    //        return true;
    //    }
    //    else if (PlayerPrefs.GetInt(itemName + COUNT) + itemCount <= PlayerPrefs.GetInt(MAX + itemName))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    public void AddItem(string itemName, int itemCount) 
    {
        if (!PlayerPrefs.HasKey(itemName + COUNT))
        {
            PlayerPrefs.SetInt(itemName + COUNT, itemCount);
            UpdateItemValue(itemName);
        }
        else if (PlayerPrefs.GetInt(itemName + COUNT) + itemCount <= PlayerPrefs.GetInt(MAX + itemName))
        {
            
        }
        PlayerPrefs.SetInt(itemName + COUNT, PlayerPrefs.GetInt(itemName + COUNT) + itemCount);
        UpdateItemValue(itemName);
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
        return boostersCount[itemName];
    }

    public int GetItemMaxCount(string itemName)
    {
        return PlayerPrefs.GetInt(MAX+itemName);
    }

    public void UpdateItemValue(string itemName)
    {
        boostersCount[itemName] = PlayerPrefs.GetInt(itemName + COUNT);
    }

    public string GetItemShopName(string itemName)
    {
        return shopNames[itemName];
    }

    /* SETTING PLAYER PREFS METHODS */
    private void SetStartingParamsForItem(string itemName, string shopName, int maxCount, int coinCost, int crystalCost)
    {
        // Start Count = 0
        if (!PlayerPrefs.HasKey(itemName + COUNT))
        {
            PlayerPrefs.SetInt(itemName + COUNT, 0);
        }
        // Max Count
        PlayerPrefs.SetInt(MAX + itemName, maxCount);
        // Cost in Coins
        PlayerPrefs.SetInt(itemName + COST_COINS, coinCost);
        // Cost in Crystals
        PlayerPrefs.SetInt(itemName + COST_CRYSTALS, crystalCost);
        //Name in Shop
        PlayerPrefs.SetString(itemName + SHOP_NAME, shopName);

        boostersCount.Add(itemName, PlayerPrefs.GetInt(itemName + COUNT));
        shopNames.Add(itemName, shopName);
    }

    public void UseHP()
    {
        Player.Instance.Health++;
        HealthUI.Instance.SetHealthbar();
        MakeFX.Instance.MakeHeal();

        RemoveItem(HEAL);

        SendUseMetric(HEAL);
    }

    public void UseAmmo()
    {
        Player.Instance.throwingIterator = Player.Instance.clipSize - 1;
        Player.Instance.ResetThrowing();

        RemoveItem(AMMO);

        SendUseMetric(HEAL);
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

        SendUseMetric(bonusType);
    }

    void SendUseMetric(string bonusName)
    {
        AppMetrica.Instance.ReportEvent("#BONUS_USE " + bonusName + " used in " + MetricaManager.Instance.currentLevel);
    }
}
