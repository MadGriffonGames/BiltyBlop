using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DailyReward : MonoBehaviour
{
    [SerializeField]
    bool tmp;

    [SerializeField]
    GameObject rewardWindow;
    [SerializeField]
    GameObject leftButton;
    [SerializeField]
    GameObject rightButton;

    [SerializeField]
    DailyRewardSlot[] dailySlots;
 
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

    int rewardDay;
    bool is24hoursPast;
    bool isTimerTick;
    DateTime lastOpenDate;
    TimeSpan span;
    TimeSpan hours24;

    struct Reward
    {
        string type;
        int rewardValue;

        void GiveReward()
        {

        }
    }

    Reward myReward;

    private void Start()
    {
        SetRewardWindow();

        isTimerTick = false;
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);// 24hours in timespan format

        chestImage = chest.GetComponent<Image>();

        if (!PlayerPrefs.HasKey("RewardDay"))
        {
            PlayerPrefs.SetInt("RewardDay", 0);
        }

        if (!PlayerPrefs.HasKey("LastOpenDate"))
        {
            PlayerPrefs.SetString("LastOpenDate", NetworkTime.GetNetworkTime().ToString());
        }

        lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("LastOpenDate"));

        rewardDay = PlayerPrefs.GetInt("RewardDay");

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

        if (AdsManager.Instance.isRewardVideoWatched)
        {
            AdsManager.Instance.isRewardVideoWatched = false;


        }
    }

    void SetRewardWindow()
    {
        leftButton.GetComponent<Button>().enabled = false;
        Color tmp = leftButton.GetComponent<Image>().color;
        tmp -= new Color(0.05f, 0.05f, 0.05f);
        leftButton.GetComponent<Image>().color = tmp;

        rewardWindow.SetActive(false);
    }

    public void OpenChestButton()
    {
        if (NetworkTime.Check24hours(lastOpenDate))
        {
            chestImage.sprite = chestOpen;
            lightCircle.gameObject.SetActive(false);
            isSpined = false;
            activateButton.SetActive(false);
            span = lastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTick = true;
            timer.gameObject.SetActive(true);

            chestFade.SetActive(true);

            rewardDay++;
            PlayerPrefs.SetInt("RewardDay", rewardDay);

            int i;

            if (rewardDay <= 5)
            {
                i = 1;
            }
            else
            {
                i = 6;
            }

            for (int j = 0; j < dailySlots.Length; j++)
            {
                dailySlots[j].dayNum = i;
                dailySlots[j].SetReward();
                i++;
            }      

            rewardWindow.SetActive(true);

            PlayerPrefs.SetString("LastOpenDate", NetworkTime.GetNetworkTime().ToString());
            lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("LastOpenDate"));

            AppMetrica.Instance.ReportEvent("#CHEST Daily chest activate");
        }
    }

    public void ClaimButton()
    {
        chestFade.SetActive(false);
        rewardWindow.SetActive(false);
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

    public void ButtonLeft()
    {
        leftButton.GetComponent<Button>().enabled = false;
        Color tmp = leftButton.GetComponent<Image>().color;
        tmp -= new Color(0.05f, 0.05f, 0.05f);
        leftButton.GetComponent<Image>().color = tmp;

        rightButton.GetComponent<Button>().enabled = true;
        Color tmp1 = rightButton.GetComponent<Image>().color;
        tmp1 += new Color(0.05f, 0.05f, 0.05f);
        rightButton.GetComponent<Image>().color = tmp1;

        int i = 1;

        for (int j = 0; j < dailySlots.Length; j++)
        {
            dailySlots[j].dayNum = i;
            dailySlots[j].SetReward();
            i++;
        }
    }

    public void ButtonRight()
    {
        rightButton.GetComponent<Button>().enabled = false;
        Color tmp = rightButton.GetComponent<Image>().color;
        tmp -= new Color(0.05f, 0.05f, 0.05f);
        rightButton.GetComponent<Image>().color = tmp;

        leftButton.GetComponent<Button>().enabled = true;
        Color tmp1 = leftButton.GetComponent<Image>().color;
        tmp1 += new Color(0.05f, 0.05f, 0.05f);
        leftButton.GetComponent<Image>().color = tmp1;

        int i = 6;

        for (int j = 0; j < dailySlots.Length; j++)
        {
            dailySlots[j].dayNum = i;
            dailySlots[j].SetReward();
            i++;
        }
    }
}
