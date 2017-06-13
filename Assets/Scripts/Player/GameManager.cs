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
    public static List<GameObject> deadEnemies = new List<GameObject>();

    void Start () 
	{
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

        lvlCollectedCoins = 0;
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            coinTxt = FindObjectOfType<Text>();
        }
        
        if((SceneManager.GetActiveScene().name != "MainMenu") && (SceneManager.GetActiveScene().name != "Level10"))
            SoundManager.PlayMusic ("kid_music", true);
        if (SceneManager.GetActiveScene().name == "Level6")
            SoundManager.PlaySoundLooped("rain sfx");
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
