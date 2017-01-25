using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	public static GameManager Instance 
	{
		get 
		{
			if (instance == null) 
			{
				instance = FindObjectOfType<GameManager> ();
			}
			return instance;
		}
	}

	private int collectedCoins;

	public int CollectedCoins
	{
		get
		{
			return collectedCoins;
		}
		set
		{
			this.collectedCoins = value;
		}
	}

	[SerializeField]
	public Text coinTxt;

	// Use this for initialization
	void Start () 
	{
		collectedCoins = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		coinTxt.text = ("" + collectedCoins);
	}
}
