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

    int[] swimmerReward;
    int[] swimmerTargetValue;

    int[] torchCollectorReward;
    int[] torchCollectorTargetValue;

    int[] secretRoomerReward;
    int[] secretRoomerTargetValue;

    public Achieve mobKiller;
    public Achieve treasureHunter;
    public Achieve idiot;
    public Achieve swimmer;
    public Achieve torchCollector;
    public Achieve secretRoomer;
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

        swimmerReward = new int[] { 100, 150, 200 };
        swimmerTargetValue = new int[] { 10, 15, 20 };
    }

    // Use this for initialization
    void Start() {
        //ResetStat("Mob killer test", "mobKillerPrefTest");
        achievementUI = GameObject.FindGameObjectWithTag("Achievement UI");
        mobKillerReward = new int[] { 100, 150, 200 };
        mobKillerTargetValue = new int[] { 1, 2, 3 };

        treasureHunterReward = new int[] { 10, 100, 500 };
        treasureHunterValue = new int[] { 1, 10, 100 };

        idiotReward = new int[] { -5, -10, -15 };
        idiotTargerValue = new int[] { 1, 5, 10 };

        swimmerReward = new int[] { 100, 150, 200 };
        swimmerTargetValue = new int[] { 1, 3, 5 };

        torchCollectorReward = new int[] { 100, 100, 100 };
        torchCollectorTargetValue = new int[] { 1, 2, 3 };

        secretRoomerReward = new int[] { 200, 400, 600 };
        secretRoomerTargetValue = new int[] { 5, 10, 15 };

        //ResetStat("Secret Rush test", "secretRoomerPref");

        mobKiller = new Achieve("Mob killer test", "mobKillerPrefTest", "Coins", mobKillerTargetValue, mobKillerReward, true);
        treasureHunter = new Achieve("Treasure Hunter", "treasureHuntertest", "Coins", treasureHunterValue, treasureHunterReward, true);
        idiot = new Achieve("Idiot blyat?", "Coins", idiotTargerValue, idiotReward, false);
        swimmer = new Achieve("Swim lover test", "Coins", swimmerTargetValue, swimmerReward, false);
        torchCollector = new Achieve("No light on level anymore.", "Coins", torchCollectorTargetValue, torchCollectorReward, false);
        secretRoomer = new Achieve("Secret Rush test", "secretRoomerPref", "Coins", secretRoomerTargetValue, secretRoomerReward, true);
    }

    public void CheckAchieve(Achieve achieve)
    {
        if (achievementUI != null)
        {
            if (achieve.isEndless)
            {

                if (achieve.weight < 3)
                {
                    PlayerPrefs.SetInt(achieve.achieveName, PlayerPrefs.GetInt(achieve.achieveName) + 1);

                    if (PlayerPrefs.GetInt(achieve.achieveName) == achieve.targetValue[achieve.weight])
                    {
                        GameManager.CollectedCoins += achieve.reward[achieve.weight];
                        achieve.RewardUpdate();
                        achievementUI.GetComponent<AchievementUI>().AchievementAppear(achieve.achieveName);
                        StartCoroutine(achievementUI.GetComponent<AchievementUI>().AchievementDisappear());
                    }
                }
            }

            else if (!achieve.isEndless)
            {
                if (achieve.weight < 3)
                {
                    PlayerPrefs.SetInt(achieve.achieveName, PlayerPrefs.GetInt(achieve.achieveName) + 1);
                    if (PlayerPrefs.GetInt(achieve.achieveName) == achieve.targetValue[achieve.weight])
                    {
                        achieve.LevelRewardUpdate();
                        GameManager.CollectedCoins += achieve.reward[achieve.weight];
                        achievementUI.GetComponent<AchievementUI>().AchievementAppear(achieve.achieveName);
                        StartCoroutine(achievementUI.GetComponent<AchievementUI>().AchievementDisappear());
                    }
                }
            }
        }
        else
        {

        }
        
    }

    void ResetStat(string achieveName, string prefsName)
    {
        PlayerPrefs.SetInt(achieveName, 0);
        PlayerPrefs.SetInt(prefsName + "weight", 0);
    }

}
