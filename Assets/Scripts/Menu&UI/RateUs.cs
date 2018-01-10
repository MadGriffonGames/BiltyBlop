using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUs : MonoBehaviour
{
    [SerializeField]
    GameObject rateUsWindow;
    [SerializeField]
    GameObject fade;

    const int FIRST_APPEAR_DELAY = 3;
    const int REMIND_LATER_DELAY = 2;

    const string IOS_URL = "item-apps://itunes.apple.com/app/idcom.hardslime.kidarian";
    const string ANDROID_URL = "market://details?id=com.hardslime.kidarian";

    const string IS_RATE_US_WINDOW_APPEARED = "IsRateUsWindowAppears";
    const string APP_ENTER_COUNTER = "AppEnterCounter";

    private void Start()
    {
        if (PlayerPrefs.GetInt("Rated") > 0)
        {
            rateUsWindow.SetActive(false);
            fade.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetInt(IS_RATE_US_WINDOW_APPEARED) > 0)
            {
                if (PlayerPrefs.GetInt(APP_ENTER_COUNTER) >= REMIND_LATER_DELAY)
                {
                    rateUsWindow.SetActive(true);
                    fade.SetActive(true);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt(APP_ENTER_COUNTER) >= FIRST_APPEAR_DELAY)
                {
                    PlayerPrefs.SetInt(IS_RATE_US_WINDOW_APPEARED, 1);
                    rateUsWindow.SetActive(true);
                    fade.SetActive(true);
                }
            }
        }
    }

    public void RateButton()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        Application.OpenURL(ANDROID_URL);
#elif UNITY_IOS
        Application.OpenURL(IOS_URL);
#endif

        PlayerPrefs.SetInt("Rated", 1);
        rateUsWindow.SetActive(false);
        fade.SetActive(false);
    }

    public void RemindLaterButton()
    {
        PlayerPrefs.SetInt(APP_ENTER_COUNTER, 0);
        rateUsWindow.SetActive(false);
        fade.SetActive(false);
    }

    public void DontShowAgainButton()
    {
        PlayerPrefs.SetInt("Rated", 1);
        rateUsWindow.SetActive(false);
        fade.SetActive(false);
    }

    public static void IncrementAppEnterCounter()//Used in Awake() in PurchaseManager
    {
        PlayerPrefs.SetInt(APP_ENTER_COUNTER, PlayerPrefs.GetInt(APP_ENTER_COUNTER) + 1);
    }
}
