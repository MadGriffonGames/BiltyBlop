﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour
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
    public bool isRewardedVideoFailed = false;
    public bool isRewardedVideoShown = false;

    InterstitialAd interstitial;

    string appKey;
    string unityGameId;
    string adMobAppId;

    private void Awake()
    {
        AppMetrica.Instance.ActivateWithAPIKey("cefd065c-fd53-443d-9f27-9ddf930a936f");
    }

    void Start ()
    {

#if UNITY_EDITOR

#elif UNITY_ANDROID
        appKey = "e98a9abebc918269e0b487f18fd271b1313447f412d4561e";
        unityGameId = "1671703";
        adMobAppId = "ca-app-pub-7702587672519508~1294300679";

#elif UNITY_IOS
		appKey = "c9868b381a1331f2a7d3d5b4dd9bc721f403735f82deebff";
        unityGameId = "1671704";
        adMobAppId = "";

#endif

        Advertisement.Initialize(unityGameId);
        MobileAds.Initialize(adMobAppId);

        RequestInterstitial();

        isInterstitialClosed = false;
        isRewardVideoWatched = false;
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void ShowAdsAtLevelEnd()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            RequestInterstitial();
        }
        else
        {
            RequestInterstitial();
        }
    }

    public void ShowRewardedVideo()
    {
        if (PlayerPrefs.GetInt("NoAds") == 0)
        {
            int tmp = Random.Range(1, 3);
            if (tmp == 1)
            {
                ShowRewardedVideoUnityAds();
            }
            else
            {
                ShowRewardedVideoUnityAds();
            }
        }
        else
        {
            isRewardVideoWatched = true;
        }

        if (CanNotShowRewardedVideo())
        {
            InstantiateWarning();
        }

    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            isRewardVideoWatched = true;
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }


    //other

    void ShowRewardedVideoUnityAds()
    {
        if (Advertisement.IsReady())
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            Advertisement.Show("rewardedVideo", options);
        }
    }

    bool CanNotShowRewardedVideo()
    {
        return PlayerPrefs.GetInt("NoAds") == 0 && Application.internetReachability == NetworkReachability.NotReachable;
    }

    void InstantiateWarning()
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
                GameObject ui;
                ui = GameObject.FindGameObjectWithTag("UI").gameObject;

                warning = Instantiate(networkWarningPrefab, ui.transform);
                warning.GetComponent<RectTransform>().localScale = new Vector2(2, 2);
            }
#endif
        }
        warning.GetComponent<RectTransform>().localPosition = new Vector2();
    }


}
