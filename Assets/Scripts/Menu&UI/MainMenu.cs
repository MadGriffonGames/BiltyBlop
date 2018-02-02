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
    GameObject availableAchievementsCounter;
    [SerializeField]
    GameObject greenCircleAchieve;
    [SerializeField]
    GameObject dailyLootCount;
    [SerializeField]
    GameObject greenCircleDailyLoot;
    [SerializeField]
    GameObject noAdsButton;
    [SerializeField]
    GameObject packButton;
    [SerializeField]
    GameObject starterPackWindow;
    [SerializeField]
    GameObject pack1Window;
    [SerializeField]
    GameObject pack1_noadsWindow;
    [SerializeField]
    Text packTitle;

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
    TimeSpan hours12;

    DateTime CoinlastOpenDate;
    DateTime ClipsCountlastOpenDate;
    DateTime PotionlastOpenDate;

    public const string dailyLootCounter = "dailyLootCounter";
    public const string dailyCoins = "dailyCoins";
    public const string dailyClipsCount = "dailyClipsCount";
    public const string dailyPotions = "dailyPotions";

    public string sceneName { get; set; }
    public const string availableAchievements = "avaliableLoots";

    private void Awake()
    {
        FirstAppEnter();

        CheckStarterPack();

        if (PlayerPrefs.GetInt("NoAds") == 1)
        {
            noAdsButton.SetActive(false);
        }

        hours12 = new TimeSpan(12, 0, 0);
        PlayerPrefs.SetInt("Level2",1);
    }

    public void Start()
    {
        SetMap();

        ChangeDatesOfGifts();

        greenCircleAchieve.SetActive(false);
        greenCircleDailyLoot.SetActive(false);

        GeneralDailyCount();
        SetGiftsIndication();
        SetAchievementsIndication();

        SoundManager.PlayMusic("main menu", true);
    }

    public void ToActSelect(string sceneName)
    {
        actCanvas.SetActive(true);
        this.gameObject.SetActive(false);
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
        spanCoin = hours12 + (CoinlastOpenDate - NetworkTime.GetNetworkTime());
        if (spanCoin < TimeSpan.Zero)
        {
            if (PlayerPrefs.GetInt(dailyCoins) == 0 || !PlayerPrefs.HasKey(dailyCoins))
                PlayerPrefs.SetInt(dailyCoins, 1);
        }
    }

    public void CheckClipsCountLoot()
    {
        ClipsCountlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("ClipsCountLastOpenDate"));
        spanClipsCount = hours12 + (ClipsCountlastOpenDate - NetworkTime.GetNetworkTime());

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
        hours24 = new TimeSpan(24, 0 , 0);
        CheckCoinLoot();
        CheckClipsCountLoot();
        CheckPotionLoot();
        PlayerPrefs.SetInt(dailyLootCounter, PlayerPrefs.GetInt(dailyCoins) + PlayerPrefs.GetInt(dailyClipsCount) + PlayerPrefs.GetInt(dailyPotions));
    }

    public void OpenPackWindow()
    {
        fade.SetActive(true);

        if (PlayerPrefs.GetInt("StarterPackBought") == 0)
        {
            starterPackWindow.SetActive(true);
        }
        else
        {
            if (PlayerPrefs.GetInt("NoAds") == 0)
            {
                pack1_noadsWindow.SetActive(true);
            }
            else
            {
                pack1Window.SetActive(true);
            }
        }
        

        AppMetrica.Instance.ReportEvent("#STARTER_PACK shown");
        DevToDev.Analytics.CustomEvent("#STARTER_PACK shown");
    }

    void ChangeDatesOfGifts()
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
    }

    void FirstAppEnter()
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

            changeClipsCountDate = true;
            changeCoinDate = true;
            changePotionDate = true;

            noAdsButton.SetActive(false);
        }
        else
        {
            changeClipsCountDate = false;
            changeCoinDate = false;
            changePotionDate = false;
        }
    }

    public void RemoveAdsButton()
    {
        PurchaseManager.Instance.BuyNonConsumable(0);
    }

    void CheckStarterPack()
    {
        if (PlayerPrefs.HasKey("StarterPackOpenDate"))
        {
            TimeSpan hours48 = new TimeSpan(48, 0, 0);

            DateTime lastOpenDate = new DateTime();
            lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("StarterPackOpenDate"));
            if ((hours48 + (lastOpenDate - DateTime.Now)) <= TimeSpan.Zero)
            {
                PlayerPrefs.SetInt("StarterPackBought", 1);
                PlayerPrefs.DeleteKey("StarterPackOpenDate");
            }
        }

        if (PlayerPrefs.GetInt("StarterPackBought") == 1)
        {
            packTitle.text = "hero's choice";
            LocalizationManager.Instance.UpdateLocaliztion(packTitle);
        }
        if (PlayerPrefs.GetInt("Pack1Bought") == 1 || PlayerPrefs.GetInt("Pack1_NoAdsBought") == 1)
        {
            packTitle.transform.parent.gameObject.SetActive(false);
        }
    }

    void SetGiftsIndication()
    {
        if (!PlayerPrefs.HasKey(dailyLootCounter))
            PlayerPrefs.SetInt(dailyLootCounter, 0);

        if (PlayerPrefs.GetInt(dailyLootCounter) != 0)
        {
            dailyLootCount.GetComponent<Text>().text = PlayerPrefs.GetInt(dailyLootCounter).ToString();
            greenCircleDailyLoot.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(dailyLootCounter) == 0 || !PlayerPrefs.HasKey(dailyLootCounter))
        {
            dailyLootCount.GetComponent<Text>().text = "";
            greenCircleDailyLoot.SetActive(false);
        }
    }

    void SetAchievementsIndication()
    {
        if (PlayerPrefs.GetInt(availableAchievements) != 0)
        {
            greenCircleAchieve.SetActive(true);
            availableAchievementsCounter.GetComponent<Text>().text = PlayerPrefs.GetInt(availableAchievements).ToString();
        }
        else if (PlayerPrefs.GetInt(availableAchievements) == 0 || !PlayerPrefs.HasKey(availableAchievements))
        {
            availableAchievementsCounter.GetComponent<Text>().text = "";
            greenCircleAchieve.SetActive(false);
        }
    }

    void SetMap()
    {
        PlayerPrefs.SetString("LastCompletedLevel", "");
        PlayerPrefs.SetInt("TutorialMode", 0);
        PlayerPrefs.SetInt("FromMap", 0);
    }
}
