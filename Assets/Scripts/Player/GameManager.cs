using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int collectedCoins = 50;

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

<<<<<<< HEAD
    public static string levelName;
=======
	// Use this for initialization
	void Start () 
	{
		SoundManager.PlayMusic ("muzlo", true);
		collectedCoins = 0;
	}
>>>>>>> DevG
	
	void Update ()
	{
		coinTxt.text = ("x " + collectedCoins);
	}
}
