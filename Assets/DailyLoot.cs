using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DailyLoot : MonoBehaviour {

    [SerializeField]
    bool changeCoinDate;
    [SerializeField]
    bool changeCrystalDate;
    [SerializeField]
    bool changePotionDate;

    DateTime CoinlastOpenDate;
    DateTime CrystallastOpenDate;
    DateTime PotionlastOpenDate;

    TimeSpan spanCoin;
    TimeSpan spanCrystal;
    TimeSpan spanPotion;

    TimeSpan hours24Coin;
    TimeSpan hours24Crystal;
    TimeSpan hours24Potion;

    bool is24hoursPastCoin;
    bool is24hoursPastCrystal;
    bool is24hoursPastPotion;

    bool isTimerTickCoin;
    bool isTimerTickCrystal;
    bool isTimerTickPotion;


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
    GameObject coinDailyRewardWindow;
    [SerializeField]
    GameObject crystalDailyRewardWindow;
    [SerializeField]
    GameObject potionDailyRewardWindow;

    [SerializeField]
    GameObject getCoin;
    [SerializeField]
    GameObject getCrystal;
    [SerializeField]
    GameObject getPotion;



    // Use this for initialization
    void Start () {
        coinDailyRewardWindow.gameObject.SetActive(false);
        crystalDailyRewardWindow.gameObject.SetActive(false);
        potionDailyRewardWindow.gameObject.SetActive(false);
        CoinStart();
        CrystalStart();
        PotionStart();
    }
	
	// Update is called once per frame
	void Update () {
        if (AdsManager.Instance.isRewardVideoWatched)
        {
            AdsManager.Instance.isRewardVideoWatched = false;
            GiveCoinReward(150);
        }

        if (isTimerTickCoin)
        {
            spanCoin = hours24Coin + (CoinlastOpenDate - NetworkTime.GetNetworkTime());
            coinTimer.text = spanCoin.ToString().Substring(0, 8);
            if (spanCoin <= TimeSpan.Zero)
            {
                ActivateCoinLoot();
            }
        }

        if (isTimerTickCrystal)
        {
            spanCrystal = hours24Crystal + (CrystallastOpenDate - NetworkTime.GetNetworkTime());
            crystalTimer.text = spanCrystal.ToString().Substring(0, 8);
            if (spanCrystal <= TimeSpan.Zero)
            {
                ActivateCrystalLoot();
            }
        }

        if (isTimerTickPotion)
        {
            spanPotion = hours24Potion + (PotionlastOpenDate - NetworkTime.GetNetworkTime());
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

    public void OpenCoinButton()
    {
        if (NetworkTime.Check24hours(CoinlastOpenDate))
        {
            Debug.Log("Button Pressed");
            spanCoin = CoinlastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTickCoin = true;
            coinTimer.gameObject.SetActive(true);
            coinButton.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetString("CoinLastOpenDate", NetworkTime.GetNetworkTime().ToString());
            coinDailyRewardWindow.gameObject.SetActive(true);
            CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));

            AppMetrica.Instance.ReportEvent("#CHEST Daily chest activate");
        }
    }

    public void OpenCrystalButton()
    {
        if (NetworkTime.Check24hours(CrystallastOpenDate))
        {
            Debug.Log("Crystal Button Pressed");
            spanCrystal = CrystallastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTickCrystal = true;
            crystalTimer.gameObject.SetActive(true);
            crystalButton.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetString("CrystalLastOpenDate", NetworkTime.GetNetworkTime().ToString());
            Debug.Log(DateTime.Parse(NetworkTime.GetNetworkTime().ToString()));
            Debug.Log(DateTime.Parse(PlayerPrefs.GetString("CrystalLastOpenDate")));
            Debug.Log(DateTime.Now);
            Debug.Log(hours24Crystal);
            CrystallastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CrystalLastOpenDate"));
            AppMetrica.Instance.ReportEvent("#CHEST Daily chest activate");
        }
    }

    public void OpenPotionButton()
    {
        if (NetworkTime.Check24hours(PotionlastOpenDate))
        {
            Debug.Log("Crystal Button Pressed");
            spanPotion = PotionlastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTickPotion = true;
            potionTimer.gameObject.SetActive(true);
            potionButton.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetString("PotionLastOpenDate", NetworkTime.GetNetworkTime().ToString());
            Debug.Log(DateTime.Parse(NetworkTime.GetNetworkTime().ToString()));
            Debug.Log(DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate")));
            Debug.Log(DateTime.Now);
            Debug.Log(hours24Potion);
            PotionlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));

            AppMetrica.Instance.ReportEvent("#CHEST Daily chest activate");
        }
    }

    void GiveCoinReward(int lootVol)
    {
        GameManager.AddCoins(lootVol);
        Debug.Log("RewardedCoins");
    }

    void GiveCrystalReward(int lootVol)
    {
        GameManager.AddCrystals(lootVol);
        Debug.Log("RewardedCrystals");
    }

    void GivePotionReward(int lootVol)
    {
        GameManager.AddCoins(lootVol);
        Debug.Log("RewardedPotions");
    }

    public void RewardedVideoButton()
    {
#if UNITY_EDITOR
        AdsManager.Instance.isRewardVideoWatched = true;
#elif UNITY_ANDROID || UNITY_IOS
        AdsManager.Instance.ShowRewardedVideo();
#endif
    }

    void CoinStart()
    {
        if (changeCoinDate)
        {
            PlayerPrefs.SetString("CoinLastOpenDate", "7/4/2016 8:30:52 AM");
            CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));
        }
        isTimerTickCoin = false;
        hours24Coin = (DateTime.Now.AddDays(1) - DateTime.Now);

        CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));

        is24hoursPastCoin = NetworkTime.Check24hours(CoinlastOpenDate);
        Debug.Log(is24hoursPastCoin);
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
        if (changeCoinDate)
        {
            PlayerPrefs.SetString("CrystalLastOpenDate", "7/4/2016 8:30:52 AM");
            CrystallastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CrystalLastOpenDate"));
        }
        isTimerTickCrystal = false;
        hours24Crystal = (DateTime.Now.AddDays(1) - DateTime.Now);

        CrystallastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CrystalLastOpenDate"));

        is24hoursPastCrystal = NetworkTime.Check24hours(CrystallastOpenDate);
        Debug.Log(is24hoursPastCrystal);
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
        if (changePotionDate)
        {
            PlayerPrefs.SetString("PotionLastOpenDate", "7/4/2016 8:30:52 AM");
            PotionlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));
        }
        isTimerTickPotion = false;
        hours24Potion = (DateTime.Now.AddDays(1) - DateTime.Now);

        PotionlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("PotionLastOpenDate"));

        is24hoursPastPotion = NetworkTime.Check24hours(PotionlastOpenDate);
        Debug.Log(is24hoursPastPotion);
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

}
