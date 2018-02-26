using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections;

public class FacebookManager : MonoBehaviour
{
    string shareURL;
    public Text userIdText;
    [SerializeField]
    GameObject shareBar;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject rewardFade;
    [SerializeField]
    GameObject reward;
    [SerializeField]
    GameObject giantButton;
    [SerializeField]
    GameObject freeGiftFB;
    [SerializeField]
    GameObject freeGiftTwit;


    //twitter zone
    public string twitterNameParameter = "Check this amazing game!";
    public string twitterDescriptionParam = "";
    public const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWITTER_LANGUAGE = "en";

    private const string FACEBOOK_SHARE = "FacebookShare";
    private const string TWITTER_SHARE = "TwitterShare";


    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
#if UNITY_ANDROID
        shareURL = "https://play.google.com/store/apps/details?id=com.hardslime.kidarian";

#elif UNITY_IOS
        shareURL = "https://www.hardslime.com/";
#endif
    }

    private void Start()
    {
        PlayerPrefs.SetInt(FACEBOOK_SHARE, 0);
        PlayerPrefs.SetInt(TWITTER_SHARE, 0);
        if (PlayerPrefs.GetInt(FACEBOOK_SHARE) > 0)
        {
            freeGiftFB.SetActive(false);
        }

        if (PlayerPrefs.GetInt(TWITTER_SHARE) > 0)
        {
            freeGiftTwit.SetActive(false);
        }
    }

    public void LogIn()
    {
        FB.LogInWithReadPermissions(callback:OnLogIn);
    }

    private void OnLogIn(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = AccessToken.CurrentAccessToken;
            userIdText.text = token.UserId;
        }
        else
            Debug.Log("Canceled Login");
    }

    public void Share()
    {
        FB.ShareLink(contentTitle:"Look, this game is awesome!", 
            contentURL: new System.Uri(shareURL), 
            contentDescription: "This is the best game I've ever seen!", 
            callback: OnShare);
    }

    private void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Sharing error: " + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("Share is fine");
            if (!PlayerPrefs.HasKey(FACEBOOK_SHARE))
            {
                PlayerPrefs.SetInt((FACEBOOK_SHARE), 0);
            }
            PlayerPrefs.SetInt(FACEBOOK_SHARE, PlayerPrefs.GetInt(FACEBOOK_SHARE) + 1);
            if (PlayerPrefs.GetInt(FACEBOOK_SHARE) == 1)
            {
                freeGiftFB.SetActive(false);
                StartCoroutine(GiveReward());
            }
        }
    }

    public void PressedTwitterButton()
    {
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(twitterNameParameter + "\n" + twitterDescriptionParam + "\n" + shareURL));
        if (!PlayerPrefs.HasKey(TWITTER_SHARE))
        {
            PlayerPrefs.SetInt((TWITTER_SHARE), 0);
        }
        PlayerPrefs.SetInt((TWITTER_SHARE), PlayerPrefs.GetInt(TWITTER_SHARE) + 1);
        if (PlayerPrefs.GetInt(TWITTER_SHARE) == 1)
        {
            freeGiftTwit.SetActive(false);
            StartCoroutine(TimeToBrowse());
            StartCoroutine(GiveReward());
        }
    }

    public void PressedShareButton()
    {
        fade.SetActive(true);
        shareBar.SetActive(true);
    }

    IEnumerator GiveReward()
    {
        reward.SetActive(true);
        rewardFade.SetActive(true);
        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 5);
        yield return new WaitForSeconds(1.2f);
        giantButton.SetActive(true);
    }

    public void CloseReward()
    {
        rewardFade.SetActive(false);
        reward.SetActive(false);
        shareBar.SetActive(false);
        fade.SetActive(false);
        giantButton.SetActive(false);
    }

    IEnumerator TimeToBrowse()
    {
        yield return new WaitForSeconds(0.23f);
    }

}
