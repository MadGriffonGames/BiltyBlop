using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DailyReward : MonoBehaviour, IAdsPlacement
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
    GameObject CloseButton;

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
    GameObject getRewardButton;
    bool musicWasPlaying;



    Image chestImage;
    bool isSpined = false;
    Quaternion rotationVector;
    Animator lootAnimator;
    public static bool isRewardCollected;

    int rewardDay;
    bool is24hoursPast;
    bool isTimerTick;
    bool _isRewardedVideoWatched;
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
        hours24 = new TimeSpan(24, 0, 0);// 24hours in timespan format

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

        if (rewardDay == 10)
        {
            doubleButton.GetComponent<Button>().interactable = false;            
        }

        is24hoursPast = NetworkTime.Check24hours(lastOpenDate);

        if (!is24hoursPast)
        {
            chest.GetComponent<Animator>().enabled = false;
            doubleButton.GetComponent<Button>().interactable = false;
            chestImage.sprite = chestClose;
            lightCircle.gameObject.SetActive(false);
            isSpined = false;
            span = lastOpenDate - NetworkTime.GetNetworkTime();
            isTimerTick = true;
            timer.gameObject.SetActive(true);
            isRewardCollected = true;
        }
        else
        {
            ActivateChest();
            isRewardCollected = false;
        }

        _isRewardedVideoWatched = false;
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
        chest.GetComponent<Animator>().enabled = false;

        chestFade.SetActive(true);

        if (!isRewardCollected)
        {
            rewardDay++;
            PlayerPrefs.SetInt("RewardDay", rewardDay);

            getRewardButton.SetActive(true);
            CloseButton.SetActive(false);
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
        else
        {
            getRewardButton.SetActive(false);
            CloseButton.SetActive(true);
            doubleButton.GetComponent<Button>().interactable = false;
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
        chest.GetComponent<Animator>().enabled = true;
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
        AppMetrica.Instance.ReportEvent("#DAILY_REWARDx2_BUTTON pressed");
        DevToDev.Analytics.CustomEvent("#DAILY_REWARDx2_BUTTON pressed");

        _isRewardedVideoWatched = true;
        if (PlayerPrefs.GetInt("MusicIsOn") == 1)
        {
            musicWasPlaying = true;
            SoundManager.Instance.currentMusic.Stop();
        }

        //AdsManager.Instance.reservedPlacement = this;
        AdsManager.Instance.ShowRewardedVideo(this);
    }

    public void OnRewardedVideoWatched()
    {
        doubleButton.GetComponent<Button>().interactable = false;

        AppMetrica.Instance.ReportEvent("#DAILY_REWARDx2_VIDEO watched");
        DevToDev.Analytics.CustomEvent("#DAILY_REWARDx2_VIDEO watched");

        rewardText.text = (int.Parse(rewardText.text) * 2).ToString();

        myReward.GiveReward();
    }

    public void OnRewardedVideoFailed()
    {

    }
}
