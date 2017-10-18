using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPrefab : MonoBehaviour {


	public string shopName;
	public bool isLocked;  // true = THROW locked; false = THROW unlocked;
	public int orderNumber;
	public Sprite throwSprite;

	public int crystalCost;
	public int coinCost;

	public int attackStat;
	public float speedStat;

	private const string LOCKED = "Locked";
	private const string UNLOCKED = "Unlocked";
	private const string CRYSTAL_COST = "CrystalCost";
	private const string COIN_COST = "CoinCost";
	private const string ATTACK = "AttackStat";
	private const string SPEED = "SpeedStat";
	private const string DISPLAY_INDEX = "DisplayIndex";

	private const string SPRITE_FOLDER = "Throw/ThrowSprites/";

	public void SetPlayerPrefsParams()
	{
		if (!PlayerPrefs.HasKey("Throw")) 
		{
			PlayerPrefs.SetString ("Throw", "ClassicThrow");
		}
		if (!PlayerPrefs.HasKey (gameObject.name)) {
			if (isLocked) {
				PlayerPrefs.SetString (gameObject.name, LOCKED);
			} else
				PlayerPrefs.SetString (gameObject.name, UNLOCKED);
		}
		if (!PlayerPrefs.HasKey(gameObject.name + CRYSTAL_COST))
		{
			PlayerPrefs.SetInt(gameObject.name + CRYSTAL_COST, crystalCost);
		}
		if (!PlayerPrefs.HasKey(gameObject.name + COIN_COST))
		{
			PlayerPrefs.SetInt(gameObject.name + COIN_COST, coinCost);
		}
		if (!PlayerPrefs.HasKey(gameObject.name + ATTACK))
		{
			PlayerPrefs.SetInt(gameObject.name + ATTACK, attackStat);
		}
		if (!PlayerPrefs.HasKey(gameObject.name + SPEED))
		{
			PlayerPrefs.SetFloat(gameObject.name + SPEED, speedStat);
		}
		throwSprite = Resources.Load<Sprite>(SPRITE_FOLDER + name);
	}

	public void UnlockThrow()
	{
		PlayerPrefs.SetString(gameObject.name, UNLOCKED);
		isLocked = false;
	}
}
