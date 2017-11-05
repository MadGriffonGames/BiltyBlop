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
        if (!PlayerPrefs.HasKey("FirstEnter"))
        {
            PlayerPrefs.SetInt("FirstEnter", 1); 
                      
            PlayerPrefs.SetInt("SwordDisplayIndex", 0);
            PlayerPrefs.SetInt("SwordAttackStat", 1);

            PlayerPrefs.SetInt("SkinDisplayIndex", 0);
            PlayerPrefs.SetInt("SkinArmorStat", 3);
            PlayerPrefs.SetString("Skin", "Classic");

            PlayerPrefs.SetString("Throw", "ClassicThrow");
            PlayerPrefs.SetInt("ThrowAttackStat", 1);
            PlayerPrefs.SetFloat("ThrowSpeedStat", 14);
        }			

        SoundManager.PlayMusic("main menu", true);
    }

    public void ToActSelect(string sceneName)
    {
        actCanvas.SetActive(true);
    }

    public void ToAchievmentMenu()
    {
        GameManager.nextLevelName = "AchievementMenu";

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

    public void RateUs()
    {
        if (Application.systemLanguage.ToString() == "Russian" || Application.systemLanguage.ToString() == "Ukrainian" || Application.systemLanguage.ToString() == "Belarusian")
        {
            Application.OpenURL("https://docs.google.com/forms/d/1RzwBi5aEDaxPxkkPDz91RwuDNApOxV_VFm2UDZDob4s");
        }
        else
        {
            Application.OpenURL("https://docs.google.com/forms/d/1sB0ASWy2K15KBX2QXThl9lED36WnT71OHxl8vWQFL9k");
        }             
    }

    public void BetaRewardButton()
    {
		if (PlayerPrefs.GetInt("Coins") == 0 && PlayerPrefs.GetInt("Crystals") == 0)
        {
			PlayerPrefs.SetString ("FirstTimeInGame", "No");
        }
        betaTestReward.SetActive(false);
    }
}
