using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    GameObject networkWarning;

    bool isLvlEnd;

    bool fromCheckpoint;

    public bool isInterstitialClosed;

    public bool isRewardVideoWatched;

    string appKey = "3481dd986d45650597337fafb3b51bd88bc5d6862675c1d2";

    void Start ()
    {
        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);

        Appodeal.setAutoCache(Appodeal.INTERSTITIAL, false);
        Appodeal.setAutoCache(Appodeal.REWARDED_VIDEO, false);

        Appodeal.setInterstitialCallbacks(this);
        Appodeal.setRewardedVideoCallbacks(this);

        isInterstitialClosed = false;
        isRewardVideoWatched = false;
    }

    public void ShowAdsAtLevelEnd()
    {
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
        if (Appodeal.isPrecache(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        else
        {
            Appodeal.cache(Appodeal.REWARDED_VIDEO);
            Appodeal.show(Appodeal.REWARDED_VIDEO);
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
        Instantiate(networkWarning, GameObject.FindObjectOfType<UI>().gameObject.transform);

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
