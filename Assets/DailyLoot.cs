using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DailyLoot : MonoBehaviour {

    //[SerializeField]
    //bool changeCoinDate;
    //[SerializeField]
    //bool changeCrystalDate;
    //[SerializeField]
    //bool changePotionDate;

    [SerializeField]
    GameObject greenCircle;
    [SerializeField]
    GameObject greenCircleCounter;

    [SerializeField]
    string coinDescription;
    [SerializeField]
    string crystalDescription;
    [SerializeField]
    string potionDescription;
    [SerializeField]
    Sprite coins;

    [SerializeField]
    Sprite crystals;

    [SerializeField]
    Sprite potion;

    DateTime CoinlastOpenDate;
    DateTime CrystallastOpenDate;
    DateTime PotionlastOpenDate;

    TimeSpan spanCoin;
    TimeSpan spanCrystal;
    TimeSpan spanPotion;

    TimeSpan hours24;

    bool is24hoursPastCoin;
    bool is24hoursPastCrystal;
    bool is24hoursPastPotion;

    bool isTimerTickCoin;
    bool isTimerTickCrystal;
    bool isTimerTickPotion;

    bool coinVideo;
    bool crystalVideo;
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
    GameObject crystalButton;
    [SerializeField]
    GameObject potionButton;


    [SerializeField]
    Text coinTimer;
    [SerializeField]
    Text crystalTimer;
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

    //[SerializeField]
    //GameObject coinDailyRewardWindow;
    //[SerializeField]
    //GameObject crystalDailyRewardWindow;
    //[SerializeField]
    //GameObject potionDailyRewardWindow;

    public const string dailyLootCounter = "dailyLootCounter";
    public const string dailyCoins = "dailyCoins";
    public const string dailyCrystals = "dailyCrystals";
    public const string dailyPotions = "dailyPotions";

    void Start ()
    {
        //coinDailyRewardWindow.gameObject.SetActive(false);
        //crystalDailyRewardWindow.gameObject.SetActive(false);
        //potionDailyRewardWindow.gameObject.SetActive(false);
        itemArray = new string[] { HEAL, DAMAGE_BONUS, SPEED_BONUS, TIME_BONUS, IMMORTAL_BONUS, AMMO };
        CoinStart();
        CrystalStart();
        PotionStart();
    }

	void Update ()
    {
        if (AdsManager.Instance.isRewardVideoWatched && coinVideo)
        {
            AdsManager.Instance.isRewardVideoWatched = false;
            GiveCoinReward(40);
            coinVideo = false;
            ShowCoinLoot();
        }

        if (AdsManager.Instance.isRewardVideoWatched && crystalVideo)
        {
            AdsManager.Instance.isRewardVideoWatched = false;
            GiveCrystalReward(5);
            crystalVideo = false;
            ShowCrystalLoot();
        }

        if (AdsManager.Instance.isRewardVideoWatched && potionVideo)
        {
            AdsManager.Instance.isRewardVideoWatched = false;
            GivePotionReward(1);
            crystalVideo = false;
            ShowPotionLoot();
        }

        if (isTimerTickCoin)
        {
            spanCoin = hours24 + (CoinlastOpenDate - DateTime.Now);
            coinTimer.text = spanCoin.ToString().Substring(0, 8);
            if (spanCoin <= TimeSpan.Zero)
            {
                ActivateCoinLoot();
            }
        }

        if (isTimerTickCrystal)
        {
            spanCrystal = hours24 + (CrystallastOpenDate - DateTime.Now);
            crystalTimer.text = spanCrystal.ToString().Substring(0, 8);
            if (spanCrystal <= TimeSpan.Zero)
            {
                ActivateCrystalLoot();
            }
        }


        if (isTimerTickPotion)
        {
            spanPotion = hours24 + (PotionlastOpenDate - DateTime.Now);
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

    void ActivateCrystalLoot()
    {
        crystalButton.gameObject.SetActive(true);
        crystalButton.GetComponent<Button>().interactable = true;
        isTimerTickCrystal = false;
        crystalTimer.gameObject.SetActive(false);
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
        if (NetworkTime.Check24hours(CoinlastOpenDate))
        {
            PlayerPrefs.SetInt(dailyCoins, 0);
            int tmp = PlayerPrefs.GetInt(dailyCoins) + PlayerPrefs.GetInt(dailyCrystals) + PlayerPrefs.GetInt(dailyPotions);
            greenCircleCounter.GetComponent<Text>().text = tmp.ToString();
            if (tmp == 0)
            {
                greenCircle.SetActive(false);
            }
            spanCoin = CoinlastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTickCoin = true;
            coinTimer.gameObject.SetActive(true);
            coinButton.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetString("CoinLastOpenDate", NetworkTime.GetNetworkTime().ToString());
            CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));

            AppMetrica.Instance.ReportEvent("#CHEST Daily chest activate");
        }
    }

    public void OpenCrystalButton()
    {
        if (NetworkTime.Check24hours(CrystallastOpenDate))
        {
            PlayerPrefs.SetInt(dailyCrystals, 0);
            int tmp = PlayerPrefs.GetInt(dailyCoins) + PlayerPrefs.GetInt(dailyCrystals) + PlayerPrefs.GetInt(dailyPotions);
            greenCircleCounter.GetComponent<Text>().text = tmp.ToString();
            if (tmp == 0)
            {
                greenCircle.SetActive(false);
            }
            spanCrystal = CrystallastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTickCrystal = true;
            crystalTimer.gameObject.SetActive(true);
            crystalButton.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetString("CrystalLastOpenDate", NetworkTime.GetNetworkTime().ToString());
            CrystallastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CrystalLastOpenDate"));
            AppMetrica.Instance.ReportEvent("#CHEST Daily chest activate");
        }
    }



    public void OpenPotionButton()
    {
        if (NetworkTime.Check24hours(PotionlastOpenDate))
        {
            PlayerPrefs.SetInt(dailyPotions, 0);
            int tmp = PlayerPrefs.GetInt(dailyCoins) + PlayerPrefs.GetInt(dailyCrystals) + PlayerPrefs.GetInt(dailyPotions);
            greenCircleCounter.GetComponent<Text>().text = tmp.ToString();
            if (tmp == 0)
            {
                greenCircle.SetActive(false);
            }
            spanPotion = PotionlastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTickPotion = true;
            potionTimer.gameObject.SetActive(true);
            potionButton.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetString("PotionLastOpenDate", NetworkTime.GetNetworkTime().ToString());
            PotionlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));
            AppMetrica.Instance.ReportEvent("#CHEST Daily chest activate");
            DevToDev.Analytics.CustomEvent("#CHEST Daily chest activate");
        }
    }

    void GiveCoinReward(int lootVol)
    {
        GameManager.AddCoins(lootVol);
        //PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + lootVol);
    }

    void GiveCrystalReward(int lootVol)
    {
        GameManager.AddCrystals(lootVol);
        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + lootVol);
    }

    void GivePotionReward(int lootVol)
    {
        string temp = PotionRandomizer();
        Inventory.Instance.AddItem(temp, lootVol);
        //if (!PlayerPrefs.HasKey(temp + "Count"))
        //{
        //    PlayerPrefs.SetInt(temp + "Count", lootVol);
        //    //Inventory.Instance.UpdateItemValue(temp);
        //}
        //else
        //{
        //    PlayerPrefs.SetInt(temp + "Count", PlayerPrefs.GetInt(temp + "Count") + lootVol);
        //    //Inventory.Instance.UpdateItemValue(temp);
        //}
        //Inventory.Instance.AddItem(temp, lootVol);
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

    public void RewardedCrystalVideoButton()
    {
        crystalVideo = true;
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
        //if (changeCoinDate)
        //{
        //    PlayerPrefs.SetString("CoinLastOpenDate", "7/4/2016 8:30:52 AM");
        //    CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));
        //}
        isTimerTickCoin = false;
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);
        CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));

        is24hoursPastCoin = NetworkTime.Check24hours(CoinlastOpenDate);
        if (!is24hoursPastCoin)
        {
            //coinButton.gameObject.SetActive(false);
            coinButton.GetComponent<Button>().interactable = false;
            isTimerTickCoin = true;
        }
        else
        {
            ActivateCoinLoot();
        }
    }

    void CrystalStart()
    {
        //if (changeCrystalDate)
        //{
        //    PlayerPrefs.SetString("CrystalLastOpenDate", "7/4/2016 8:30:52 AM");
        //    CrystallastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CrystalLastOpenDate"));
        //}
        isTimerTickCrystal = false;
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);

        CrystallastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CrystalLastOpenDate"));

        is24hoursPastCrystal = NetworkTime.Check24hours(CrystallastOpenDate);
        if (!is24hoursPastCrystal)
        {
            //coinButton.gameObject.SetActive(false);
            crystalButton.GetComponent<Button>().interactable = false;
            isTimerTickCrystal = true;
        }
        else
        {
            ActivateCrystalLoot();
        }
    }

    void PotionStart()
    {
        //if (changePotionDate)
        //{
        //    PlayerPrefs.SetString("PotionLastOpenDate", "7/4/2016 8:30:52 AM");
        //    PotionlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));
        //}
        isTimerTickPotion = false;
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);

        PotionlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));

        is24hoursPastPotion = NetworkTime.Check24hours(PotionlastOpenDate);
        if (!is24hoursPastPotion)
        {
            //coinButton.gameObject.SetActive(false);
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

    void ShowCrystalLoot()
    {
        lootVolume.GetComponent<Text>().text = crystalDescription;
        rewardFade.gameObject.SetActive(true);
        loot.gameObject.GetComponent<Image>().sprite = crystals;
        loot.gameObject.SetActive(true);
        fadeButton.SetActive(true);
    }

    void ShowPotionLoot()
    {
        lootVolume.GetComponent<Text>().text = crystalDescription;
        rewardFade.gameObject.SetActive(true);
        loot.gameObject.GetComponent<Image>().sprite = potion;
        loot.gameObject.SetActive(true);
        fadeButton.SetActive(true);
    }

    public void FadeOut()
    {
        fadeButton.SetActive(false);
        rewardFade.SetActive(false);
        loot.SetActive(false);
    }

}
