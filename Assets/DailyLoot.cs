using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DailyLoot : MonoBehaviour {

    DateTime CoinlastOpenDate;
    TimeSpan spanCoin;
    TimeSpan hours24Coin;
    bool is24hoursPastCoin;
    bool isTimerTickCoin;

    [SerializeField]
    GameObject coinButton;
    [SerializeField]
    Text timer;



    // Use this for initialization
    void Start () {
        PlayerPrefs.SetString("CoinLastOpenDate", "7/4/2016 8:30:52 AM");
        CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("LastOpenDate"));
        isTimerTickCoin = false;
        hours24Coin = (DateTime.Now.AddDays(1) - DateTime.Now);

        CoinlastOpenDate = DateTime.Parse(PlayerPrefs.GetString("CoinLastOpenDate"));

        is24hoursPastCoin = NetworkTime.Check24hours(CoinlastOpenDate);
        Debug.Log(is24hoursPastCoin);
        if (!is24hoursPastCoin)
        {
            coinButton.gameObject.SetActive(false);
            isTimerTickCoin = true;
        }
        else
        {
            ActivateCoinLoot();
        }
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
            spanCoin = hours24Coin + (CoinlastOpenDate - DateTime.Now);
            timer.text = spanCoin.ToString().Substring(0, 10);
            if (spanCoin <= TimeSpan.Zero)
            {
                ActivateCoinLoot();
            }
        }
    }

    void ActivateCoinLoot()
    {
        coinButton.gameObject.SetActive(true);
    }

    public void OpenCoinButton()
    {
        if (NetworkTime.Check24hours(CoinlastOpenDate))
        {
            Debug.Log("Button Pressed");
            spanCoin = CoinlastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTickCoin = true;
        }
    }

    void GiveCoinReward(int lootVol)
    {
        GameManager.AddCoins(lootVol);
        Debug.Log("Rewarded");
    }

    public void RewardedVideoButton()
    {
#if UNITY_EDITOR
        AdsManager.Instance.isRewardVideoWatched = true;
#elif UNITY_ANDROID || UNITY_IOS
        AdsManager.Instance.ShowRewardedVideo();
#endif
    }

}
