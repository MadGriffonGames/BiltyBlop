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

	string appKey = "027fffae726e025f6f6e311d8e8370af0bac2f6ce6630a81";

    void Start ()
    {
        Appodeal.setAutoCache(Appodeal.INTERSTITIAL, false);
        Appodeal.setAutoCache(Appodeal.REWARDED_VIDEO, false);

        AppMetrica.Instance.ActivateWithAPIKey("cefd065c-fd53-443d-9f27-9ddf930a936f");

        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);

        Appodeal.cache(Appodeal.INTERSTITIAL);
        Appodeal.cache(Appodeal.REWARDED_VIDEO);

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
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
        else
        {
            Appodeal.cache(Appodeal.INTERSTITIAL);
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
    }

    public void ShowRewardedVideo()
    {
        Appodeal.cache(Appodeal.REWARDED_VIDEO);
        Appodeal.show(Appodeal.REWARDED_VIDEO);

        if (!Appodeal.isLoaded(Appodeal.REWARDED_VIDEO) && !isRewardVideoWatched)
        {
            if (SceneManager.GetActiveScene().name.Contains("Level"))
            {
                warning = Instantiate(networkWarningPrefab, GameObject.FindObjectOfType<UI>().gameObject.transform);
                warning.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            }
            else
            {
                warning = Instantiate(networkWarningPrefab, GameObject.Find("MainPanel").gameObject.transform);
                warning.GetComponent<RectTransform>().localScale = new Vector2(3, 3);
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
