using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public const string HEAL = "HealthPot";
    public const string DAMAGE_BONUS = "DamageBonus";
    public const string SPEED_BONUS = "SpeedBonus";
    public const string TIME_BONUS = "TimeBonus";
    public const string IMMORTAL_BONUS = "ImmortalBonus";
    public const string AMMO = "ClipsCount";
    public const string availableLoots = "avaliableLoots";

    int killCounterTyplak;
    GameObject achievementUI;
    
    string[] itemsNames;

    //------------achievements data---------------

    int[] mobKillerReward;
    int[] mobKillerTargetValue;
    string[] mobKillerRewardType;

    int[] treasureHunterReward;
    int[] treasureHunterValue;
    string[] treasureHunterRewardType;

    int[] idiotReward;
    int[] idiotTargerValue;

    int[] torchCollectorReward;
    int[] torchCollectorTargetValue;

    int[] secretRoomerReward;
    int[] secretRoomerTargetValue;
    string[] secretRoomRewardType;

    int[] starWalkerReward;
    int[] starWalkerTargetValue;
    string[] starWalkerRewardType;

    int[] millionareTargetValue;
    int[] millionareReward;

    int[] graverTargetValue;
    int[] graverReward;
    string[] graverRewardType;

    int[] potionerReward;
    int[] potionerTargetValue;
    string[] potionerRewardType;


    //---------other types of loot---------


    string[] tripleCoins;
    string[] tripleCrystals;
    string[] items;
    string[] differentItems2;

    //--------achievements---------------

    public Achieve mobKiller;
    public Achieve treasureHunter;
    public Achieve selfDestructor;
    public Achieve torchCollector3;
    public Achieve secretRoomer;
    public Achieve starWalker;
    public Achieve millionare;
    public Achieve graver;
    public Achieve potioner;
    public LevelAchieve swimmer;
    public LevelAchieve torchCollector;
    public LevelAchieve firstBuy;
    public LevelAchieve spiderKiller;
    public LevelAchieve firstBoss;
    public LevelAchieve tutorialAchieve;
    public LevelAchieve tenDaysReward;


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

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "AchievementMenu" || SceneManager.GetActiveScene().name == "Map" || SceneManager.GetActiveScene().name == "Shop")
            SoundManager.PlayMusic("main menu", true);

        if (!PlayerPrefs.HasKey(availableLoots))
            PlayerPrefs.SetInt(availableLoots, 0);
        achievementUI = GameObject.FindGameObjectWithTag("Achievement UI");
        items = new string[] { HEAL, HEAL, HEAL };
        
        graverTargetValue = new int[] {5, 25, 50 };
        graverReward = new int[] { 100, 5, 100 };
        graverRewardType = new string[] { "Coins", "Crystals", "MeatThrow" };

        mobKillerReward = new int[] { 1, 3, 1 };
        mobKillerTargetValue = new int[] { 25, 300, 1000 };
        mobKillerRewardType = new string[] { DAMAGE_BONUS, AMMO, "JonSnowSword" };

        tripleCoins = new string[] { "Coins", "Coins", "Coins" };
        tripleCrystals = new string[] { "Crystals", "Crystals", "Crystals" };
        secretRoomRewardType = new string[] { "Coins", AMMO, "Crystals" };
        differentItems2 = new string[] { "Coins", "Coins", "MeatThrow" };
        treasureHunterRewardType = new string[] { "Coins", DAMAGE_BONUS, "Crystals" };
        starWalkerRewardType = new string[] { "Coins" };

        treasureHunterReward = new int[] { 150, 3, 8 };

        treasureHunterValue = new int[] { 10, 35, 70 };

        idiotReward = new int[] { 100, 5, 1 };
        idiotTargerValue = new int[] { 10, 20, 30 };

        torchCollectorReward = new int[] { 100, 150, 250 };
        torchCollectorTargetValue = new int[] { 10, 25, 50 };

        secretRoomerReward = new int[] { 150, 3, 8 };
        secretRoomerTargetValue = new int[] { 5, 35, 50 };

        starWalkerReward = new int[] { 150, 200, 250 };
        starWalkerTargetValue = new int[] { 10, 45, 90 };

        millionareTargetValue = new int[] { 1000, 10000, 15000 };
        millionareReward = new int[] { 100, 150, 200 };

        potionerTargetValue = new int[] { 10, 25, 50};
        potionerReward = new int[] { 100, 2, 1 };
        potionerRewardType = new string[] { "Coins", "Crystals", HEAL};

		mobKiller = new Achieve("mob killa", mobKillerRewardType, mobKillerTargetValue, mobKillerReward);
		treasureHunter = new Achieve("treasure hunter", treasureHunterRewardType, treasureHunterValue, treasureHunterReward);
		swimmer = new LevelAchieve("diver", HEAL, 5, 3);
		torchCollector3 = new Achieve("torch collector", tripleCoins, torchCollectorTargetValue, torchCollectorReward);
		secretRoomer = new Achieve("secret room rush", secretRoomRewardType, secretRoomerTargetValue, secretRoomerReward);
		firstBuy = new LevelAchieve("first buy", "Crystals", 1, 2);
		spiderKiller = new LevelAchieve("ice lord", "Crystals", 1, 6);
		starWalker = new Achieve("star walker", tripleCoins, starWalkerTargetValue, starWalkerReward);
		firstBoss = new LevelAchieve("dragon bones", "Crystals", 1, 3);
		graver = new Achieve("graver", graverRewardType, graverTargetValue, graverReward);
		selfDestructor = new Achieve("self-destructor", graverRewardType, idiotTargerValue, idiotReward);
		potioner = new Achieve("potioner", potionerRewardType, potionerTargetValue, potionerReward);
		tutorialAchieve = new LevelAchieve("tutorial achievement", "Crystals", 1, 5);
		tenDaysReward = new LevelAchieve("ten days", "Crystals", 1, 5);
    }

    public void CheckAchieve(Achieve achieve)
    {
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
                        PlayerPrefs.SetInt(availableLoots, PlayerPrefs.GetInt(availableLoots) + 1);   // инкрементирование индикации
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
            PlayerPrefs.SetInt(availableLoots, PlayerPrefs.GetInt(availableLoots) + 1);   // инкрементирование индикации
            if (achievementUI)
            {
                achievementUI.GetComponent<AchievementUI>().AchievementAppear(levelAchieve.achieveName);
                StartCoroutine(achievementUI.GetComponent<AchievementUI>().AchievementDisappear());
            }
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

