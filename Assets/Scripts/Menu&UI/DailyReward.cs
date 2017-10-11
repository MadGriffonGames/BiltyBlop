using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DailyReward : RewardedChest
{
    [SerializeField]
    bool tmp;
 
    [SerializeField]
    Image lightCircle;
    [SerializeField]
    GameObject chest;
    [SerializeField]
    Sprite chestOpen;
    [SerializeField]
    Sprite chestClose;
    [SerializeField]
    GameObject activateButton;
    [SerializeField]
    GameObject chestFade;
    [SerializeField]
    Text timer;


    Image chestImage;
    bool isSpined = false;
    Quaternion rotationVector;
    Animator lootAnimator;

    bool is24hoursPast;
    bool isTimerTick;
    DateTime lastOpenDate;
    TimeSpan span;
    TimeSpan hours24;

    private void Start()
    {
        base.Start();

        isTimerTick = false;
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);// 24hours in timespan format

        chestImage = chest.GetComponent<Image>();
        lootAnimator = loot.gameObject.GetComponent<Animator>();

        if (!PlayerPrefs.HasKey("LastOpenDate"))
        {
            PlayerPrefs.SetString("LastOpenDate", NetworkTime.GetNetworkTime().ToString());
        }
        lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("LastOpenDate"));

        is24hoursPast = NetworkTime.Check24hours(lastOpenDate);

        if (!is24hoursPast)
        {
            chestImage.sprite = chestClose;
            lightCircle.gameObject.SetActive(false);
            isSpined = false;
            chestImage.color = new Color(chestImage.color.r, chestImage.color.g, chestImage.color.b, 0.55f);
            activateButton.SetActive(false);
            span = lastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTick = true;
            timer.gameObject.SetActive(true);
        }
        else
        {
            ActivateChest();
        }
    }

    public void TMP()
    {
        tmp = true;
    }

    private void Update()
    {
        if (isSpined)
        {
            rotationVector = lightCircle.rectTransform.rotation;
            rotationVector.z -= 0.0005f;
            lightCircle.rectTransform.rotation = rotationVector;
        }

        if (AdsManager.Instance.isRewardVideoWatched)
        {
            AdsManager.Instance.isRewardVideoWatched = false;
            PlayerPrefs.SetString("LastOpenDate", NetworkTime.GetNetworkTime().ToString());
            lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("LastOpenDate"));
            GiveLoot();

            AppMetrica.Instance.ReportEvent("#CHEST Daily chest activate");
        }

        if (tmp)
        {
            PlayerPrefs.SetString("LastOpenDate", "7/4/2016 8:30:52 AM");
            lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("LastOpenDate"));
            tmp = false;
        }

        if (isTimerTick)
        {
            span = hours24 + (lastOpenDate - DateTime.Now);
            timer.text = span.ToString().Substring(0, 8);
            if (span <= TimeSpan.Zero)
            {
                ActivateChest();
            }
        }
    }

    public void OpenChestButton()
    {

        if (NetworkTime.Check24hours(lastOpenDate))
        {
#if UNITY_EDITOR
            AdsManager.Instance.isRewardVideoWatched = true;

#elif UNITY_ANDROID
        AdsManager.Instance.ShowRewardedVideo();//check if ad was showed in update()

#elif UNITY_IOS
        AdsManager.Instance.ShowRewardedVideo();//check if ad was showed in update()

#endif
        }
    }

    public void GiveLoot()
    {
        Randomize();
        chestImage.sprite = chestOpen;
        chestFade.SetActive(true);
        loot.gameObject.SetActive(true);
        activateButton.SetActive(false);
        isTimerTick = true;
        timer.gameObject.SetActive(true);
    }

    public void CollectLoot()
    {
        chestFade.SetActive(false);
        loot.gameObject.SetActive(false);
    }

    void ActivateChest()
    {
        chestImage.sprite = chestClose;
        chestImage.color = new Color(chestImage.color.r, chestImage.color.g, chestImage.color.b, 1);
        lightCircle.gameObject.SetActive(true);
        isSpined = true;
        activateButton.SetActive(true);
        isTimerTick = false;
        timer.gameObject.SetActive(false);
    }

    void AddCoins(int value)
    {
        loot.gameObject.GetComponentInChildren<Text>().text = value.ToString();
        GameManager.collectedCoins += value;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + value);
    }

    void AddCrystals(int value)
    {
        loot.gameObject.GetComponentInChildren<Text>().text = value.ToString();
        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + value);
    }
}
