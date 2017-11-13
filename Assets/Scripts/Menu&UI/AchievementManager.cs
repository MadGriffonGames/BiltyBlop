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

    int[] graverTargetValue;
    int[] graverReward;

    string[] tripleCoins;
    string[] tripleCrystals;
    string[] items;
    string[] mobKillerRewardType;
    string[] secretRoomRewardType;
    string[] differentItems2;
    string[] treasureHunterRewardType;
    string[] starWalkerRewardType;
    string[] graverRewardType;

    public const string HEAL = "HealthPot";
    public const string DAMAGE_BONUS = "DamageBonus";
    public const string SPEED_BONUS = "SpeedBonus";
    public const string TIME_BONUS = "TimeBonus";
    public const string IMMORTAL_BONUS = "ImmortalBonus";
    public const string AMMO = "ClipsCount";
    public const string availableLoots = "avaliableLoots";

    public Achieve mobKiller;
    public Achieve treasureHunter;
    public Achieve selfDestructor;
    public LevelAchieve swimmer;
    public LevelAchieve torchCollector;
    public Achieve torchCollector3;
    public LevelAchieve firstBuy;
    public Achieve secretRoomer;
    public LevelAchieve spiderKiller;
    public Achieve starWalker;
    public Achieve millionare;
    public Achieve graver;
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

        if (!PlayerPrefs.HasKey(availableLoots))
            PlayerPrefs.SetInt(availableLoots, 0);
        //ResetStat("Mob killer test", "mobKillerPrefTest");
        achievementUI = GameObject.FindGameObjectWithTag("Achievement UI");
        items = new string[] { HEAL, HEAL, HEAL };
        
        graverTargetValue = new int[] {5, 25, 50 };
        graverReward = new int[] { 10, 50, 100 };
        graverRewardType = new string[] { "Coins", "Crystals", "MeatThrow" };

        mobKillerReward = new int[] { 1, 3, 1 };
        mobKillerTargetValue = new int[] { 25, 250, 1000 };
        mobKillerRewardType = new string[] { DAMAGE_BONUS, "PizzaThrow", "JonSnowSword" };

        tripleCoins = new string[] { "Coins", "Coins", "Coins" };
        tripleCrystals = new string[] { "Crystals", "Crystals", "Crystals" };
        secretRoomRewardType = new string[] { "Coins", AMMO, "Crystals" };
        differentItems2 = new string[] { "Coins", "Coins", "MeatThrow" };
        treasureHunterRewardType = new string[] { "Coins", DAMAGE_BONUS, "Crystals" };
        starWalkerRewardType = new string[] { "Coins" };

        treasureHunterReward = new int[] { 150, 3, 8 };

        treasureHunterValue = new int[] { 10, 25, 50 };

        idiotReward = new int[] { 100, 5, 1 };
        idiotTargerValue = new int[] { 10, 20, 30 };

        torchCollectorReward = new int[] { 100, 150, 250 };
        torchCollectorTargetValue = new int[] { 10, 25, 50 };

        secretRoomerReward = new int[] { 150, 3, 8 };
        secretRoomerTargetValue = new int[] { 5, 10, 15 };

        starWalkerReward = new int[] { 150, 200, 250 };
        starWalkerTargetValue = new int[] { 10, 25, 50 };

        millionareTargetValue = new int[] { 1000, 10000, 15000 };
        millionareReward = new int[] { 100, 150, 200 };


        PlayerPrefs.SetString("Black_ninja", "Locked");
        PlayerPrefs.SetString("Sword3Throw", "Locked");

        //ResetStat("Mob killer");
        //ResetStat("Diver");
        //ResetStat("Secret Rush test");
        //ResetStat("Spider Boss killer test");
        //ResetStat("First Buy Test");
        //ResetStat("StarWalker");
        mobKiller = new Achieve("Mob killer", mobKillerRewardType, mobKillerTargetValue, mobKillerReward);
        treasureHunter = new Achieve("Treasure Hunter", treasureHunterRewardType, treasureHunterValue, treasureHunterReward);
        swimmer = new LevelAchieve("Diver", HEAL, 5, 3);
        //swimmer = new LevelAchieve("Diver", "Crystals", 5, 500);
        //torchCollector = new LevelAchieve("Torch Collector", "Coins", 1, 150);
        torchCollector3 = new Achieve("TorchCollector", tripleCoins, torchCollectorTargetValue, torchCollectorReward);
        secretRoomer = new Achieve("Secret Rush", secretRoomRewardType, secretRoomerTargetValue, secretRoomerReward);
        firstBuy = new LevelAchieve("First Buy", "Crystals", 1, 2);
        spiderKiller = new LevelAchieve("Spider Boss killer", "Crystals", 1, 6);
        starWalker = new Achieve("StarWalker", tripleCoins, starWalkerTargetValue, starWalkerReward);
        firstBoss = new LevelAchieve("Dragon Killer", "Crystals", 1, 3);
        graver = new Achieve("Graver", graverRewardType, graverTargetValue, graverReward);
        selfDestructor = new Achieve("SelfDestructor", graverRewardType, idiotTargerValue, idiotReward);
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
                        PlayerPrefs.SetInt(availableLoots, PlayerPrefs.GetInt(availableLoots) + 1);
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
            PlayerPrefs.SetInt(availableLoots, PlayerPrefs.GetInt(availableLoots) + 1);
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

