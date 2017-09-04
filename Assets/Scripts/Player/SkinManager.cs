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
    public GameObject[] skinPrefabs;   // all skin prefabs

    private const string LOCKED = "Locked";
    private const string UNLOCKED = "Unlocked";

    private const string CRYSTAL_COST = "CrystalCost";
    private const string COIN_COST = "CoinCost";

    private const string PREFABS_FOLDER = "Skins/";

    private void Start()
    {
        LoadSkinPrefabs();
    }

    private void LoadSkinPrefabs()
    {
		skinPrefabs = Resources.LoadAll<GameObject>(PREFABS_FOLDER);
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
		Debug.Log (skinPrefabs [NumberOfSkin (skinName)].GetComponent<SkinPrefab> ().displayIndex);
		PlayerPrefs.SetInt("SkinDisplayIndex", skinPrefabs [NumberOfSkin (skinName)].GetComponent<SkinPrefab> ().displayIndex);
		PlayerPrefs.SetInt("SkinArmorStat", skinPrefabs [NumberOfSkin (skinName)].GetComponent<SkinPrefab> ().armorStat);
		PlayerPrefs.SetInt("SkinAttackStat", skinPrefabs [NumberOfSkin (skinName)].GetComponent<SkinPrefab> ().attackStat);
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
}
