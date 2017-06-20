using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int collectedCoins;

	public static int CollectedCoins
	{
		get
		{
			return collectedCoins;
		}
		set
		{
			collectedCoins = value;
		}
	}

	[SerializeField]
	public Text coinTxt;
    public static string levelName;
    public static int lvlCollectedCoins;
    public static List<GameObject> deadEnemies;

    /* Inventory Items Names */ 
    public static string hpPots = "HealthPotCount";
    public static string damageBonuses = "DamageBonusCount";
    public static string speedBonuses = "SpeedBonusCount";
    public static string timeBonuses = "TimeBonusCount";
    public static string immortalBonuses = "ImmortalBonusCount";
    public static string clips = "ClipsCount";

    void Start () 
	{
        deadEnemies = new List<GameObject>();

        /* SETTING INVENTORY */
        if (!PlayerPrefs.HasKey("Max" + hpPots))
        {
            PlayerPrefs.SetInt("Max" + hpPots, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + damageBonuses))
        {
            PlayerPrefs.SetInt("Max" + damageBonuses, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + speedBonuses))
        {
            PlayerPrefs.SetInt("Max" + speedBonuses, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + timeBonuses))
        {
            PlayerPrefs.SetInt("Max" + timeBonuses, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + immortalBonuses))
        {
            PlayerPrefs.SetInt("Max" + immortalBonuses, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + clips))
        {
            PlayerPrefs.SetInt("Max" + clips, 5);
        }
        

        if (!PlayerPrefs.HasKey("Coins"))
        {
            collectedCoins = 500;//DON'T FORGET SET IT TO ZERO WHEN RELEASE
            PlayerPrefs.SetInt("Coins", collectedCoins);
        }
        else
        {
            collectedCoins = PlayerPrefs.GetInt("Coins");
        }

        if (!PlayerPrefs.HasKey("Skin"))
        {
            PlayerPrefs.SetString("Skin", "Classic");
        }

        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            coinTxt = FindObjectOfType<Text>();
        }
        
        if((SceneManager.GetActiveScene().name != "MainMenu") && (SceneManager.GetActiveScene().name != "Level10"))
            SoundManager.PlayMusic ("kid_music", true);
        if (SceneManager.GetActiveScene().name == "Level6")
            SoundManager.PlaySoundLooped("rain sfx");

        lvlCollectedCoins = 0;
    }

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    void Update ()
	{
        coinTxt.text = (" " + collectedCoins);
    }
}
