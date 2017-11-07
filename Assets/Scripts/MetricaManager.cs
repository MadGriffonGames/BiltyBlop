using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MetricaManager : MonoBehaviour
{
    private static MetricaManager instance;
    public static MetricaManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<MetricaManager>();
            return instance;
        }
    }

    public string lastUnlockedLevel;
    public string currentLevel;

    public int deaths;

    public int rewindCount;
    public int rewardedCheckpoints;
    public int coinCheckpoints;
    public int crystalCheckpoints;

    public int collectedCoins;
    public int collectedStars;

    public float passingTime;
    public int integerPassingTime;

    public Dictionary<string, object> levelParams;

	void Start ()
    {
        DevToDevInitialize();

        levelParams = new Dictionary<string, object>();

        deaths              = 0;

        collectedCoins      = 0;
        collectedStars      = 0;

        rewindCount         = 0;
        rewardedCheckpoints = 0;
        coinCheckpoints     = 0;
        crystalCheckpoints  = 0;

        passingTime   = 0;

        currentLevel = SceneManager.GetActiveScene().name;
        lastUnlockedLevel = PlayerPrefs.GetString("LastUnlockedLevel");
    }

	void Update ()
    {
        passingTime += Time.deltaTime;
	}

    public void SetParametrs()
    {
        levelParams.Add("Level: ", currentLevel);

        integerPassingTime = (int) passingTime;
        levelParams.Add("Passing time: ", "" + integerPassingTime / 60 + " min " + integerPassingTime % 60 + " sec");

        levelParams.Add("Deaths: ", deaths);

        levelParams.Add("Coins: ", collectedCoins);
        levelParams.Add("Stars: ", collectedStars);

        levelParams.Add("Rewinds: ", rewindCount);
        levelParams.Add("Checkpoint packs for rewarded video: ", rewardedCheckpoints);
        levelParams.Add("Checkpoint packs for coins: ", coinCheckpoints);
        levelParams.Add("Checkpoint packs for crystals: ", crystalCheckpoints);
    }

    void DevToDevInitialize()
    {
        string lvlName = SceneManager.GetActiveScene().name;
        string tmp = "" + lvlName[lvlName.Length - 1];
        int currentLvl = int.Parse(tmp);

        DevToDev.Analytics.CurrentLevel(currentLvl);
    }
}
