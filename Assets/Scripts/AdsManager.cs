using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine;

public class AdsManager : MonoBehaviour, IInterstitialAdListener, IRewardedVideoAdListener
{
    private static AdsManager instance;
    public static AdsManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<AdsManager>();
            return instance;
        }
    }

    [SerializeField]
    public GameObject networkWarningPrefab;

    GameObject warning;

    bool isLvlEnd;

    public bool fromShowfunction = false;

    public bool isInterstitialClosed = false;

    public bool isRewardVideoWatched = false;

    string appKey;

    void Start ()
    {

#if UNITY_EDITOR

#elif UNITY_ANDROID
    appKey = "e98a9abebc918269e0b487f18fd271b1313447f412d4561e";

#elif UNITY_IOS
    appKey = "027fffae726e025f6f6e311d8e8370af0bac2f6ce6630a81";

#endif

        AppMetrica.Instance.ActivateWithAPIKey("cefd065c-fd53-443d-9f27-9ddf930a936f");

        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);

        Appodeal.setInterstitialCallbacks(this);
        Appodeal.setRewardedVideoCallbacks(this);

        isInterstitialClosed = false;
        isRewardVideoWatched = false;
    }

    public void ShowAdsAtLevelEnd()
    {
        fromShowfunction = true;
        if (Appodeal.isPrecache(Appodeal.INTERSTITIAL))
        {
            if (PlayerPrefs.GetInt("NoAds") == 0)
            {
                Appodeal.show(Appodeal.INTERSTITIAL);
            }
            else
            {
                isInterstitialClosed = true;
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("NoAds") == 0)
            {
                Appodeal.show(Appodeal.INTERSTITIAL);
            }
            else
            {
                isInterstitialClosed = true;
            }
        }
    }

    public void ShowRewardedVideo()
    {
        Appodeal.cache(Appodeal.REWARDED_VIDEO);

        if (PlayerPrefs.GetInt("NoAds") == 0)
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        else
        {
            isRewardVideoWatched = true;
        }

        if (PlayerPrefs.GetInt("NoAds") == 0 && !Appodeal.isLoaded(Appodeal.REWARDED_VIDEO) && !isRewardVideoWatched)
        {
            if (SceneManager.GetActiveScene().name.Contains("Level"))
            {
#if !UNITY_IOS
                if (GameObject.Find("NetworkWarning(Clone)") == null)//if there is no active warnings in scene
                {
                    warning = Instantiate(networkWarningPrefab, GameObject.FindObjectOfType<UI>().gameObject.transform);
                    warning.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                }
#endif
            }
            else
            {
#if !UNITY_IOS
                if (GameObject.Find("NetworkWarning(Clone)") == null)//if there is no active warnings in scene
                {
                    warning = Instantiate(networkWarningPrefab, GameObject.FindObjectOfType<UI>().gameObject.transform);
                    warning.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                }
#endif
            }
            warning.GetComponent<RectTransform>().localPosition = new Vector2();
        }

    }

    /*
     * Interstitial callback handlers
     */

    public void onInterstitialLoaded()
    {
        print("Interstitial loaded");
    }

    public void onInterstitialFailedToLoad()
    {
        print("Interstitial failed");
        isInterstitialClosed = true;
    }

    public void onInterstitialShown()
    {
        print("Interstitial opened");
    }

    public void onInterstitialClosed()
    {
        isInterstitialClosed = true;
    }

    public void onInterstitialClicked()
    {
        print("Interstitial clicked");
    }

    /*
     * Rewarded video callback handlers
     */

    public void onRewardedVideoLoaded()
    {
        print("Video loaded");
    }

    public void onRewardedVideoFailedToLoad()
    {
        print("Video failed");
    }

    public void onRewardedVideoShown()
    {
        print("Video shown");
    }

    public void onRewardedVideoClosed()
    {
        print("Video closed");
    }

    public void onRewardedVideoFinished(int amount, string name)
    {
        isRewardVideoWatched = true;
    }
}
