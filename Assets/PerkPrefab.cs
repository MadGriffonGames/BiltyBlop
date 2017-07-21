using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkPrefab : MonoBehaviour {

    public string shopName;
    public string description;
    public bool isLocked;  // true = skin locked; false = skin unlocked;
    public int crystalCost;
    public int coinCost;
    public int orderNumber;
    public Sprite perkSprite;

    [SerializeField]
    Text perkName;
    [SerializeField]
    Text perkDeascription;

    private const string LOCKED = "Locked";
    private const string UNLOCKED = "Unlocked";
    private const string CRYSTAL_COST = "CrystalCost";
    private const string COIN_COST = "CoinCost";

    private const string SPRITE_FOLDER = "PerkSprites/";


    void Awake()
    {
        perkName.text = shopName;
        perkDeascription.text = description;
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

        //perkSprite = Resources.Load<Sprite>(SPRITE_FOLDER + name);
    }
    public void UnlockPerk()
    {
        PlayerPrefs.SetString(gameObject.name, UNLOCKED);
        isLocked = false;
    }
}
