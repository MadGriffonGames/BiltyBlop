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
    void Start () {
        achievementUI = GameObject.FindGameObjectWithTag("Achievement UI");
        achievement = new Achieve[20];
        //PlayerPrefs.SetInt("Penguin's king", 0);
        penguinReward = new int[] { 100, 150, 200 };
        penguinAchieve = new Achieve("Penguin's kingtest", "penguinPrefstest","Coins", 1, penguinReward, 1);
        mobReward = new int[] { 1000, 1500, 5000 };
        mobKiller = new Achieve("Mob killer test", "mobKillerPrefTest", "Coins", 1, penguinReward, 100); 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckAchieve(Achieve achieve)
    { 
        Debug.Log(PlayerPrefs.GetInt(achieve.achieveName));
        PlayerPrefs.SetInt(achieve.achieveName, PlayerPrefs.GetInt(achieve.achieveName) + 1);
        Debug.Log(PlayerPrefs.GetInt(achieve.achieveName));
        if (PlayerPrefs.GetInt(achieve.achieveName) == achieve.targetValue)
        {
            GameManager.CollectedCoins += achieve.reward[achieve.weight];
            achieve.RewardUpdate();
            achievementUI.GetComponent<AchievementUI>().AchievementAppear(achieve.achieveName);
            StartCoroutine(achievementUI.GetComponent<AchievementUI>().AchievementDisappear());
        }
    }



}
