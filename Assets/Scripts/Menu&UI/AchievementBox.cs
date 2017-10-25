using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBox : MonoBehaviour {


    [SerializeField]
    public string achievementName;

    [SerializeField]
    GameObject getBtn;
    [SerializeField]
    GameObject getBtn1;
    [SerializeField]
    GameObject getBtn2;

    [SerializeField]
    string[] descriptionText;

    [SerializeField]
    GameObject description;


    [SerializeField]
    GameObject text;
    [SerializeField]
    GameObject levelText;


    [SerializeField]
    GameObject inProgress;
    [SerializeField]
    GameObject doneImg;


    [SerializeField]
    GameObject gold;
    [SerializeField]
    GameObject silver;
    [SerializeField]
    GameObject bronze;

    [SerializeField]
    GameObject rewardFade;
    [SerializeField]
    GameObject loot;

    [SerializeField]
    GameObject swordLoot;


    [SerializeField]
    Sprite coins;

    [SerializeField]
    Sprite crystals;

    [SerializeField]
    Sprite healthPot;

    [SerializeField]
    Sprite DamageBonus;

    [SerializeField]
    Sprite SpeedBonus;

    [SerializeField]
    Sprite TimeBonus;

    [SerializeField]
    Sprite ImmortalBonus;

    [SerializeField]
    Sprite ClipsCount;


    [SerializeField]
    RectTransform status;

    [SerializeField]
    GameObject fadeButton;


    [SerializeField]
    GameObject lootVolume;

    [SerializeField]
    GameObject swordLootVolume;

    [SerializeField]
    Sprite classicThrow;

    [SerializeField]
    Sprite hammerThrow;

    [SerializeField]
    Sprite iceCreamThrow;

    [SerializeField]
    Sprite magicThrow;

    [SerializeField]
    Sprite meatThrow;

    [SerializeField]
    Sprite pizzaThrow;

    [SerializeField]
    Sprite ravenThrow;

    [SerializeField]
    Sprite sheepThrow;

    [SerializeField]
    Sprite sword1Throw;

    [SerializeField]
    Sprite sword2Throw;

    [SerializeField]
    Sprite sword3Throw;

    [SerializeField]
    Sprite sword4Throw;

    [SerializeField]
    Sprite barbarianSword;

    [SerializeField]
    Sprite black_ninjaSword;

    [SerializeField]
    Sprite classicSword;

    [SerializeField]
    Sprite jonSnowSword;

    [SerializeField]
    Sprite kingSword;

    [SerializeField]
    Sprite barbarian;

    [SerializeField]
    Sprite black_ninja;

    [SerializeField]
    Sprite classic;

    [SerializeField]
    Sprite jonSnow;

    [SerializeField]
    Sprite king;

    string recordName;

    public const string HEAL = "HealthPot";
    public const string DAMAGE_BONUS = "DamageBonus";
    public const string SPEED_BONUS = "SpeedBonus";
    public const string TIME_BONUS = "TimeBonus";
    public const string IMMORTAL_BONUS = "ImmortalBonus";
    public const string AMMO = "ClipsCount";


    int gotReward;
    const string btn = "btn";
    const string medal = "medal";
    


	// Use this for initialization
	void Start () {

        recordName = text.GetComponent<Text>().text;

        //PlayerPrefs.SetInt(achievementName + medal, 0);
        //PlayerPrefs.SetInt(achievementName + btn, 0);

        if (!PlayerPrefs.HasKey(achievementName + btn))
            PlayerPrefs.SetInt(achievementName + btn, 0);
        GetInfo();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void GetInfo()
    {
        int weight = PlayerPrefs.GetInt(achievementName + "weight");
        int currentValue = PlayerPrefs.GetInt(achievementName);
        int targetValue0 = PlayerPrefs.GetInt(achievementName + "targetValue0");
        int targetValue1 = PlayerPrefs.GetInt(achievementName + "targetValue1");
        int targetValue2 = PlayerPrefs.GetInt(achievementName + "targetValue2");

        if (PlayerPrefs.GetInt(achievementName + btn) == 0)         // Get button has never been pressed
        {
            //Debug.Log(achievementName);
            //Debug.Log("never pressed");
            description.gameObject.GetComponent<Text>().text = descriptionText[0];
            text.GetComponent<Text>().text += " " + PlayerPrefs.GetInt(achievementName) + "/" + PlayerPrefs.GetInt(achievementName + "targetValue0");
            UpdateStatus(PlayerPrefs.GetInt(achievementName), PlayerPrefs.GetInt(achievementName + "targetValue0"));
        }

        if (PlayerPrefs.GetInt(achievementName + btn) == 1)         // Get button has been pressed once
        {
            description.gameObject.GetComponent<Text>().text = descriptionText[1];
            text.GetComponent<Text>().text += " " + PlayerPrefs.GetInt(achievementName) + "/" + PlayerPrefs.GetInt(achievementName + "targetValue1");
            UpdateStatus(PlayerPrefs.GetInt(achievementName), PlayerPrefs.GetInt(achievementName + "targetValue1"));
            bronze.SetActive(true);
        }

        if (PlayerPrefs.GetInt(achievementName + btn) == 2)         // Get butten has been pressed twice
        {
            //Debug.Log(achievementName);
            //Debug.Log("twice pressed");
            description.gameObject.GetComponent<Text>().text = descriptionText[2];
            text.GetComponent<Text>().text += " " + PlayerPrefs.GetInt(achievementName) + "/" + PlayerPrefs.GetInt(achievementName + "targetValue2");
            UpdateStatus(PlayerPrefs.GetInt(achievementName), PlayerPrefs.GetInt(achievementName + "targetValue2"));
            bronze.SetActive(false);
            silver.SetActive(true);
        }

        if (PlayerPrefs.GetInt(achievementName + btn) == 3)
        {
            bronze.SetActive(false);
            silver.SetActive(false);
            gold.SetActive(true);
            description.gameObject.GetComponent<Text>().text = descriptionText[2];
            doneImg.SetActive(true);
        }

        if (currentValue < targetValue0)
        {
            inProgress.gameObject.SetActive(true);
        }

        if (currentValue < targetValue1 && currentValue >= targetValue0)
        {
            if (achievementName == "Treasure Hunter")
            {
                Debug.Log("inProgress");
            }
            inProgress.gameObject.SetActive(true);
        }

        if (currentValue < targetValue2 && currentValue >= targetValue1)
        {
            inProgress.gameObject.SetActive(true);
        }


            if (currentValue >= targetValue0 && PlayerPrefs.GetInt(achievementName + btn) == 0)
            {
                inProgress.gameObject.SetActive(false);
                getBtn.gameObject.SetActive(true);
                getBtn.gameObject.GetComponent<Button>().enabled = true;
            }

            if (currentValue >= targetValue1 && PlayerPrefs.GetInt(achievementName + btn) == 1)
            {
                inProgress.gameObject.SetActive(false);
                getBtn1.gameObject.SetActive(true);
                getBtn1.gameObject.GetComponent<Button>().enabled = true;
            }

            if (currentValue >= targetValue2 && PlayerPrefs.GetInt(achievementName + btn) == 2)
            {
                inProgress.gameObject.SetActive(false);
                getBtn2.gameObject.SetActive(true);
                getBtn2.gameObject.GetComponent<Button>().enabled = true;
            }
        }



    public void GetFirstReward()
    {

        int currentValue = PlayerPrefs.GetInt(achievementName);
        int targetValue1 = PlayerPrefs.GetInt(achievementName + "targetValue1");
        UpdateValue(1);
        PlayerPrefs.SetInt(achievementName + btn, 1);
        description.gameObject.GetComponent<Text>().text = descriptionText[1];
        UpdateStatus(currentValue, targetValue1);
        getBtn.gameObject.SetActive(false);
        rewardFade.SetActive(true);
        string lootType = PlayerPrefs.GetString(achievementName + "rewardType0");
        Debug.Log(lootType);
        if (PlayerPrefs.GetString(achievementName + "rewardType0") == "Coins")
        {
            StartCoroutine(ShowCoinLoot(0));
        }

        else if (PlayerPrefs.GetString(achievementName + "rewardType0") == "Crystals")
        {
            StartCoroutine(ShowCrystalLoot(0));
        }

        else if (lootType == HEAL || lootType == DAMAGE_BONUS || lootType == SPEED_BONUS || lootType == TIME_BONUS || lootType == IMMORTAL_BONUS || lootType == AMMO)
        {
            StartCoroutine(ShowItemLoot(1));
        }

        else if (lootType == "ClassicThrow" || lootType == "HammerThrow" || lootType == "IcecreamThrow" || lootType == "MagicThrow" || lootType == "MeatThrow" || lootType == "PizzaThrow" || lootType == "RavenThrow" || lootType == "SheepThrow" || lootType == "Sword1Throw" || lootType == "Sword2Throw" || lootType == "Sword3Throw" || lootType == "Sword4Throw")
        {
            StartCoroutine(ShowThrowLoot(1));
        }

        else if (lootType == "BarbarianSword" || lootType == "Black_ninjaSword" || lootType == "ClassicSword" || lootType == "JonSnowSword" || lootType == "KingSword")
        {
            Debug.Log("Coinssss");
            StartCoroutine(ShowSwordLoot(0));
        }

        else if (lootType == "Barbarian" || lootType == "Black_ninja" || lootType == "Classic" || lootType == "JonSnow" || lootType == "King")
        {
            StartCoroutine(ShowSkinLoot(0));
        }


        if (currentValue >= targetValue1)
        {

            inProgress.SetActive(false);
            getBtn1.gameObject.SetActive(true);
        }
        else
            inProgress.SetActive(true);
        bronze.gameObject.SetActive(true);

    }

    public void GetSecondReward()
    {
        int currentValue = PlayerPrefs.GetInt(achievementName);
        int targetValue2 = PlayerPrefs.GetInt(achievementName + "targetValue2");
        UpdateValue(2);
        string lootType = PlayerPrefs.GetString(achievementName + "rewardType1");
        UpdateStatus(currentValue, targetValue2);
        PlayerPrefs.SetInt(achievementName + btn, 2);
        description.gameObject.GetComponent<Text>().text = descriptionText[2];
        getBtn1.gameObject.SetActive(false);
        if (PlayerPrefs.GetString(achievementName + "rewardType1") == "Coins")
        {
            StartCoroutine(ShowCoinLoot(1));
        }

        else if (PlayerPrefs.GetString(achievementName + "rewardType1") == "Crystals")
        {
            StartCoroutine(ShowCrystalLoot(1));
        }

        else if (lootType == HEAL || lootType == DAMAGE_BONUS || lootType == SPEED_BONUS || lootType == TIME_BONUS || lootType == IMMORTAL_BONUS || lootType == AMMO)
        {
            StartCoroutine(ShowItemLoot(1));
        }

        else if (lootType == "ClassicThrow" || lootType == "HammerThrow" || lootType == "IcecreamThrow" || lootType == "MagicThrow" || lootType == "MeatThrow" || lootType == "PizzaThrow" || lootType == "RavenThrow" || lootType == "SheepThrow" || lootType == "Sword1Throw" || lootType == "Sword2Throw" || lootType == "Sword3Throw" || lootType == "Sword4Throw")
        {
            StartCoroutine(ShowThrowLoot(1));
        }

        else if (lootType == "BarbarianSword" || lootType == "Black_ninjaSword" || lootType == "ClassicSword" || lootType == "JonSnowSword" || lootType == "KingSword")
        {
            StartCoroutine(ShowSwordLoot(1));
        }

        else if (lootType == "Barbarian" || lootType == "Black_ninja" || lootType == "Classic" || lootType == "JonSnow" || lootType == "King")
        {
            StartCoroutine(ShowSkinLoot(1));
        }

        if (currentValue < targetValue2)
        {
            inProgress.SetActive(true);
        }
        else
        {
            inProgress.SetActive(false);
            getBtn2.SetActive(true);
        }
        bronze.SetActive(false);
        silver.SetActive(true);
    }

    public void GetThirdReward()
    {
        PlayerPrefs.SetInt(achievementName + btn, 3);
        getBtn2.gameObject.SetActive(false);
        doneImg.gameObject.SetActive(true);
        string lootType = PlayerPrefs.GetString(achievementName + "rewardType2");
        if (lootType == "Coins")
        {
            StartCoroutine(ShowCoinLoot(2));
        }

        else if (lootType == "Crystals")
        {
            StartCoroutine(ShowCrystalLoot(2));
        }

        else if (lootType == HEAL || lootType == DAMAGE_BONUS || lootType == SPEED_BONUS || lootType == TIME_BONUS || lootType == IMMORTAL_BONUS || lootType == AMMO)
        {
            StartCoroutine(ShowItemLoot(2));
        }

        else if (lootType == "ClassicThrow" || lootType == "HammerThrow" || lootType == "IcecreamThrow" || lootType == "MagicThrow" || lootType == "MeatThrow" || lootType == "PizzaThrow" || lootType == "RavenThrow" || lootType == "SheepThrow" || lootType == "Sword1Throw" || lootType == "Sword2Throw" || lootType == "Sword3Throw" || lootType == "Sword4Throw")
        {
            StartCoroutine(ShowThrowLoot(2));
        }

        else if (lootType == "BarbarianSword" || lootType == "Black_ninjaSword" || lootType == "ClassicSword" || lootType == "JonSnowSword" || lootType == "KingSword")
        {
            StartCoroutine(ShowSwordLoot(2));
        }

        else if (lootType == "Barbarian" || lootType == "Black_ninja" || lootType == "Classic" || lootType == "JonSnow" || lootType == "King")
        {
            StartCoroutine(ShowSkinLoot(2));
        }

        UpdateStatus(PlayerPrefs.GetInt(achievementName), PlayerPrefs.GetInt(achievementName + "targetValue2"));
        description.gameObject.GetComponent<Text>().text = descriptionText[2];
        bronze.SetActive(false);
        silver.SetActive(false);
        gold.SetActive(true);
    }

    public void UpdateValue(int level)
    {
        text.GetComponent<Text>().text = recordName;
        text.GetComponent<Text>().text += " " + PlayerPrefs.GetInt(achievementName) + "/" + PlayerPrefs.GetInt(achievementName + "targetValue" + level.ToString());
    }

    void UpdateStatus(int currentValue, int currenTargetValue)
    {
        float newX;

        if (currentValue >= currenTargetValue)
            newX = 1;
        else
        {
            newX = (float)currentValue / (float)currenTargetValue;
        }
        status.localScale = new Vector3(newX, status.localScale.y, status.localScale.z);
    }

    public void FadeOut()
    {
        Debug.Log("Fade out");
        fadeButton.SetActive(false);
        rewardFade.SetActive(false);
        loot.SetActive(false);
        swordLoot.SetActive(false);
    }

    IEnumerator ShowCoinLoot(int level)
    {
        Debug.Log(PlayerPrefs.GetInt(achievementName + level.ToString()));
        lootVolume.GetComponent<Text>().text = PlayerPrefs.GetInt(achievementName + level.ToString()).ToString();
        rewardFade.gameObject.SetActive(true);
        loot.gameObject.GetComponent<Image>().sprite = coins;
        loot.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fadeButton.SetActive(true);
    }

    IEnumerator ShowCrystalLoot(int level)
    {
        Debug.Log(PlayerPrefs.GetInt(achievementName + level.ToString()));
        lootVolume.GetComponent<Text>().text = PlayerPrefs.GetInt(achievementName + level.ToString()).ToString();
        rewardFade.gameObject.SetActive(true);
        loot.gameObject.GetComponent<Image>().sprite = crystals;
        GameManager.CollectedCoins += PlayerPrefs.GetInt(achievementName + level.ToString());
        loot.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fadeButton.SetActive(true);
    }

    IEnumerator ShowItemLoot(int level)
    {
        Debug.Log(PlayerPrefs.GetInt(achievementName + level.ToString()));
        lootVolume.GetComponent<Text>().text = PlayerPrefs.GetInt(achievementName + level.ToString()).ToString();
        rewardFade.gameObject.SetActive(true);
        string lootType = PlayerPrefs.GetString(achievementName + "rewardType" + level.ToString());
        if (lootType == HEAL)
            loot.gameObject.GetComponent<Image>().sprite = healthPot;
        if (lootType == DAMAGE_BONUS)
            loot.gameObject.GetComponent<Image>().sprite = DamageBonus;
        if (lootType == SPEED_BONUS)
            loot.gameObject.GetComponent<Image>().sprite = SpeedBonus;
        if (lootType == TIME_BONUS)
            loot.gameObject.GetComponent<Image>().sprite = TimeBonus;
        if (lootType == IMMORTAL_BONUS)
            loot.gameObject.GetComponent<Image>().sprite = ImmortalBonus;
        if (lootType == AMMO)
            loot.gameObject.GetComponent<Image>().sprite = ClipsCount;
        Inventory.Instance.AddItem(PlayerPrefs.GetString(achievementName + "rewardType" + level.ToString()), PlayerPrefs.GetInt(achievementName + level.ToString()));
        loot.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fadeButton.SetActive(true);
    }

    IEnumerator ShowThrowLoot(int level)
    {
        rewardFade.gameObject.SetActive(true);
        string lootType = PlayerPrefs.GetString(achievementName + "rewardType" + level.ToString());

        if (lootType == "ClassicThrow")
        {
            PlayerPrefs.SetString("ClassicThrow", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = classicThrow;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "HammerThrow")
        {
            PlayerPrefs.SetString("HammerThrow", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = hammerThrow;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "IcecreamThrow")
        {
            PlayerPrefs.SetString("IcecreamThrow", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = iceCreamThrow;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "MagicThrow")
        {
            PlayerPrefs.SetString("MagicThrow", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = magicThrow;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "MeatThrow")
        {
            PlayerPrefs.SetString("MeatThrow", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = meatThrow;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "PizzaThrow")
        {
            PlayerPrefs.SetString("PizzaThrow", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = pizzaThrow;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "RavenThrow")
        {
            PlayerPrefs.SetString("RavenThrow", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = ravenThrow;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "SheepThrow")
        {
            PlayerPrefs.SetString("SheepThrow", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = sheepThrow;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "Sword1Throw")
        {
            PlayerPrefs.SetString("Sword1Throw", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = sword1Throw;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "Sword2Throw")
        {
            PlayerPrefs.SetString("Sword2Throw", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = sword2Throw;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "Sword3Throw")
        {
            PlayerPrefs.SetString("Sword3Throw", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = sword3Throw;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "Sword4Throw")
        {
            PlayerPrefs.SetString("Sword4Throw", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = sword4Throw;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }
        loot.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fadeButton.SetActive(true);
    }

    IEnumerator ShowSwordLoot(int level)
    {
        rewardFade.gameObject.SetActive(true);
        string lootType = PlayerPrefs.GetString(achievementName + "rewardType" + level.ToString());
        if (lootType == "BarbarianSword")
        {
            PlayerPrefs.SetString("BarbarianSword", "Unlocked");
            swordLoot.gameObject.GetComponent<Image>().sprite = barbarianSword;
            swordLootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "Black_ninjaSword")
        {
            Debug.Log("take ninja sword");
            PlayerPrefs.SetString("Black_ninjaSword", "Unlocked");
            swordLoot.gameObject.GetComponent<Image>().sprite = black_ninjaSword;
            swordLootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "ClassicSword")
        {
            PlayerPrefs.SetString("ClassicSword", "Unlocked");
            swordLoot.gameObject.GetComponent<Image>().sprite = classicSword;
            swordLootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "JonSnowSword")
        {
            PlayerPrefs.SetString("JonSnowSword", "Unlocked");
            swordLoot.gameObject.GetComponent<Image>().sprite = jonSnowSword;
            swordLootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "KingSword")
        {
            PlayerPrefs.SetString("KingSword", "Unlocked");
            swordLoot.gameObject.GetComponent<Image>().sprite = kingSword;
            swordLootVolume.GetComponent<Text>().text = "Unlocked";
        }

        swordLoot.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fadeButton.SetActive(true);
    }

    IEnumerator ShowSkinLoot(int level)
    {
        rewardFade.gameObject.SetActive(true);
        string lootType = PlayerPrefs.GetString(achievementName + "rewardType" + level.ToString());

        if (lootType == "Barbarian")
        {
            PlayerPrefs.SetString("Barbarian", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = barbarian;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "Black_ninja")
        {
            PlayerPrefs.SetString("Black_ninja", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = black_ninja;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "Classic")
        {
            PlayerPrefs.SetString("Classic", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = classic;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "JonSnow")
        {
            PlayerPrefs.SetString("JonSnow", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = jonSnow;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        if (lootType == "King")
        {
            PlayerPrefs.SetString("King", "Unlocked");
            loot.gameObject.GetComponent<Image>().sprite = king;
            lootVolume.GetComponent<Text>().text = "Unlocked";
        }

        loot.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fadeButton.SetActive(true);
    }
}
