using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUI : MonoBehaviour
{
    int coinsCollected;
    [SerializeField]
    Text coinsText;
    [SerializeField]
    GameObject[] starsFull;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject controlsUI;
    [SerializeField]
    GameObject pauseUI;
    [SerializeField]
    ChestUI chest;
    [SerializeField]
    GameObject videoButton;
    [SerializeField]
    GameObject freeButton;
    bool isButtonPressed;

    void Start ()
    {
        isButtonPressed = false;
        if (PlayerPrefs.GetInt("NoAds") > 0)
        {
            videoButton.SetActive(false);
            freeButton.SetActive(true);
        }

        controlsUI.SetActive(false);
        pauseUI.SetActive(false);
        SoundManager.Instance.currentMusic.Stop();
        SoundManager.PlayMusic("victory sound", false);
        fade.SetActive(true);

        coinsCollected = GameManager.lvlCollectedCoins;
        coinsText.text = "" + coinsCollected;
        StartCoroutine(ShowStars(Player.Instance.stars));
    }

    private void Update()
    {
        if (AdsManager.Instance.isInterstitialClosed)
        {
            AdsManager.Instance.isInterstitialClosed = false;
            SceneManager.LoadScene("Loading");
        }

        if (isButtonPressed && AdsManager.Instance.isRewardVideoWatched)
        {
            isButtonPressed = false;
            AdsManager.Instance.isRewardVideoWatched = false;

            GameManager.AddCoins(int.Parse(coinsText.text));
            coinsText.text = (int.Parse(coinsText.text) * 2).ToString();
            videoButton.GetComponent<Button>().interactable = false;
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        if (GameManager.currentLvl == "Level30")
        {
            GameManager.nextLevelName = "MainMenu";
        }
        SceneManager.LoadScene("Loading");
    }

    public void DoubleButton()
    {
        if (PlayerPrefs.GetInt("NoAds") > 0)
        {
            GameManager.AddCoins(int.Parse(coinsText.text));
            coinsText.text = (int.Parse(coinsText.text) * 2).ToString();
            freeButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            isButtonPressed = true;

#if UNITY_EDITOR
            AdsManager.Instance.isRewardVideoWatched = true;
#elif UNITY_ANDROID || UNITY_IOS
            AdsManager.Instance.ShowRewardedVideo();
#endif
        }
    }

    public void Restart()
    {
        GameManager.nextLevelName = SceneManager.GetActiveScene().name;

#if UNITY_EDITOR
        AdsManager.Instance.isInterstitialClosed = true;

#elif UNITY_ANDROID
        AdsManager.Instance.ShowAdsAtLevelEnd();//check if ad was showed in update()

#elif UNITY_IOS
        AdsManager.Instance.ShowAdsAtLevelEnd();//check if ad was showed in update()

#endif


        GameManager.collectedCoins = Player.Instance.startCoinCount;
        GameManager.nextLevelName = SceneManager.GetActiveScene().name;
    }

    public IEnumerator ShowStars(int value)
    {
        for (int i = 0; i < value; i++)
        {
            starsFull[i].SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
