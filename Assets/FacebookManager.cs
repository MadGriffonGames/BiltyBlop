using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections.Generic;

public class FacebookManager : MonoBehaviour
{
    string shareURL;
    public Text userIdText;

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
            Debug.Log("ShareLing error: " + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
            Debug.Log("Share is fine");
    }
}
