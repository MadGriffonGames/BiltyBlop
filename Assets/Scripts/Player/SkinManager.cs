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

    private const string LOCKED = "Locked";
    private const string UNLOCKED = "Unlocked";

    private const string CRYSTAL_COST = "CrystalCost";
    private const string COIN_COST = "CoinCost";

    private const string SKIN_PREFABS_FOLDER = "Skins/";
	private const string SWORDS_PREFAB_FOLDER = "Swords/";
    const string firstBuy = "firstBuy";

    private void Start()
    {
        LoadSkinPrefabs();
		LoadSwordsPrefabs ();

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
            if (PlayerPrefs.GetInt(firstBuy) == 0 || !PlayerPrefs.HasKey(firstBuy))
            {
                PlayerPrefs.SetInt(firstBuy, 1);
            }

            if (PlayerPrefs.GetInt(firstBuy) == 1)
            {
                AchievementManager.Instance.CheckLevelAchieve(AchievementManager.Instance.firstBuy);
            }
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
            if (PlayerPrefs.GetInt(firstBuy) == 0 || !PlayerPrefs.HasKey(firstBuy))
            {
                PlayerPrefs.SetInt(firstBuy, 1);
            }

            if (PlayerPrefs.GetInt(firstBuy) == 1)
            {
                AchievementManager.Instance.CheckLevelAchieve(AchievementManager.Instance.firstBuy);
            }
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
            if (PlayerPrefs.GetInt(firstBuy) == 0 || !PlayerPrefs.HasKey(firstBuy))
            {
                PlayerPrefs.SetInt(firstBuy, 1);
            }

            if (PlayerPrefs.GetInt(firstBuy) == 1)
            {
                AchievementManager.Instance.CheckLevelAchieve(AchievementManager.Instance.firstBuy);
            }
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
            if (PlayerPrefs.GetInt(firstBuy) == 0 || !PlayerPrefs.HasKey(firstBuy))
            {
                PlayerPrefs.SetInt(firstBuy, 1);
            }

            if (PlayerPrefs.GetInt(firstBuy) == 1)
            {
                AchievementManager.Instance.CheckLevelAchieve(AchievementManager.Instance.firstBuy);
            }
            return true;
		}
		else
			return false;
	} // buying SWORD

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
        PlayerPrefs.SetInt("SkinAttackStat", swordPrefabs[NumberOfSword(swordName)].GetComponent<SwordPrefab>().attackStat);
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
}
