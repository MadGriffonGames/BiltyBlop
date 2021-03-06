﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DailyLoot : MonoBehaviour
{
    [SerializeField]
    GameObject greenCircle;
    [SerializeField]
    GameObject greenCircleCounter;

    [SerializeField]
    string coinDescription;
    [SerializeField]
    string clipsCountDescription;
    [SerializeField]
    string potionDescription;
    [SerializeField]
    Sprite coins;

    [SerializeField]
    Sprite clipsCount;

    [SerializeField]
    Sprite potion;

    [SerializeField]
    Sprite damageBonus;

    [SerializeField]
    Sprite speedBonus;

    [SerializeField]
    Sprite timeBonus;

    [SerializeField]
    Sprite immortalBonus;

    [SerializeField]
    Sprite heal;

    DateTime coinLastOpenDate;
    DateTime clipsCountLastOpenDate;
    DateTime potionLastOpenDate;

    TimeSpan spanCoin;
    TimeSpan spanClipsCount;
    TimeSpan spanPotion;

    TimeSpan hours24;

    bool is24hoursPastCoin;
    bool is24hoursPastClipsCount;
    bool is24hoursPastPotion;

    bool isTimerTickCoin;
    bool isTimerTickClipsCount;
    bool isTimerTickPotion;

    bool coinVideo;
    bool clipsCountVideo;
    bool potionVideo;

    public const string HEAL = "HealthPot";
    public const string DAMAGE_BONUS = "DamageBonus";
    public const string SPEED_BONUS = "SpeedBonus";
    public const string TIME_BONUS = "TimeBonus";
    public const string IMMORTAL_BONUS = "ImmortalBonus";
    public const string AMMO = "ClipsCount";

    public string[] itemArray;


    [SerializeField]
    GameObject coinButton;
    [SerializeField]
    GameObject clipsCountButton;
    [SerializeField]
    GameObject potionButton;


    [SerializeField]
    Text coinTimer;
    [SerializeField]
    Text clipsCountTimer;
    [SerializeField]
    Text potionTimer;

    [SerializeField]
    GameObject lootVolume;
    [SerializeField]
    GameObject rewardFade;
    [SerializeField]
    GameObject loot;
    [SerializeField]
    GameObject fadeButton;
    [SerializeField]
    Sprite freeButton;
    [SerializeField]
    Image[] buttonsImages;
    [SerializeField]
    Text[] freeText;

    public const string dailyLootCounter = "dailyLootCounter";
    public const string dailyCoins = "dailyCoins";
    public const string dailyClipsCount = "dailyClipsCount";
    public const string dailyPotions = "dailyPotions";

    TimeSpan hours12;

    void Start ()
    {
        if (!PlayerPrefs.HasKey("CoinLastOpenDate"))
        {
            PlayerPrefs.SetString("CoinLastOpenDate", "7/4/2016 8:30:52 AM");
        }
        if (!PlayerPrefs.HasKey("ClipsCountLastOpenDate"))
        {
            PlayerPrefs.SetString("ClipsCountLastOpenDate", "7/4/2016 8:30:52 AM");
        }
        if (!PlayerPrefs.HasKey("PotionLastOpenDate"))
        {
            PlayerPrefs.SetString("PotionLastOpenDate", "7/4/2016 8:30:52 AM");
        }

        hours12 = new TimeSpan(12,0,0);
        itemArray = new string[] { HEAL, DAMAGE_BONUS, SPEED_BONUS, TIME_BONUS, IMMORTAL_BONUS };
        CoinStart();
        ClipsCountStart();
        PotionStart();
        SetGiftButtons();
    }

	void Update ()
    {
        if (AdsManager.Instance.isRewardVideoWatched && coinVideo)
        {
            AdsManager.Instance.isRewardVideoWatched = false;
            PlayerPrefs.SetInt(dailyCoins, 0);
            UpdateIndicator();

            GiveCoinReward(50);
            coinVideo = false;
        }

        if (AdsManager.Instance.isRewardVideoWatched && clipsCountVideo)
        {
            AdsManager.Instance.isRewardVideoWatched = false;
            PlayerPrefs.SetInt(dailyClipsCount, 0);
            UpdateIndicator();

            GiveClipsCountReward(1);
            clipsCountVideo = false;
        }

        if (AdsManager.Instance.isRewardVideoWatched && potionVideo)
        {
            AdsManager.Instance.isRewardVideoWatched = false;
            PlayerPrefs.SetInt(dailyPotions, 0);
            UpdateIndicator();

            GivePotionReward(1);
            clipsCountVideo = false;
        }

        if (isTimerTickCoin)
        {
            spanCoin = hours12 + (coinLastOpenDate - DateTime.Now);
            coinTimer.text = spanCoin.ToString().Substring(0, 8);
            if (spanCoin <= TimeSpan.Zero)
            {
                ActivateCoinLoot();
            }
        }

        if (isTimerTickClipsCount)
        {
            spanClipsCount = hours12 + (clipsCountLastOpenDate - DateTime.Now);
            clipsCountTimer.text = spanClipsCount.ToString().Substring(0, 8);
            if (spanClipsCount <= TimeSpan.Zero)
            {
                ActivateClipsCountLoot();
            }
        }
        

        if (isTimerTickPotion)
        {
            spanPotion = hours24 + (potionLastOpenDate - DateTime.Now);
            potionTimer.text = spanPotion.ToString().Substring(0, 8);
            if (spanPotion <= TimeSpan.Zero)
            {
                ActivatePotionLoot();
            }
        }
    }

    void ActivateCoinLoot()
    {
        coinButton.gameObject.SetActive(true);
        coinButton.GetComponent<Button>().interactable = true;
        isTimerTickCoin = false;
        coinTimer.gameObject.SetActive(false);
    }

    void ActivateClipsCountLoot()
    {
        clipsCountButton.gameObject.SetActive(true);
        clipsCountButton.GetComponent<Button>().interactable = true;
        isTimerTickClipsCount = false;
        clipsCountTimer.gameObject.SetActive(false);
    }

    void ActivatePotionLoot()
    {
        potionButton.gameObject.SetActive(true);
        potionButton.GetComponent<Button>().interactable = true;
        isTimerTickPotion = false;
        potionTimer.gameObject.SetActive(false);
    }

    public string PotionRandomizer()
    {
        int tmp = UnityEngine.Random.Range(0, 5);
        return itemArray[tmp];
    }

    public void OpenCoinButton()
    {
        DateTime now = NetworkTime.GetNetworkTime();
        if (now - coinLastOpenDate > hours12)
        {
            spanCoin = coinLastOpenDate - NetworkTime.GetNetworkTime();
            PlayerPrefs.SetString("CoinLastOpenDate", NetworkTime.GetNetworkTime().ToString());
            coinLastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));

            AppMetrica.Instance.ReportEvent("#GIFTS coins");
            DevToDev.Analytics.CustomEvent("#GIFTS coins");
        }
    }

    public void OpenClipsCountButton()
    {
        DateTime now = NetworkTime.GetNetworkTime();
        if (now - clipsCountLastOpenDate > hours12)
        {
            spanClipsCount = clipsCountLastOpenDate - NetworkTime.GetNetworkTime();
            PlayerPrefs.SetString("ClipsCountLastOpenDate", NetworkTime.GetNetworkTime().ToString());
            clipsCountLastOpenDate = DateTime.Parse(PlayerPrefs.GetString("ClipsCountLastOpenDate"));

            AppMetrica.Instance.ReportEvent("#GIFTS ammo");
            DevToDev.Analytics.CustomEvent("#GIFTS ammo");
        }
    }



    public void OpenPotionButton()
    {
        if (NetworkTime.Check24hours(potionLastOpenDate))
        {
            spanPotion = potionLastOpenDate - NetworkTime.GetNetworkTime();
            PlayerPrefs.SetString("PotionLastOpenDate", NetworkTime.GetNetworkTime().ToString());
            potionLastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));

            AppMetrica.Instance.ReportEvent("#GIFTS potion");
            DevToDev.Analytics.CustomEvent("#CHEST Daily chest activate");
        }
    }

    void GiveCoinReward(int lootVol)
    {
        GameManager.AddCoins(lootVol);
        ShowCoinLoot();
        isTimerTickCoin = true;
        coinTimer.gameObject.SetActive(true);
        coinButton.GetComponent<Button>().interactable = false;
    }

    void GiveClipsCountReward(int lootVol)
    {
        Inventory.Instance.AddItem(AMMO, lootVol);
        ShowClipsCountLoot();
        isTimerTickClipsCount = true;
        clipsCountTimer.gameObject.SetActive(true);
        clipsCountButton.GetComponent<Button>().interactable = false;
    }

    void GivePotionReward(int lootVol)
    {
        string temp = PotionRandomizer();
        Inventory.Instance.AddItem(temp, lootVol);
        ShowPotionLoot(temp);
        isTimerTickPotion = true;
        potionTimer.gameObject.SetActive(true);
        potionButton.GetComponent<Button>().interactable = false;
    }

    public void RewardedCoinVideoButton()
    {
        coinVideo = true;
#if UNITY_EDITOR
        AdsManager.Instance.isRewardVideoWatched = true;
        
#elif UNITY_ANDROID || UNITY_IOS
        AdsManager.Instance.ShowRewardedVideo();
#endif
    }

    public void RewardedClipsCountVideoButton()
    {
        clipsCountVideo = true;
#if UNITY_EDITOR
        AdsManager.Instance.isRewardVideoWatched = true;
#elif UNITY_ANDROID || UNITY_IOS
        AdsManager.Instance.ShowRewardedVideo();
#endif
    }

    public void RewardedPotionVideoButton()
    {
        potionVideo = true;
#if UNITY_EDITOR
        AdsManager.Instance.isRewardVideoWatched = true;
#elif UNITY_ANDROID || UNITY_IOS
        AdsManager.Instance.ShowRewardedVideo();
#endif
    }


    void CoinStart()
    {
        isTimerTickCoin = false;
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);
        coinLastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));

        is24hoursPastCoin = NetworkTime.Check24hours(coinLastOpenDate);
        if (!is24hoursPastCoin)
        {
            coinButton.GetComponent<Button>().interactable = false;
            isTimerTickCoin = true;
        }
        else
        {
            ActivateCoinLoot();
        }
    }

    void ClipsCountStart()
    {
        isTimerTickClipsCount = false;
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);

        clipsCountLastOpenDate = DateTime.Parse(PlayerPrefs.GetString("ClipsCountLastOpenDate"));

        is24hoursPastClipsCount = NetworkTime.Check24hours(clipsCountLastOpenDate);
        if (!is24hoursPastClipsCount)
        {
            clipsCountButton.GetComponent<Button>().interactable = false;
            isTimerTickClipsCount = true;
        }
        else
        {
            ActivateClipsCountLoot();
        }
    }

    void PotionStart()
    {
        isTimerTickPotion = false;
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);

        potionLastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));

        is24hoursPastPotion = NetworkTime.Check24hours(potionLastOpenDate);
        if (!is24hoursPastPotion)
        {
            potionButton.GetComponent<Button>().interactable = false;
            isTimerTickPotion = true;
        }
        else
        {
            ActivatePotionLoot();
        }
    }

    void ShowCoinLoot()
    {
        lootVolume.GetComponent<Text>().text = coinDescription;
        rewardFade.gameObject.SetActive(true);
        loot.gameObject.GetComponent<Image>().sprite = coins;
        loot.gameObject.SetActive(true);
        fadeButton.SetActive(true);
    }

    void ShowClipsCountLoot()
    {
        lootVolume.GetComponent<Text>().text = clipsCountDescription;
        rewardFade.gameObject.SetActive(true);
        loot.gameObject.GetComponent<Image>().sprite = clipsCount;
        loot.gameObject.SetActive(true);
        fadeButton.SetActive(true);
    }

    void ShowPotionLoot(string potionName)
    {
        lootVolume.GetComponent<Text>().text = clipsCountDescription;
        rewardFade.gameObject.SetActive(true);
        if (potionName == HEAL)
            loot.gameObject.GetComponent<Image>().sprite = heal;
        if (potionName == DAMAGE_BONUS)
            loot.gameObject.GetComponent<Image>().sprite = damageBonus;
        if (potionName == SPEED_BONUS)
            loot.gameObject.GetComponent<Image>().sprite = speedBonus;
        if (potionName == TIME_BONUS)
            loot.gameObject.GetComponent<Image>().sprite = timeBonus;
        if (potionName == IMMORTAL_BONUS)
            loot.gameObject.GetComponent<Image>().sprite = immortalBonus;
        loot.gameObject.SetActive(true);
        fadeButton.SetActive(true);
    }

    public void FadeOut()
    {
        fadeButton.SetActive(false);
        rewardFade.SetActive(false);
        loot.SetActive(false);
    }

    void UpdateIndicator()
    {
        int tmp = PlayerPrefs.GetInt(dailyCoins) + PlayerPrefs.GetInt(dailyClipsCount) + PlayerPrefs.GetInt(dailyPotions);
        greenCircleCounter.GetComponent<Text>().text = tmp.ToString();
        if (tmp == 0)
        {
            greenCircle.SetActive(false);
        }
    }

    void SetGiftButtons()
    {
        if (PlayerPrefs.GetInt("NoAds") > 0)
        {
            for (int i = 0; i < buttonsImages.Length; i++)
            {
                buttonsImages[i].sprite = freeButton;
                freeText[i].gameObject.SetActive(true);
            }
        }
    }
}
