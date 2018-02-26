using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPrefab : MonoBehaviour {


	public string shopName;
	public bool isLocked;  // true = SWORD locked; false = skin unlocked;
	public bool isAvaliableInShop; // thrue - avaliable; false - NOT avaliable;
	public int orderNumber;
	public Sprite swordSprite;

	public int crystalCost;
	public int coinCost;

	public int attackStat;
	public int displayIndex;

	private const string LOCKED = "Locked";
	private const string UNLOCKED = "Unlocked";
	private const string AVALIABLE = "Avaliable";
	private const string NOT_AVALIABLE = "NotAvaliable";
	private const string CRYSTAL_COST = "CrystalCost";
	private const string COIN_COST = "CoinCost";
	private const string ARMOR = "ArmorStat";
	private const string ATTACK = "AttackStat";
	private const string DISPLAY_INDEX = "DisplayIndex";
	private const string IS_AVALIABLE_IN_SHOP = "IsAvaliableInShop";

	private const string SPRITE_FOLDER = "Swords/SwordSprites/";

	public void SetPlayerPrefsParams()
	{
		if (!PlayerPrefs.HasKey("Sword")) 
		{
			PlayerPrefs.SetString ("Sword", "ClassicSword");
		}
		if (!PlayerPrefs.HasKey (gameObject.name)) {
			if (isLocked) {
				PlayerPrefs.SetString (gameObject.name, LOCKED);
			} else
				PlayerPrefs.SetString (gameObject.name, UNLOCKED);
		}

		if (!PlayerPrefs.HasKey (gameObject.name + IS_AVALIABLE_IN_SHOP)) {
			if (isAvaliableInShop) {
				PlayerPrefs.SetString (gameObject.name + IS_AVALIABLE_IN_SHOP, AVALIABLE);
			} else
				PlayerPrefs.SetString (gameObject.name + IS_AVALIABLE_IN_SHOP, NOT_AVALIABLE);
		} else 
		{
			if (PlayerPrefs.GetString (gameObject.name + IS_AVALIABLE_IN_SHOP) == AVALIABLE)
				isAvaliableInShop = true;
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
