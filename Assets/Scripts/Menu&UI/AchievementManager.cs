using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

    int killCounterTyplak;
    GameObject achievementUI;



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

    }

    // Use this for initialization
    void Start() {
        //ResetStat("Mob killer test", "mobKillerPrefTest");
        achievementUI = GameObject.FindGameObjectWithTag("Achievement UI");
        mobKillerReward = new int[] { 100, 150, 200 };
        mobKillerTargetValue = new int[] { 1, 3, 5 };
        tripleCoins = new string[] { "Coins", "Coins", "Coins" };

        treasureHunterReward = new int[] { 10, 100, 500 };
        treasureHunterValue = new int[] { 3, 10, 15 };

        idiotReward = new int[] { -5, -10, -15 };
        idiotTargerValue = new int[] { 1, 5, 10 };

        torchCollectorReward = new int[] { 100, 100, 100 };
        torchCollectorTargetValue = new int[] { 1, 2, 3 };

        secretRoomerReward = new int[] { 250, 450, 800 };
        secretRoomerTargetValue = new int[] { 1, 5, 10 };

        starWalkerReward = new int[] { 100, 150, 200 };
        starWalkerTargetValue = new int[] { 3, 5, 10 };

        millionareTargetValue = new int[] { 1000, 10000, 15000 };
        millionareReward = new int[] { 100, 150, 200 };


        //ResetStat("Mob killer test8");
        //ResetStat("DiverTest");
        //ResetStat("Secret Rush test");
        //ResetStat("Spider Boss killer test");
        ResetStat("First Buy Test");
        mobKiller = new Achieve("Mob killer test8", tripleCoins, mobKillerTargetValue, mobKillerReward);
        treasureHunter = new Achieve("Treasure Hunter Test", tripleCoins, treasureHunterValue, treasureHunterReward);
        swimmer = new LevelAchieve("DiverTest", "Coins", 5, 500);
        torchCollector = new LevelAchieve("Torch Collector Test", "Coins", 1, 500);
        secretRoomer = new Achieve("Secret Rush test", tripleCoins, secretRoomerTargetValue, secretRoomerReward);
        firstBuy = new LevelAchieve("First Buy Test", "Coins", 1, 1000);
        spiderKiller = new LevelAchieve("Spider Boss killer test", "Coins", 1, 1500);
        starWalker = new Achieve("StarWalker test", tripleCoins, starWalkerTargetValue, starWalkerReward);
        firstBoss = new LevelAchieve("Dragon Killer Test", "Coins", 1, 1500);
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
        Debug.Log(PlayerPrefs.GetInt(levelAchieve.achieveName));
        if (PlayerPrefs.GetInt(levelAchieve.achieveName) == levelAchieve.targetValue)
        {
            Debug.Log("achieved");
            GameManager.CollectedCoins += levelAchieve.reward;
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

