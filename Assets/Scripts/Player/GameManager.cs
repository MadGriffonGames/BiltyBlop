using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private static int collectedCoins = 0;

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
	
	void Update ()
	{
		coinTxt.text = ("" + collectedCoins);
	}
}
