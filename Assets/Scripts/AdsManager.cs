using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
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
    public bool isRewardedVideoFailed = false;
    public bool isRewardedVideoShown = false;

    string appKey;
    string unityGameId;

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

#elif UNITY_IOS
		appKey = "c9868b381a1331f2a7d3d5b4dd9bc721f403735f82deebff";
        unityGameId = "1671704";

#endif

        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);
        Advertisement.Initialize(unityGameId);

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

    /*
     * Interstitial callback handlers
     */

    public void onInterstitialLoaded(bool var)
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
        isRewardedVideoFailed = true;
    }

    public void onRewardedVideoShown()
    {
        isRewardedVideoShown = true;
    }

    public void onRewardedVideoClosed(bool var)
    {
        print("Video closed");
    }

    public void onRewardedVideoFinished(int amount, string name)
    {
        isRewardVideoWatched = true;
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
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("rewardedVideo", options);
    }

    bool CanNotShowRewardedVideo()
    {
        return PlayerPrefs.GetInt("NoAds") == 0 && !Appodeal.isLoaded(Appodeal.REWARDED_VIDEO) && !isRewardVideoWatched;
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
