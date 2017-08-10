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
            betaTestReward.SetActive(true);
        }

        SoundManager.PlayMusic("main menu", true);
    }

    public void ToActSelect(string sceneName)
    {
        actCanvas.SetActive(true);
    }

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    public void ToShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void BetaRewardButton()
    {
        if (!PlayerPrefs.HasKey("BetaReward") && !PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("BetaReward", 1);
            PlayerPrefs.SetInt("Crystals", 100);
            PlayerPrefs.SetInt("Coins", 1500); 
        }
        betaTestReward.SetActive(false);
    }
}
