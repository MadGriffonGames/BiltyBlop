using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DragonBones;
using UnityEngine;
using System;

public class SkinManager : MonoBehaviour
{
    private static SkinManager instance;
    public static SkinManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<SkinManager>();
            return instance;
        }
    }

    [SerializeField]
	public GameObject[] skinPrefabs; // all skin prefabs
	public GameObject[] swordPrefabs; 
	public GameObject[] throwPrefabs;

    private const string LOCKED = "Locked";
    private const string UNLOCKED = "Unlocked";

    private const string CRYSTAL_COST = "CrystalCost";
    private const string COIN_COST = "CoinCost";

    private const string SKIN_PREFABS_FOLDER = "Skins/";
	private const string SWORDS_PREFAB_FOLDER = "Swords/";
	private const string THROW_PREFAB_FOLDER = "Throw/";

    private void Start()
    {
        LoadSkinPrefabs();
		LoadSwordsPrefabs ();
		LoadThrowPrefabs ();

    }

	private void LoadThrowPrefabs()
	{
		throwPrefabs = Resources.LoadAll<GameObject>(THROW_PREFAB_FOLDER);
	}

    private void LoadSkinPrefabs()
    {
		skinPrefabs = Resources.LoadAll<GameObject>(SKIN_PREFABS_FOLDER);
    }
	private void LoadSwordsPrefabs()
	{
		swordPrefabs = Resources.LoadAll<GameObject> (SWORDS_PREFAB_FOLDER);
	}

    public bool isSkinLocked(int skinNumber) // true - Locked | false - Unlocked
    {
        return skinPrefabs[skinNumber].GetComponent<SkinPrefab>().isLocked;
        
    }

    public bool BuySkinByCrystals(int skinNumber)
    {
        if (PlayerPrefs.GetInt("Crystals") >= skinPrefabs[skinNumber].GetComponent<SkinPrefab>().crystalCost && skinPrefabs[skinNumber].GetComponent<SkinPrefab>().isLocked)
        {
            PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") - skinPrefabs[skinNumber].GetComponent<SkinPrefab>().crystalCost);
            skinPrefabs[skinNumber].GetComponent<SkinPrefab>().UnlockSkin();
            return true;
        }
        else
            return false;
    } // buying skin

    public bool BuySkinByCoins(int skinNumber)
    {
        // PAYMENT LOGIC
        if (PlayerPrefs.GetInt("Coins") >= skinPrefabs[skinNumber].GetComponent<SkinPrefab>().coinCost && skinPrefabs[skinNumber].GetComponent<SkinPrefab>().isLocked)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - skinPrefabs[skinNumber].GetComponent<SkinPrefab>().coinCost);
            skinPrefabs[skinNumber].GetComponent<SkinPrefab>().UnlockSkin();
            return true;
        }
        else
            return false;
    } // buying skin

	public bool BuySwordByCrystals(int swordNumber)
	{
		if (PlayerPrefs.GetInt("Crystals") >= swordPrefabs[swordNumber].GetComponent<SwordPrefab>().crystalCost && swordPrefabs[swordNumber].GetComponent<SwordPrefab>().isLocked)
		{
			PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") - swordPrefabs[swordNumber].GetComponent<SwordPrefab>().crystalCost);
			swordPrefabs[swordNumber].GetComponent<SwordPrefab>().UnlockSword();
			return true;
		}
		else
			return false;
	} // buying SWORD

	public bool BuySwordByCoins(int swordNumber)
	{
		// PAYMENT LOGIC
		if (PlayerPrefs.GetInt("Coins") >= swordPrefabs[swordNumber].GetComponent<SwordPrefab>().coinCost && swordPrefabs[swordNumber].GetComponent<SwordPrefab>().isLocked)
		{
			PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - swordPrefabs[swordNumber].GetComponent<SwordPrefab>().coinCost);
			swordPrefabs[swordNumber].GetComponent<SwordPrefab>().UnlockSword();
			return true;
		}
		else
			return false;
	} // buying SWORD

	public bool BuyThrowByCrystals(int throwNumber)
	{
		if (PlayerPrefs.GetInt("Crystals") >= throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().crystalCost && throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().isLocked)
		{
			PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") - throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().crystalCost);
			throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().UnlockThrow();
			return true;
		}
		else
			return false;
	} // buying THROW

	public bool BuyThrowByCoins(int throwNumber)
	{
		// PAYMENT LOGIC
		if (PlayerPrefs.GetInt("Coins") >= throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().coinCost && throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().isLocked)
		{
			PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().coinCost);
			throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().UnlockThrow();
			return true;
		}
		else
			return false;
	} // buying THROW

    private void UnlockSkin(int skinNumber)
    {
        PlayerPrefs.SetString(skinPrefabs[skinNumber].name, UNLOCKED);
		skinPrefabs [skinNumber].GetComponent<SkinPrefab> ().isLocked = false;
    }
    private void UnlockSkin(string skinName)
    {   
        PlayerPrefs.SetString(skinPrefabs[NumberOfSkin(skinName)].name, UNLOCKED);
		skinPrefabs [NumberOfSkin(skinName)].GetComponent<SkinPrefab> ().isLocked = false;
    }

	public void ApplySkin(string skinName) // applying (equiping) "skinName" skin 
    {
        PlayerPrefs.SetString("Skin", skinName);
		PlayerPrefs.SetInt("SkinDisplayIndex", skinPrefabs [NumberOfSkin (skinName)].GetComponent<SkinPrefab> ().displayIndex);
		PlayerPrefs.SetInt("SkinArmorStat", skinPrefabs [NumberOfSkin (skinName)].GetComponent<SkinPrefab> ().armorStat);
    }

	public void ApplySword(string swordName, int index) // applying (equiping) "skinName" skin 
	{
		PlayerPrefs.SetString("Sword", swordName);
		PlayerPrefs.SetInt("SwordDisplayIndex", index);
        PlayerPrefs.SetInt("SwordAttackStat", swordPrefabs[NumberOfSword(swordName)].GetComponent<SwordPrefab>().attackStat);
    }

	public void ApplyThrow(string throwName) // applying (equiping) "skinName" skin 
	{
		PlayerPrefs.SetString("Throw", throwName);
		PlayerPrefs.SetFloat("ThrowSpeedStat", throwPrefabs[NumberOfThrow(throwName)].GetComponent<ThrowPrefab>().speedStat);
		PlayerPrefs.SetInt("ThrowAttackStat", throwPrefabs[NumberOfThrow(throwName)].GetComponent<ThrowPrefab>().attackStat);
	}

	public int IndexOfSwordByOrderNumber(int orderNumber)
	{
		for (int i = 0; i < swordPrefabs.Length; i++)
		{
			if (swordPrefabs[i].GetComponent<SwordPrefab>().orderNumber == orderNumber)
			{
				return swordPrefabs[i].GetComponent<SwordPrefab>().displayIndex;
			}
		}
		return 0;
	}

	public string SpriteOfThrowByOrderNumber(int orderNumber)
	{
		for (int i = 0; i < throwPrefabs.Length; i++)
		{
			if (throwPrefabs[i].GetComponent<ThrowPrefab>().orderNumber == orderNumber)
			{
				return throwPrefabs[i].name;
			}
		}
		return "ClassicThrow";
	}

    public int NumberOfSkinPrefabBySkinOrder(int orderNumber)
    {
        for (int i = 0; i < skinPrefabs.Length; i++)
        {
            if (skinPrefabs[i].GetComponent<SkinPrefab>().orderNumber == orderNumber)
            {
                return i;
            }
        }
        return 0;
    }

	public int NumberOfThrowPrefabByOrder(int orderNumber)
	{
		for (int i = 0; i < throwPrefabs.Length; i++)
		{
			if (throwPrefabs[i].GetComponent<ThrowPrefab>().orderNumber == orderNumber)
			{
				return i;
			}
		}
		return 0;
	}

    public string NameOfSkinPrefabBySkinOrder(int orderNumber)
    {
        for (int i = 0; i < skinPrefabs.Length; i++)
        {
            if (skinPrefabs[i].GetComponent<SkinPrefab>().orderNumber == orderNumber)
            {
                return skinPrefabs[i].GetComponent<SkinPrefab>().name;
            }
        }
        return "Classic";
    }

	public string NameOfSwordPrefabBySwordOrder(int orderNumber)
	{
		for (int i = 0; i < swordPrefabs.Length; i++)
		{
			if (swordPrefabs[i].GetComponent<SwordPrefab>().orderNumber == orderNumber)
			{
				return swordPrefabs[i].GetComponent<SwordPrefab>().name;
			}
		}
		return "Classic";
	}

	public string NameOfThrowPrefabBySwordOrder(int orderNumber)
	{
		for (int i = 0; i < throwPrefabs.Length; i++)
		{
			if (throwPrefabs[i].GetComponent<ThrowPrefab>().orderNumber == orderNumber)
			{
				return throwPrefabs[i].GetComponent<ThrowPrefab>().name;
			}
		}
		return "Classic";
	}

    public int NumberOfSkin(string skinName)
    {
        for (int i = 0; i < skinPrefabs.Length; i++)
        {
            if (skinPrefabs[i].name == skinName)
                return i;
        }
        return 0;
    }

    int NumberOfSword(string swordName)
    {
        for (int i = 0; i < swordPrefabs.Length; i++)
        {
            if (swordPrefabs[i].name == swordName)
                return i;
        }
        return 0;
    }
	int NumberOfThrow(string throwName)
	{
		for (int i = 0; i < throwPrefabs.Length; i++)
		{
			if (throwPrefabs[i].name == throwName)
				return i;
		}
		return 0;
	}
}
