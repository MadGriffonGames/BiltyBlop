using System.Collections;
using System.Collections.Generic;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject actCanvas;
    
    public string sceneName { get; set; }

    string appKey = "3481dd986d45650597337fafb3b51bd88bc5d6862675c1d2";

    public void Start()
    {
        SoundManager.PlayMusic("main menu", true);
        
        Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO | Appodeal.INTERSTITIAL);
    }

    public void ToActSelect(string sceneName)
    {
        actCanvas.SetActive(true);
        Appodeal.show(Appodeal.INTERSTITIAL);
    }

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    public void ToShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
