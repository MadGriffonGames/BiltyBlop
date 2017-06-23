using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DragonBones;
using UnityEngine;

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
    public GameObject[] skinPrefabs;
    private string[] skinLocks;
    private static string locked = "Locked";
    private static string unlocked = "Unlocked";

    private void Start()
    {
        skinLocks = new string[skinPrefabs.Length];
        skinLocks[0] = unlocked;
        for (int i = 1; i < skinPrefabs.Length; i++)
        {
            if (!PlayerPrefs.HasKey(skinPrefabs[i].name))
            {
                PlayerPrefs.SetString(skinPrefabs[i].name, locked);
            }
            skinLocks[i] = PlayerPrefs.GetString(skinPrefabs[i].name);
        }
    }

    public bool isSkinUnlocked(string skinName)
    {
        int skinNumber = NumberOfSkin(skinName);
        if (skinLocks[skinNumber] == unlocked)
        {
            return true;
        }
        else return false;
    }
    public bool isSkinUnlocked(int skinNumber)
    {
        if (skinLocks[skinNumber] == unlocked)
        {
            return true;
        }
        else return false;
    }

    public void UnlockSkin(int skinNumber)
    {
        skinLocks[skinNumber] = unlocked;
        PlayerPrefs.SetString(skinPrefabs[skinNumber].name, unlocked);
    }
    public void UnlockSkin(string skinName)
    {   
        skinLocks[NumberOfSkin(skinName)] = unlocked;
        PlayerPrefs.SetString(skinPrefabs[NumberOfSkin(skinName)].name, unlocked);
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
