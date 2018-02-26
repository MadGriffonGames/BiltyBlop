using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RateUs : MonoBehaviour
{
    [SerializeField]
    GameObject rateUsWindow;
    [SerializeField]
    GameObject fade;

    const int FIRST_APPEAR_DELAY = 5;
    const int REMIND_LATER_DELAY = 3;

    const string IOS_URL = "item-apps://itunes.apple.com/app/idcom.hardslime.kidarian";
    const string ANDROID_URL = "market://details?id=com.hardslime.kidarian";

    const string IS_RATE_US_WINDOW_APPEARED = "IsRateUsWindowAppears";
    const string APP_ENTER_COUNTER = "AppEnterCounter";

    private void Start()
    {
        if (PlayerPrefs.GetInt("Rated") > 0 || PlayerPrefs.GetInt("NoAds") > 0)
        {
            EnableRateWindow(false);
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Map")
            {
                if (PlayerPrefs.GetString("LastCompletedLevel") == "Level5" && PlayerPrefs.GetInt("RatedAfetrLevel5") == 0)
                {
                    PlayerPrefs.SetInt("RatedAfetrLevel5", 1);
                    EnableRateWindow(true);
                }
                else if (PlayerPrefs.GetString("LastCompletedLevel") == "Level9" && PlayerPrefs.GetInt("RatedAfetrLevel9") == 0)
                {
                    PlayerPrefs.SetInt("RatedAfetrLevel9", 1);
                    EnableRateWindow(true);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt(IS_RATE_US_WINDOW_APPEARED) > 0)
                {
                    if (PlayerPrefs.GetInt(APP_ENTER_COUNTER) >= REMIND_LATER_DELAY)
                    {
                        EnableRateWindow(true);
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt(APP_ENTER_COUNTER) >= FIRST_APPEAR_DELAY)
                    {
                        PlayerPrefs.SetInt(IS_RATE_US_WINDOW_APPEARED, 1);
                        EnableRateWindow(true);
                    }
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
        EnableRateWindow(false);

        AppMetrica.Instance.ReportEvent("#RATE_US_RATED");
        DevToDev.Analytics.CustomEvent("#RATE_US_RATED");
    }

    public void RemindLaterButton()
    {
        PlayerPrefs.SetInt(APP_ENTER_COUNTER, 0);
        EnableRateWindow(false);

        AppMetrica.Instance.ReportEvent("#RATE_US_REMIND_LATER");
        DevToDev.Analytics.CustomEvent("#RATE_US_REMIND_LATER");
    }

    public void DontShowAgainButton()
    {
        PlayerPrefs.SetInt("Rated", 1);
        EnableRateWindow(false);

        AppMetrica.Instance.ReportEvent("#RATE_US_DONT_REMIND");
        DevToDev.Analytics.CustomEvent("#RATE_US_DONT_REMIND");
    }

    public static void IncrementAppEnterCounter()//Used in Awake() in PurchaseManager
    {
        PlayerPrefs.SetInt(APP_ENTER_COUNTER, PlayerPrefs.GetInt(APP_ENTER_COUNTER) + 1);
    }

    void EnableRateWindow(bool enable)
    {
        rateUsWindow.SetActive(enable);
        fade.SetActive(enable);
    }
}
