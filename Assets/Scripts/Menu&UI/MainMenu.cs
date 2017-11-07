using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject actCanvas;
    [SerializeField]
    GameObject betaTestReward;
    [SerializeField]
    GameObject availableLootCounter;
    [SerializeField]
    GameObject greenCircleAchieve;
    [SerializeField]
    GameObject dailyLootCount;
    [SerializeField]
    GameObject greenCircleDailyLoot;

    TimeSpan spanCoin;
    TimeSpan spanCrystal;
    TimeSpan spanPotion;

    TimeSpan hours24;

    DateTime CoinlastOpenDate;
    DateTime CrystallastOpenDate;
    DateTime PotionlastOpenDate;

    public const string dailyLootCounter = "dailyLootCounter";
    public const string dailyCoins = "dailyCoins";
    public const string dailyCrystals = "dailyCrystals";
    public const string dailyPotions = "dailyPotions";

    public string sceneName { get; set; }
    public const string availableLoots = "avaliableLoots";

    public void Start()
    {
        GeneralDailyCount();
        Debug.Log(PlayerPrefs.GetInt(dailyLootCounter));
        if (!PlayerPrefs.HasKey(dailyLootCounter))
            PlayerPrefs.SetInt(dailyLootCounter, 0);

        if (PlayerPrefs.GetInt(dailyLootCounter) != 0)
        {
            dailyLootCount.GetComponent<Text>().text = PlayerPrefs.GetInt(dailyLootCounter).ToString();
            greenCircleDailyLoot.gameObject.SetActive(true);
        }
        else
        {
            dailyLootCount.GetComponent<Text>().text = "";
            greenCircleDailyLoot.gameObject.SetActive(false);
        }
            
        
        if (PlayerPrefs.GetInt(availableLoots) != 0)
        {
            greenCircleAchieve.gameObject.SetActive(true);
            availableLootCounter.gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt(availableLoots).ToString();
        }
        else
        {
            availableLootCounter.gameObject.GetComponent<Text>().text = "";
            greenCircleAchieve.gameObject.SetActive(false);
        }


        if (!PlayerPrefs.HasKey("FirstEnter"))
        {
            PlayerPrefs.SetInt("FirstEnter", 1); 
                      
            PlayerPrefs.SetInt("SwordDisplayIndex", 0);
            PlayerPrefs.SetInt("SwordAttackStat", 1);

            PlayerPrefs.SetInt("SkinDisplayIndex", 0);
            PlayerPrefs.SetInt("SkinArmorStat", 3);
            PlayerPrefs.SetString("Skin", "Classic");

            PlayerPrefs.SetString("Throw", "ClassicThrow");
            PlayerPrefs.SetInt("ThrowAttackStat", 1);
            PlayerPrefs.SetFloat("ThrowSpeedStat", 14);
        }			

        SoundManager.PlayMusic("main menu", true);
    }

    public void ToActSelect(string sceneName)
    {
        actCanvas.SetActive(true);
    }

    public void ToAchievmentMenu()
    {
        GameManager.nextLevelName = "AchievementMenu";

        SceneManager.LoadScene("Loading");
    }

    public void ToGiftsMenu()
    {
        GameManager.nextLevelName = "Gifts";

        SceneManager.LoadScene("Loading");
    }

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    public void ToShop()
    {
        GameManager.nextLevelName = "Shop";

        SceneManager.LoadScene("Loading");
    }

    public void RateUs()
    {
        if (Application.systemLanguage.ToString() == "Russian" || Application.systemLanguage.ToString() == "Ukrainian" || Application.systemLanguage.ToString() == "Belarusian")
        {
            Application.OpenURL("https://docs.google.com/forms/d/1RzwBi5aEDaxPxkkPDz91RwuDNApOxV_VFm2UDZDob4s");
        }
        else
        {
            Application.OpenURL("https://docs.google.com/forms/d/1sB0ASWy2K15KBX2QXThl9lED36WnT71OHxl8vWQFL9k");
        }             
    }

    public void BetaRewardButton()
    {
		if (PlayerPrefs.GetInt("Coins") == 0 && PlayerPrefs.GetInt("Crystals") == 0)
        {
			PlayerPrefs.SetString ("FirstTimeInGame", "No");
        }
        betaTestReward.SetActive(false);
    }

    public void CheckCoinLoot()
    {
        CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));
        spanCoin = hours24 + (CoinlastOpenDate - NetworkTime.GetNetworkTime());
        if (spanCoin < TimeSpan.Zero)
        {
            if (PlayerPrefs.GetInt(dailyCoins) == 0 || !PlayerPrefs.HasKey(dailyCoins))
                PlayerPrefs.SetInt(dailyCoins, 1);
        }
    }

    public void CheckCrystalLoot()
    {
        CrystallastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CrystalLastOpenDate"));
        spanCrystal = hours24 + (CrystallastOpenDate - NetworkTime.GetNetworkTime());
        if (spanCrystal < TimeSpan.Zero)
        {
            if (PlayerPrefs.GetInt(dailyCrystals) == 0 || !PlayerPrefs.HasKey(dailyCrystals))
                PlayerPrefs.SetInt(dailyCrystals, 1);
        }
    }

    public void CheckPotionLoot()
    {
        PotionlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));
        spanPotion = hours24 + (CrystallastOpenDate - NetworkTime.GetNetworkTime());
        if (spanPotion < TimeSpan.Zero)
        {
            if (PlayerPrefs.GetInt(dailyPotions) == 0 || !PlayerPrefs.HasKey(dailyPotions))
                PlayerPrefs.SetInt(dailyPotions, 1);
        }
    }

    void GeneralDailyCount()
    {
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);
        CheckCoinLoot();
        CheckCrystalLoot();
        CheckPotionLoot();
        PlayerPrefs.SetInt(dailyLootCounter, PlayerPrefs.GetInt(dailyCoins) + PlayerPrefs.GetInt(dailyCrystals) + PlayerPrefs.GetInt(dailyPotions));
    }
}
