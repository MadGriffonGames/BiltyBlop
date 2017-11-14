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

    [SerializeField]
    bool changeCoinDate;
    [SerializeField]
    bool changeClipsCountDate;
    [SerializeField]
    bool changePotionDate;

    [SerializeField]
    GameObject fadeButton;


    [SerializeField]
    GameObject lootVolume;


    [SerializeField]
    GameObject fade;

    [SerializeField]
    GameObject giftsPanel;

    TimeSpan spanCoin;
    TimeSpan spanClipsCount;
    TimeSpan spanPotion;

    TimeSpan hours24;

    DateTime CoinlastOpenDate;
    DateTime ClipsCountlastOpenDate;
    DateTime PotionlastOpenDate;

    public const string dailyLootCounter = "dailyLootCounter";
    public const string dailyCoins = "dailyCoins";
    public const string dailyClipsCount = "dailyClipsCount";
    public const string dailyPotions = "dailyPotions";

    public string sceneName { get; set; }
    public const string availableLoots = "avaliableLoots";

    private void Awake()
    {
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
    }

    public void Start()
    {
        if (changeCoinDate)
        {
            PlayerPrefs.SetString("CoinLastOpenDate", "7/4/2016 8:30:52 AM");
            CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));
        }

        if (changeClipsCountDate)
        {
            PlayerPrefs.SetString("ClipsCountLastOpenDate", "7/4/2016 8:30:52 AM");
            ClipsCountlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("ClipsCountLastOpenDate"));
        }

        if (changePotionDate)
        {
            PlayerPrefs.SetString("PotionLastOpenDate", "7/4/2016 8:30:52 AM");
            PotionlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));
        }

        greenCircleAchieve.SetActive(false);
        greenCircleDailyLoot.SetActive(false);

        GeneralDailyCount();

        if (!PlayerPrefs.HasKey(dailyLootCounter))
            PlayerPrefs.SetInt(dailyLootCounter, 0);

        if (PlayerPrefs.GetInt(dailyLootCounter) != 0)
        {
            dailyLootCount.GetComponent<Text>().text = PlayerPrefs.GetInt(dailyLootCounter).ToString();
            greenCircleDailyLoot.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(dailyLootCounter) == 0 || !PlayerPrefs.HasKey(dailyLootCounter))
        {
            dailyLootCount.GetComponent<Text>().text = "";
            greenCircleDailyLoot.gameObject.SetActive(false);
        }
            
        
        if (PlayerPrefs.GetInt(availableLoots) != 0)
        {
            greenCircleAchieve.gameObject.SetActive(true);
            availableLootCounter.gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt(availableLoots).ToString();
        }
        else if (PlayerPrefs.GetInt(availableLoots) == 0 || !PlayerPrefs.HasKey(availableLoots))
        {
            availableLootCounter.gameObject.GetComponent<Text>().text = "";
            greenCircleAchieve.gameObject.SetActive(false);
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
        fade.gameObject.SetActive(true);
        giftsPanel.gameObject.SetActive(true);
        //GameManager.nextLevelName = "Gifts";

        //SceneManager.LoadScene("Loading");
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

    public void GiftClose()
    {
        fade.gameObject.SetActive(false);
        giftsPanel.gameObject.SetActive(false);
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

    public void CheckClipsCountLoot()
    {
        ClipsCountlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("ClipsCountLastOpenDate"));
        spanClipsCount = hours24 + (ClipsCountlastOpenDate - NetworkTime.GetNetworkTime());
        if (spanClipsCount < TimeSpan.Zero)
        {
            if (PlayerPrefs.GetInt(dailyClipsCount) == 0 || !PlayerPrefs.HasKey(dailyClipsCount))
                PlayerPrefs.SetInt(dailyClipsCount, 1);
        }
    }

    public void CheckPotionLoot()
    {
        PotionlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));
        spanPotion = hours24 + (ClipsCountlastOpenDate - NetworkTime.GetNetworkTime());
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
        CheckClipsCountLoot();
        CheckPotionLoot();
        PlayerPrefs.SetInt(dailyLootCounter, PlayerPrefs.GetInt(dailyCoins) + PlayerPrefs.GetInt(dailyClipsCount) + PlayerPrefs.GetInt(dailyPotions));
    }
}
