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
    bool musicWasPlaying;
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
            chest.transform.rotation = Quaternion.Euler(0, 0, 0);

            GiveLoot();

            isRewardCollected = true;
            if (musicWasPlaying)
            {
                musicWasPlaying = false;
                PlayerPrefs.SetInt("MusicIsOn", 1);
                SoundManager.MuteMusic(false);
            }

            if (PlayerPrefs.GetInt("TutorialMode") > 0)
            {
                GetComponent<ChestTutorial>().DisableTutorial();
                PlayerPrefs.SetInt(SceneTutorial.CHEST_TUTORIAL_COMPLETE, 1);
                DevToDev.Analytics.Tutorial(3);
            }

            AppMetrica.Instance.ReportEvent("#MAP_CHEST activate");
            DevToDev.Analytics.CustomEvent("#MAP_CHEST activate");
        }
    }

    public void OpenChestButton()
    {
        AdsManager.Instance.ShowRewardedVideo();
        if (PlayerPrefs.GetInt("MusicIsOn") == 1)
        {
            musicWasPlaying = true;
            PlayerPrefs.SetInt("MusicIsOn", 0);
            SoundManager.MuteMusic(true);
        }
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
            chest.transform.rotation = Quaternion.Euler(0,0,0);
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
