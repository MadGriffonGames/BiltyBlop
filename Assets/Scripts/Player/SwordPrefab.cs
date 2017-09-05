using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPrefab : MonoBehaviour {


	public string shopName;
	public bool isLocked;  // true = SWORD locked; false = skin unlocked;
	public int orderNumber;
	public Sprite swordSprite;

	public int crystalCost;
	public int coinCost;

	public int armorStat;
	public int attackStat;
	public int displayIndex;

	private const string LOCKED = "Locked";
	private const string UNLOCKED = "Unlocked";
	private const string CRYSTAL_COST = "CrystalCost";
	private const string COIN_COST = "CoinCost";
	private const string ARMOR = "ArmorStat";
	private const string ATTACK = "AttackStat";
	private const string DISPLAY_INDEX = "DisplayIndex";

	private const string SPRITE_FOLDER = "Swords/SwordSprites/";

	public void SetPlayerPrefsParams()
	{
		if (!PlayerPrefs.HasKey("Sword")) 
		{
			PlayerPrefs.SetString ("Sword", "Classic");
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
		if (!PlayerPrefs.HasKey(gameObject.name + ARMOR))
		{
			PlayerPrefs.SetInt(gameObject.name + ARMOR, armorStat);
		}
		if (!PlayerPrefs.HasKey(gameObject.name + ATTACK))
		{
			PlayerPrefs.SetInt(gameObject.name + ATTACK, attackStat);
		}
		if (!PlayerPrefs.HasKey(gameObject.name + DISPLAY_INDEX))
		{
			PlayerPrefs.SetInt(gameObject.name + DISPLAY_INDEX, displayIndex);
		}
		swordSprite = Resources.Load<Sprite>(SPRITE_FOLDER + name);
	}

	public void UnlockSword()
	{
		PlayerPrefs.SetString(gameObject.name, UNLOCKED);
		isLocked = false;
	}
}
