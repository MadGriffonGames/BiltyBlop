using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChestUI : RewardedChest
{
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
    Text text;

    Image chestImage;
    bool isSpined;
    Quaternion rotationVector;
    Animator lootAnimator;
    bool isOpened;
    public bool isRewardCollected;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Level2") == 0)
        {
            this.gameObject.SetActive(false);
        }

        chestImage = chest.GetComponent<Image>();
        lootAnimator = loot.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        base.Start();

        CheckIsChestOpen();

        isRewardCollected = false;
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
            PlayerPrefs.SetInt("IsMapChestOpen", 1);
            chest.GetComponent<Animator>().enabled = false;

            GiveLoot();

            isRewardCollected = true;

            if (PlayerPrefs.GetInt("TutorialMode") == 1)
            {
                GetComponent<ChestTutorial>().DisableTutorial();
            }

            AppMetrica.Instance.ReportEvent("#MAP_CHEST activate");
            DevToDev.Analytics.CustomEvent("#MAP_CHEST activate");
        }
    }

    public void OpenChestButton()
    {
        AdsManager.Instance.ShowRewardedVideo();
    }

    public void GiveLoot()
    {
        Randomize();
        chestImage.sprite = chestOpen;
        chestFade.SetActive(true);
        loot.gameObject.SetActive(true);
        activateButton.SetActive(false);
    }

    public void CollectLoot()
    {
        chestFade.SetActive(false);
        loot.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CheckIsChestOpen();
    }

    void CheckIsChestOpen()
    {
        isOpened = PlayerPrefs.GetInt("IsMapChestOpen") > 0;

        if (isOpened)
        {
            chestImage.sprite = chestOpen;
            activateButton.SetActive(false);
            chest.GetComponent<Animator>().enabled = false;
        }
        else
        {
            chestImage.sprite = chestClose;
            lightCircle.gameObject.SetActive(true);
            isSpined = true;
            activateButton.SetActive(true);
        }
    }
}
