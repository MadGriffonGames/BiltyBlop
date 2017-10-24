using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBoxOneLevel : MonoBehaviour
{


    [SerializeField]
    public string achievementName;

    [SerializeField]
    GameObject getBtn;


    [SerializeField]
    GameObject text;

    [SerializeField]
    GameObject inProgress;
    [SerializeField]
    GameObject doneImg;


    [SerializeField]
    GameObject gold;

    [SerializeField]
    RectTransform status;

    [SerializeField]
    GameObject description;

    [SerializeField]
    string descriptionText;

    [SerializeField]
    GameObject lootVolume;

    [SerializeField]
    GameObject fadeButton;

    [SerializeField]
    GameObject rewardFade;

    [SerializeField]
    GameObject loot;

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
    void Start()
    {
        PlayerPrefs.SetInt(achievementName + medal, 0);
        PlayerPrefs.SetInt(achievementName + btn, 0);


        UpdateStatus(PlayerPrefs.GetInt(achievementName), PlayerPrefs.GetInt(achievementName + "targetValue"));
        if (!PlayerPrefs.HasKey(achievementName + btn))
            PlayerPrefs.SetInt(achievementName + btn, 0);
        if (PlayerPrefs.GetInt(achievementName + btn) == 0)
        {
            text.GetComponent<Text>().text += " " + PlayerPrefs.GetInt(achievementName) + "/" + PlayerPrefs.GetInt(achievementName + "targetValue");
        }
        if (PlayerPrefs.GetInt(achievementName + btn) == 1)
        {
            doneImg.gameObject.SetActive(true);
        }

        description.gameObject.GetComponent<Text>().text = descriptionText;


        GetInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void GetInfo()
    {
            int weight = PlayerPrefs.GetInt(achievementName + "weight");
            int currentValue = PlayerPrefs.GetInt(achievementName);
            int targetValue = PlayerPrefs.GetInt(achievementName + "targetValue");

        UpdateStatus(PlayerPrefs.GetInt(achievementName), PlayerPrefs.GetInt(achievementName + "targetValue"));

        if (PlayerPrefs.GetInt(achievementName + medal) == 1)
            {
                gold.SetActive(true);
            }


        if (currentValue < targetValue)
        {
            inProgress.gameObject.SetActive(true);
        }


        if (currentValue >= targetValue && PlayerPrefs.GetInt(achievementName + btn) == 0)
            {
                inProgress.gameObject.SetActive(false);
                getBtn.gameObject.SetActive(true);
                getBtn.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                getBtn.gameObject.GetComponent<Button>().enabled = true;
            }
        }


    public void GetReward()
    {
        int currentValue = PlayerPrefs.GetInt(achievementName);
        int targetValue = PlayerPrefs.GetInt(achievementName + "targetValue");
        UpdateStatus(currentValue, targetValue);
        PlayerPrefs.SetInt(achievementName + btn, 1);
        inProgress.SetActive(false);
        PlayerPrefs.SetInt(achievementName + medal, 1);
        gold.gameObject.SetActive(true);
        doneImg.gameObject.SetActive(true);
        if (PlayerPrefs.GetString(achievementName + "rewardType") == "Coins")
        {
            StartCoroutine(ShowCoinLoot());
        }

        else if (PlayerPrefs.GetString(achievementName + "rewardType") == "Crystals")
        {
            StartCoroutine(ShowCrystalLoot());
        }

        else if (PlayerPrefs.GetString(achievementName + "rewardType") == HEAL || PlayerPrefs.GetString(achievementName + "rewardType") == DAMAGE_BONUS || PlayerPrefs.GetString(achievementName + "rewardType") == SPEED_BONUS || PlayerPrefs.GetString(achievementName + "rewardType") == TIME_BONUS || PlayerPrefs.GetString(achievementName + "rewardType") == IMMORTAL_BONUS || PlayerPrefs.GetString(achievementName + "rewardType") == AMMO)
        {
            StartCoroutine(ShowItemLoot());
        }

        else if (PlayerPrefs.GetString(achievementName + "rewardType") == "ClassicThrow" || PlayerPrefs.GetString(achievementName + "rewardType") == "CoinThrow" || PlayerPrefs.GetString(achievementName + "rewardType") == "MagicThrow" || PlayerPrefs.GetString(achievementName + "rewardType") == "SheepThrow")
        {

        }

        getBtn.gameObject.SetActive(false);
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

    IEnumerator ShowCoinLoot()
    {
        lootVolume.GetComponent<Text>().text = PlayerPrefs.GetInt(achievementName + "reward").ToString();
        rewardFade.gameObject.SetActive(true);
        loot.gameObject.GetComponent<Image>().sprite = coins;
        loot.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fadeButton.SetActive(true);
    }

    IEnumerator ShowCrystalLoot()
    {
        lootVolume.GetComponent<Text>().text = PlayerPrefs.GetInt(achievementName + "reward").ToString();
        rewardFade.gameObject.SetActive(true);
        loot.gameObject.GetComponent<Image>().sprite = crystals;
        GameManager.CollectedCoins += PlayerPrefs.GetInt(achievementName + "reward");
        loot.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fadeButton.SetActive(true);
    }

    IEnumerator ShowItemLoot()
    {
        lootVolume.GetComponent<Text>().text = PlayerPrefs.GetInt(achievementName + "reward").ToString();
        rewardFade.gameObject.SetActive(true);
        string lootType = PlayerPrefs.GetString(achievementName + "rewardType");

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


        Debug.Log(PlayerPrefs.GetInt(HEAL + "Count"));
        Debug.Log(PlayerPrefs.GetString(achievementName + "rewardType"));
        Inventory.Instance.AddItem(PlayerPrefs.GetString(achievementName + "rewardType"), PlayerPrefs.GetInt(achievementName + "reward"));
        loot.gameObject.SetActive(true);
        Debug.Log(PlayerPrefs.GetInt(HEAL + "Count"));
        yield return new WaitForSeconds(1.2f);
        fadeButton.SetActive(true);
    }
}