using System.Collections;
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

    public bool isInterstitialClosed = false;
    public bool isRewardVideoWatched = false;
    public bool isRewardedVideoFailed = false;
    public bool isRewardedVideoShown = false;

    InterstitialAd interstitial;
    private RewardBasedVideoAd adMobRewardedVideo;

    string appKey;
    string unityGameId;
    string adMobAppId;

    private void Awake()
    {
        AppMetrica.Instance.ActivateWithAPIKey("cefd065c-fd53-443d-9f27-9ddf930a936f");
    }

    void Start ()
    {

#if UNITY_ANDROID
        appKey = "e98a9abebc918269e0b487f18fd271b1313447f412d4561e";
        unityGameId = "1671703";
        adMobAppId = "ca-app-pub-7702587672519508~1294300679";

#elif UNITY_IOS
		appKey = "c9868b381a1331f2a7d3d5b4dd9bc721f403735f82deebff";
        unityGameId = "1671704";
        adMobAppId = "ca-app-pub-7702587672519508~6806924428";

#endif

        Advertisement.Initialize(unityGameId);
        MobileAds.Initialize(adMobAppId);

        RequestInterstitial();
        RequestRewardedVideo();

        isInterstitialClosed = false;
        isRewardVideoWatched = false;
    }

    void RequestRewardedVideo()
    {
        adMobRewardedVideo = RewardBasedVideoAd.Instance;

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7702587672519508/5527360718";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-7702587672519508/4520985510";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Called when the user should be rewarded for watching a video.
        adMobRewardedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Load the rewarded video ad with the request.
        this.adMobRewardedVideo.LoadAd(request, adUnitId);
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7702587672519508/4784645751";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-7702587672519508/2187779299";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
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
            isInterstitialClosed = true;
        }
    }

    public void ShowRewardedVideo()
    {
        Debug.Log(Advertisement.IsReady());
        if (CanNotShowRewardedVideo())
        {
            InstantiateWarning();
        }
        else
        {
            if (PlayerPrefs.GetInt("NoAds") == 0)
            {
                int tmp = Random.Range(1, 3);
                Debug.Log(tmp);
                if (tmp == 1)
                {
                    Debug.Log("Unity");
                    if (Advertisement.IsReady())
                    {
                        UnityAdsShowRewardedVideo();
                    }
                    else
                    {
                        if (adMobRewardedVideo.IsLoaded())
                        {
                            Debug.Log("AdMob");
                            AdMobShowRewardedVideo();
                        }
                    }
                }
                else
                {
                    if (adMobRewardedVideo.IsLoaded())
                    {
                        Debug.Log("AdMob");
                        AdMobShowRewardedVideo();
                    }
                    else
                    {
                        Debug.Log("Unity");
                        if (Advertisement.IsReady())
                        {
                            UnityAdsShowRewardedVideo();
                        }
                    }
                }
            }
            else
            {
                isRewardVideoWatched = true;
            }
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

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        isInterstitialClosed = true;
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        isRewardVideoWatched = true;

        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    }

    //other

    void UnityAdsShowRewardedVideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("rewardedVideo", options);
    }

    void AdMobShowRewardedVideo()
    {
        adMobRewardedVideo.Show();
        RequestRewardedVideo();
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
