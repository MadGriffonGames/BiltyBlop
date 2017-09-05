using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject actCanvas;
    [SerializeField]
    GameObject betaTestReward;
    
    public string sceneName { get; set; }


    public void Start()
    {
        if (!PlayerPrefs.HasKey("BetaReward"))
        {
            PlayerPrefs.SetInt("BetaReward", 1);
			PlayerPrefs.SetInt("SwordDisplayIndex", 0);
			PlayerPrefs.SetInt ("SkinDisplayIndex", 0);
			PlayerPrefs.SetInt ("SkinArmorStat", 3);
			PlayerPrefs.SetInt ("SkinAttackStat", 1);
			PlayerPrefs.SetString ("Skin", "Classic");
            betaTestReward.SetActive(true);
        }

        SoundManager.PlayMusic("main menu", true);
    }

    public void ToActSelect(string sceneName)
    {
        actCanvas.SetActive(true);
    }

    public void ToIapTest()
    {
        GameManager.nextLevelName = "IapTest";

        SceneManager.LoadScene("Loading");
    }

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    public void ToShop()
    {
        GameManager.nextLevelName = "Shop";

        SceneManager.LoadScene("Loading");
    }

    public void BetaRewardButton()
    {
		if (PlayerPrefs.GetInt("Coins") == 0 && PlayerPrefs.GetInt("Crystals") == 0)
        {
            PlayerPrefs.SetInt("BetaReward", 1);
            PlayerPrefs.SetInt("Crystals", 150);
            PlayerPrefs.SetInt("Coins", 1500);
			PlayerPrefs.SetString ("FirstTimeInGame", "No");
        }
        betaTestReward.SetActive(false);
    }
}
