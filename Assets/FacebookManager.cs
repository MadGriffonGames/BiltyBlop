using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections.Generic;

public class FacebookManager : MonoBehaviour
{
    string shareURL;
    public Text userIdText;
    [SerializeField]
    GameObject shareBar;
    [SerializeField]
    GameObject fade;


    //twitter zone
    public string twitterNameParameter = "Check this amazing game!";
    public string twitterDescriptionParam = "";
    public const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWITTER_LANGUAGE = "en";


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
        shareURL = "www.kek.com";
#endif
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
            PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 900);
        }
    }

    public void PressedTwitterButton()
    {
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(twitterNameParameter + "\n" + twitterDescriptionParam + "\n" + shareURL));
        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 5);
    }

    public void PressedShareButton()
    {
        fade.SetActive(true);
        shareBar.SetActive(true);
    }

}
