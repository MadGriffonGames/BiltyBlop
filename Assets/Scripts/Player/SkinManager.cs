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
    private string[] skinLocks; // contains information abaut skin locks: LOCKED || UNLOCKED

    private const string LOCKED = "Locked";
    private const string UNLOCKED = "Unlocked";

    private const string CRYSTAL_COST = "CrystalCost";
    private const string COIN_COST = "CoinCost";

    private const string PREFABS_FOLDER = "Skins/";

    private void Start()
    {
        skinLocks = new string[skinPrefabs.Length];
        skinLocks[0] = UNLOCKED;

        LoadSkinPrefabs();
    }

    private void LoadSkinPrefabs()
    {
        skinPrefabs = Resources.LoadAll<GameObject>(PREFABS_FOLDER);
    }

    public bool isSkinUnlocked(string skinName)
    {
        int skinNumber = NumberOfSkin(skinName);
        if (skinLocks[skinNumber] == UNLOCKED)
        {
            return true;
        }
        else return false;
    }
    public bool isSkinUnlocked(int skinNumber)
    {
        if (skinLocks[skinNumber] == UNLOCKED)
        {
            return true;
        }
        else return false;
    }

    public void UnlockSkin(int skinNumber)
    {
        skinLocks[skinNumber] = UNLOCKED;
        PlayerPrefs.SetString(skinPrefabs[skinNumber].name, UNLOCKED);
    }
    public void UnlockSkin(string skinName)
    {   
        skinLocks[NumberOfSkin(skinName)] = UNLOCKED;
        PlayerPrefs.SetString(skinPrefabs[NumberOfSkin(skinName)].name, UNLOCKED);
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
