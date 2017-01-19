using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int collectedCoins;

	public Text coinTxt;

	// Use this for initialization
	void Start () 
	{
		collectedCoins = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		coinTxt.text = ("0" + collectedCoins);
	}
}
