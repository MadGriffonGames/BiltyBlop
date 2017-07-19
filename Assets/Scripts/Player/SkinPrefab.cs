using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPrefab : MonoBehaviour {

    public string shopName;
    public bool isLocked;  // true = skin locked; false = skin unlocked;
    public int crystalCost;
    public int coinCost;
    public int armorStat;
    public int attackStat;
    public int orderNumber;
    public Sprite skinSprite;

    private const string LOCKED = "Locked";
    private const string UNLOCKED = "Unlocked";
    private const string CRYSTAL_COST = "CrystalCost";
    private const string COIN_COST = "CoinCost";
    private const string ARMOR = "ArmorStat";
    private const string ATTACK = "AttackStat";

    private const string SPRITE_FOLDER = "SkinSprites/";


    void Awake ()
    {
        SetPlayerPrefsParams();		
	}

    private void SetPlayerPrefsParams()
    {
        if (!PlayerPrefs.HasKey(gameObject.name))
        {
            if (isLocked)
            {
                PlayerPrefs.SetString(gameObject.name, LOCKED);
            }
            else
                PlayerPrefs.SetString(gameObject.name, UNLOCKED);
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
        skinSprite = Resources.Load<Sprite>(SPRITE_FOLDER + name);
    }
}
