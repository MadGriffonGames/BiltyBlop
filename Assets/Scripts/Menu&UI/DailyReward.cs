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
    GameObject doubleButton;
    [SerializeField]
    Text rewardText;

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
    [SerializeField]
    Image rewardedButtonImage;
    [SerializeField]
    Sprite freeSprite;
    [SerializeField]
    GameObject freeText;
    [SerializeField]
    Image getRewardButtonImage;
    [SerializeField]
    Text getRewardButtonText;
    [SerializeField]
    Sprite orangeButtonSprite;
    [SerializeField]
    Sprite greenButtonSprite;


    Image chestImage;
    bool isSpined = false;
    Quaternion rotationVector;
    Animator lootAnimator;
    public static bool isRewardCollected;

    int rewardDay;
    bool is24hoursPast;
    bool isTimerTick;
    DateTime lastOpenDate;
    TimeSpan span;
    TimeSpan hours24;
    

    public class Reward//conteiner for reward, made to give it for rewarded video
    {
        public string type;
        public string itemType;
        public int rewardValue;

        public void SetReward(string _type, int _rewardValue = 0, string _itemType = null)
        {
            type = _type;
            rewardValue = _rewardValue;
            itemType = _itemType;
        }

        public void GiveReward()
        {
            switch (type)
            {
                case "Coins":
                    GameManager.AddCoins(rewardValue);
                    break;
                case "Crystals":
                    GameManager.AddCrystals(rewardValue);
                    break;
                case "Item":
                    Inventory.Instance.AddItem(itemType, rewardValue);
                    break;
                case "Pots":
                    Inventory.Instance.AddItem(Inventory.IMMORTAL_BONUS, 1);
                    Inventory.Instance.AddItem(Inventory.DAMAGE_BONUS, 1);
                    Inventory.Instance.AddItem(Inventory.SPEED_BONUS, 1);
                    Inventory.Instance.AddItem(Inventory.TIME_BONUS, 1);
                    break;
                case "Skin":
                    break;
                default:
                    break;
            }
        }
    }

    public Reward myReward;

    private void Start()
    {
        myReward = new Reward();

        SetRewardWindow();

        if (PlayerPrefs.GetInt("NoAds") > 0)
        {
            rewardedButtonImage.sprite = freeSprite;
            freeText.SetActive(true);
        }

        isTimerTick = false;
        hours24 = (DateTime.Now.AddDays(1) - DateTime.Now);// 24hours in timespan format

        chestImage = chest.GetComponent<Image>();

        if (!PlayerPrefs.HasKey("RewardDay"))
        {
            PlayerPrefs.SetInt("RewardDay", 0);
        }

        if (!PlayerPrefs.HasKey("LastOpenDate"))
        {
            PlayerPrefs.SetString("LastOpenDate", "7/4/2016 8:30:52 AM");
            lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("LastOpenDate"));
        }

        lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("LastOpenDate"));

        rewardDay = PlayerPrefs.GetInt("RewardDay");
        if (rewardDay == 9)
        {
            doubleButton.GetComponent<Button>().interactable = false;            
        }

        is24hoursPast = NetworkTime.Check24hours(lastOpenDate);

        if (!is24hoursPast)
        {
            doubleButton.GetComponent<Button>().interactable = false;
            chestImage.sprite = chestClose;
            lightCircle.gameObject.SetActive(false);
            isSpined = false;
            span = lastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTick = true;
            timer.gameObject.SetActive(true);
            getRewardButtonImage.sprite = orangeButtonSprite;
            getRewardButtonText.text = "ok";
            isRewardCollected = true;
        }
        else
        {
            ActivateChest();
            isRewardCollected = false;
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
            isRewardCollected = false;
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

            rewardText.text = (int.Parse(rewardText.text)*2).ToString();
            myReward.GiveReward();
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


        chestFade.SetActive(true);

        if (!isRewardCollected)
        {
            rewardDay++;
            PlayerPrefs.SetInt("RewardDay", rewardDay);

            getRewardButtonImage.sprite = greenButtonSprite;
            getRewardButtonText.text = "get";
            doubleButton.GetComponent<Button>().interactable = true;

            chestImage.sprite = chestOpen;
            lightCircle.gameObject.SetActive(false);
            isSpined = false;
            span = lastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTick = true;
            timer.gameObject.SetActive(true);

            PlayerPrefs.SetString("LastOpenDate", NetworkTime.GetNetworkTime().ToString());
            lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("LastOpenDate"));

            AppMetrica.Instance.ReportEvent("#CHEST Daily chest activate");
            DevToDev.Analytics.CustomEvent("#CHEST Daily chest activate");
        }       

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

    public void RewardedVideoButton()
    {
        AppMetrica.Instance.ReportEvent("#REWARDx2_BUTTON pressed");
        DevToDev.Analytics.CustomEvent("#REWARDx2_BUTTON pressed");

#if UNITY_EDITOR
        AdsManager.Instance.isRewardVideoWatched = true;
#elif UNITY_ANDROID || UNITY_IOS
        AdsManager.Instance.ShowRewardedVideo();
#endif
        doubleButton.GetComponent<Button>().interactable = false;
    }
}
