using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

    int killCounterTyplak;
    GameObject achievementUI;


    string[] itemsNames;

    int[] mobKillerReward;
    int[] mobKillerTargetValue;

    int[] treasureHunterReward;
    int[] treasureHunterValue;

    int[] idiotReward;
    int[] idiotTargerValue;

    int[] torchCollectorReward;
    int[] torchCollectorTargetValue;

    int[] secretRoomerReward;
    int[] secretRoomerTargetValue;

    int[] starWalkerReward;
    int[] starWalkerTargetValue;

    int[] millionareTargetValue;
    int[] millionareReward;

    string[] tripleCoins;
    string[] tripleCrystals;
    string[] items;

    public const string HEAL = "HealthPot";
    public const string DAMAGE_BONUS = "DamageBonus";
    public const string SPEED_BONUS = "SpeedBonus";
    public const string TIME_BONUS = "TimeBonus";
    public const string IMMORTAL_BONUS = "ImmortalBonus";
    public const string AMMO = "ClipsCount";

    public Achieve mobKiller;
    public Achieve treasureHunter;
    public Achieve idiot;
    public LevelAchieve swimmer;
    public LevelAchieve torchCollector;
    public LevelAchieve firstBuy;
    public Achieve secretRoomer;
    public LevelAchieve spiderKiller;
    public Achieve starWalker;
    public Achieve millionare;
    public LevelAchieve firstBoss;
    int[] mobReward;


    private static AchievementManager instance;
    public static AchievementManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<AchievementManager>();
            return instance;
        }
    }

    private void Awake()
    {
        idiotReward = new int[] { -5, -10, -15 };
        idiotTargerValue = new int[] { 1, 5, 10 };
        itemsNames = new string[] { HEAL, DAMAGE_BONUS, SPEED_BONUS, TIME_BONUS, IMMORTAL_BONUS, AMMO };
    }

    // Use this for initialization
    void Start() {
        //ResetStat("Mob killer test", "mobKillerPrefTest");
        achievementUI = GameObject.FindGameObjectWithTag("Achievement UI");

        items = new string[] { HEAL, HEAL, HEAL };

        mobKillerReward = new int[] { 50, 150, 200 };
        mobKillerTargetValue = new int[] { 10, 50, 100 };

        tripleCoins = new string[] { "Coins", "Coins", "Coins" };
        tripleCrystals = new string[] { "Crystals", "Crystals", "Crystals" };

        treasureHunterReward = new int[] { 10, 100, 500 };
        treasureHunterValue = new int[] { 3, 10, 15 };

        idiotReward = new int[] { -5, -10, -15 };
        idiotTargerValue = new int[] { 1, 5, 10 };

        torchCollectorReward = new int[] { 100, 100, 100 };
        torchCollectorTargetValue = new int[] { 1, 2, 3 };

        secretRoomerReward = new int[] { 250, 450, 800 };
        secretRoomerTargetValue = new int[] { 1, 5, 10 };

        starWalkerReward = new int[] { 100, 150, 200 };
        starWalkerTargetValue = new int[] { 5, 15, 30 };

        millionareTargetValue = new int[] { 1000, 10000, 15000 };
        millionareReward = new int[] { 100, 150, 200 };


        ResetStat("Mob killer");
        ResetStat("Diver");
        //ResetStat("Secret Rush test");
        //ResetStat("Spider Boss killer test");
        //ResetStat("First Buy Test");
        ResetStat("StarWalker");
        mobKiller = new Achieve("Mob killer", tripleCrystals, mobKillerTargetValue, mobKillerReward);
        treasureHunter = new Achieve("Treasure Hunter", tripleCoins, treasureHunterValue, treasureHunterReward);
        swimmer = new LevelAchieve("Diver", HEAL, 5, 1);
        //swimmer = new LevelAchieve("Diver", "Crystals", 5, 500);
        torchCollector = new LevelAchieve("Torch Collector", "Coins", 1, 500);
        secretRoomer = new Achieve("Secret Rush", tripleCoins, secretRoomerTargetValue, secretRoomerReward);
        firstBuy = new LevelAchieve("First Buy", "Coins", 1, 1000);
        spiderKiller = new LevelAchieve("Spider Boss killer", "Coins", 1, 1500);
        starWalker = new Achieve("StarWalker", tripleCoins, starWalkerTargetValue, starWalkerReward);
        firstBoss = new LevelAchieve("Dragon Killer", "Coins", 1, 1500);
    }

    public void CheckAchieve(Achieve achieve)
    {
        string targetValue = "targetValue";
        string achieveName = achieve.achieveName;
        if (achievementUI != null)
        {
            if (achieve.threeLevel)
            {
                if (achieve.weight <= 2)
                {
                    PlayerPrefs.SetInt(achieveName, PlayerPrefs.GetInt(achieveName) + 1);
                    if (PlayerPrefs.GetInt(achieveName) == achieve.targetValueArray[achieve.weight])
                    {
                        GameManager.CollectedCoins += achieve.rewardArray[achieve.weight];
                        achieve.weight++;
                        PlayerPrefs.SetInt(achieveName + "weight", achieve.weight);
                        achievementUI.GetComponent<AchievementUI>().AchievementAppear(achieveName);
                        StartCoroutine(achievementUI.GetComponent<AchievementUI>().AchievementDisappear());
                    }
                }
                else
                {
                    Destroy(achieve);
                }
                
            }
            else
            {
                PlayerPrefs.SetInt(achieveName, PlayerPrefs.GetInt(achieveName) + 1);
                if (PlayerPrefs.GetInt(achieveName) == achieve.targetValue)
                {
                    GameManager.CollectedCoins += achieve.reward;

                    achievementUI.GetComponent<AchievementUI>().AchievementAppear(achieveName);
                    StartCoroutine(achievementUI.GetComponent<AchievementUI>().AchievementDisappear());
                }
            }
        }
    }


    public void CheckLevelAchieve(LevelAchieve levelAchieve)
    {
        PlayerPrefs.SetInt(levelAchieve.achieveName, PlayerPrefs.GetInt(levelAchieve.achieveName) + 1);
        if (PlayerPrefs.GetInt(levelAchieve.achieveName) == levelAchieve.targetValue)
        {
            achievementUI.GetComponent<AchievementUI>().AchievementAppear(levelAchieve.achieveName);
            StartCoroutine(achievementUI.GetComponent<AchievementUI>().AchievementDisappear());
            Destroy(levelAchieve);
        }

        if (PlayerPrefs.GetInt(levelAchieve.achieveName) > levelAchieve.targetValue)
        {
            Destroy(levelAchieve);
        }
    }



    void ResetStat(string achieveName)
    {
        PlayerPrefs.SetInt(achieveName + "btn", 0);
        PlayerPrefs.SetInt(achieveName, 0);
        PlayerPrefs.SetInt(achieveName + "weight", 0);
    }

    void GetReward(string achieveName, string achievePrefsName)
    {
        if (PlayerPrefs.GetString(achievePrefsName + "rewardType") == "Coins")
        {
            GameManager.collectedCoins += PlayerPrefs.GetInt(achievePrefsName + "reward");
        }

        if (PlayerPrefs.GetString(achievePrefsName + "rewardType") == "Gems")
        {
            PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + PlayerPrefs.GetInt(achievePrefsName + "reward"));
        }
    }

}

