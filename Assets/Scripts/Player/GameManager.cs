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

    void Start () 
	{
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            crystalTxt = GameObject.Find("CrystalTxt").GetComponent<Text>();
        }

        coinTxt = GameObject.Find("CoinTxt").GetComponent<Text>();

        Instantiate(adsManager);

        deadEnemies = new List<GameObject>();

        SetMoneyValues();

        if (PlayerPrefs.HasKey("Crystals") && crystalTxt != null)
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
        Debug.Log(torches);
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

    void ThrowBird()
    {
        try
        {
            if (isBirded == false)
            {
                isBirded = true;
                Vector3 firstPoint = new Vector3(Player.Instance.transform.position.x - 10, Player.Instance.transform.position.y, Player.Instance.transform.position.z);
                GameObject tmp = (GameObject)Instantiate(bird, new Vector3(), Quaternion.Euler(0, 0, 0));
                tmp.transform.position = firstPoint;
            }
        }
        catch (UnassignedReferenceException) { }
    }
   
}