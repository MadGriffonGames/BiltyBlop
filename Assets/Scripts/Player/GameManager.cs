using UnityEngine;
using System.Collections;
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

	void Start () 
	{
        if (!PlayerPrefs.HasKey("Coins"))
        {
            collectedCoins = 0;
        }
        else
            collectedCoins = PlayerPrefs.GetInt("Coins");
        collectedCoins = 500;
        SoundManager.PlayMusic ("kid_music", true);
	}
	
	void Update ()
	{
		coinTxt.text = ("x " + collectedCoins);
	}
}
