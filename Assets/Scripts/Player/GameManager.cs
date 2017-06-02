using UnityEngine;
using System.Collections;
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

    void Start () 
	{
        if (!PlayerPrefs.HasKey("Coins"))
        {
            collectedCoins = 500;//DON'T FORGET SET IT TO ZERO WHEN RELEASE
            PlayerPrefs.SetInt("Coins", collectedCoins);
        }
        else
            collectedCoins = PlayerPrefs.GetInt("Coins");
        lvlCollectedCoins = 0;
        coinTxt = FindObjectOfType<Text>();
        if((SceneManager.GetActiveScene().name != "MainMenu") && (SceneManager.GetActiveScene().name != "Level9"))
            SoundManager.PlayMusic ("kid_music", true);
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
