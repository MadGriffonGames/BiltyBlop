using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    Image pauseMenu;
    [SerializeField]
    Image warning;
    [SerializeField]
    GameObject controls;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject warningFade;
    [SerializeField]
    GameObject pauseButton;
    [SerializeField]
    GameObject continueButton;

    private void Update()
    {
        if (AdsManager.Instance.isInterstitialClosed && AdsManager.Instance.fromShowfunction)
        {
            AdsManager.Instance.isInterstitialClosed = false;
            SceneManager.LoadScene("Loading");
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        buttonsSetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        buttonsSetActive(false);
    }

    public void RWRD()
    {
        AdsManager.Instance.ShowRewardedVideo();
    }

    public void Restart()
    {
        Time.timeScale = 1;

#if UNITY_EDITOR
        AdsManager.Instance.isInterstitialClosed = true;
        AdsManager.Instance.fromShowfunction = true;

#elif UNITY_ANDROID
        AdsManager.Instance.ShowAdsAtLevelEnd();//check if ad was showed in update()

#elif UNITY_IOS
        AdsManager.Instance.ShowAdsAtLevelEnd();//check if ad was showed in update()

#endif

        GameManager.collectedCoins = Player.Instance.startCoinCount;
        GameManager.nextLevelName = SceneManager.GetActiveScene().name;
    }

    public void WarnActive()
    {
        warningFade.SetActive(true);
        warningSetActive(true);
    }

    public void WarnOff()
    {
        warningFade.SetActive(false);
        warningSetActive(false);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    void warningSetActive(bool quit)
    {
        warning.gameObject.SetActive(quit);
    }

    void buttonsSetActive(bool pause) // переключатель кнопок
    {
        pauseMenu.gameObject.SetActive(pause);
        fade.SetActive(pause);
        controls.SetActive(!pause);
        pauseButton.SetActive(!pause);
        continueButton.SetActive(pause);
    }

}
