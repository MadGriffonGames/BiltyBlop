﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int collectedCoins;

    public static int torches;

    bool isBirded = false;



    public static int CollectedCoins
    {
        get
        {
            return collectedCoins;
        }
        set
        {
            collectedCoins = value;
        }
    }

    [SerializeField]
    public Text coinTxt;
    [SerializeField]
    public static Text crystalTxt;
    [SerializeField]
    public GameObject adsManager;

    public static string nextLevelName;
    public static int lvlCollectedCoins;
    public static List<GameObject> deadEnemies;
    [SerializeField]
    GameObject bird;

    /* Inventory Items Names */
    public static string hpPots = "HealthPotCount";
    public static string damageBonuses = "DamageBonusCount";
    public static string speedBonuses = "SpeedBonusCount";
    public static string timeBonuses = "TimeBonusCount";
    public static string immortalBonuses = "ImmortalBonusCount";
    public static string clips = "ClipsCount";

    void Start () 
	{
        coinTxt = GameObject.Find("CoinTxt").GetComponent<Text>();
        crystalTxt = GameObject.Find("CrystalTxt").GetComponent<Text>();

        Instantiate(adsManager);

        deadEnemies = new List<GameObject>();

        SetMaxInventoryValues();

        SetMoneyValues();

        if (PlayerPrefs.HasKey("Crystals"))
        {
            crystalTxt.text = PlayerPrefs.GetInt("Crystals").ToString();
        }

        if (!PlayerPrefs.HasKey("Skin"))
        {
            PlayerPrefs.SetString("Skin", "Classic");
        }       
        
        if((SceneManager.GetActiveScene().name != "MainMenu") && (SceneManager.GetActiveScene().name != "Level10"))
            SoundManager.PlayMusic ("kid_music", true);
        if (SceneManager.GetActiveScene().name == "Level6")
            SoundManager.PlaySoundLooped("rain sfx");

        lvlCollectedCoins = 0;
    }

    void Update()
    {
        coinTxt.text = (" " + collectedCoins);
        if (torches == 0 && isBirded == false)
        {
            ThrowBird();
        }
    }

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    void SetMoneyValues()
    {
        if (!PlayerPrefs.HasKey("Coins"))
        {
            collectedCoins = 0;//DON'T FORGET SET IT TO ZERO WHEN RELEASE
            PlayerPrefs.SetInt("Coins", collectedCoins);
        }
        else
        {
            collectedCoins = PlayerPrefs.GetInt("Coins");
        }

        if (!PlayerPrefs.HasKey("Crystals"))
        {
            PlayerPrefs.SetInt("Crystals", 0);
        }
    }

    void SetMaxInventoryValues()
    {

        /* SETTING INVENTORY */
        if (!PlayerPrefs.HasKey("Max" + hpPots))
        {
            PlayerPrefs.SetInt("Max" + hpPots, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + damageBonuses))
        {
            PlayerPrefs.SetInt("Max" + damageBonuses, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + speedBonuses))
        {
            PlayerPrefs.SetInt("Max" + speedBonuses, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + timeBonuses))
        {
            PlayerPrefs.SetInt("Max" + timeBonuses, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + immortalBonuses))
        {
            PlayerPrefs.SetInt("Max" + immortalBonuses, 5);
        }
        if (!PlayerPrefs.HasKey("Max" + clips))
        {
            PlayerPrefs.SetInt("Max" + clips, 5);
        }
    }

    void ThrowBird()
    {
        if (isBirded==false)
        {
            isBirded = true;
            Vector3 firstPoint = new Vector3(Player.Instance.transform.position.x-10, Player.Instance.transform.position.y, Player.Instance.transform.position.z);
            GameObject tmp = (GameObject)Instantiate(bird, new Vector3(), Quaternion.Euler(0, 0, 0));
            tmp.transform.position = firstPoint;   
        }
    }
   
}