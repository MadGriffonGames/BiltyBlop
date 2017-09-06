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

	public float[] upgradeScales;
	public int[] upgradeCoinCost;
	public int[] upgradeCrystalCost;

    [SerializeField]
    Text perkName;

    private const string LOCKED = "Locked";
    private const string UNLOCKED = "Unlocked";
    private const string CRYSTAL_COST = "CrystalCost";
    private const string COIN_COST = "CoinCost";

    private const string SPRITE_FOLDER = "Perks/PerkSprites/";


    void Awake()
    {
        perkName.text = shopName;
        SetPlayerPrefsParams();
    }

    public void SetPlayerPrefsParams()
    {
		if (!PlayerPrefs.HasKey(gameObject.name)) 
		{
			PlayerPrefs.SetInt (gameObject.name, 0); // 0 = LOCKED
		}
		if (!PlayerPrefs.HasKey(gameObject.name + "1")) 
		{
			PlayerPrefs.SetFloat (gameObject.name + "1", upgradeScales[0]); // 1 = first scale
		}
		if (!PlayerPrefs.HasKey(gameObject.name + "2")) 
		{
			PlayerPrefs.SetFloat (gameObject.name + "2", upgradeScales[1]); // 2 = second scale
		}
		if (!PlayerPrefs.HasKey(gameObject.name + "3")) 
		{
			PlayerPrefs.SetFloat (gameObject.name + "3", upgradeScales[2]); // 3 = third scale
		}

        perkSprite = Resources.Load<Sprite>(SPRITE_FOLDER + name);
    }
    public void UnlockPerk()
    {
        PlayerPrefs.SetString(gameObject.name, UNLOCKED);
        isLocked = false;
    }

	public bool CanUpgradePerk()
	{
		if (PlayerPrefs.GetInt (gameObject.name) < 3) 
		{
			PlayerPrefs.SetInt (gameObject.name, PlayerPrefs.GetInt (gameObject.name) + 1);
			return true;
		}
		else
		return false;
	}
}
