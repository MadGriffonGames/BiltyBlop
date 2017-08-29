using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

    int killCounterTyplak;
    GameObject achievementUI;

    Achieve[] achievement;

    string typlakText = "You have killed 1 enemy";
    string penguinText = "You have killed 1 enemy";

    public Achieve penguinAchieve;
    int[] penguinReward;
    int[] mobKillerTargetValue;

    public Achieve mobKiller;
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

    // Use this for initialization
    void Start() {
        achievementUI = GameObject.FindGameObjectWithTag("Achievement UI");
        achievement = new Achieve[20];
        //PlayerPrefs.SetInt("Penguin's king", 0);
        penguinReward = new int[] { 100, 150, 200 };
        mobKillerTargetValue = new int[] { 10, 15, 20 };
        //penguinAchieve = new Achieve("Penguin's kingtest", "penguinPrefstest","Coins", 1, penguinReward, 1);
        mobReward = new int[] { 1000, 1500, 5000 };
        mobKiller = new Achieve("Mob killer test", "mobKillerPrefTest1", "Coins", mobKillerTargetValue, penguinReward, 100); 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckAchieve(Achieve achieve)
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
