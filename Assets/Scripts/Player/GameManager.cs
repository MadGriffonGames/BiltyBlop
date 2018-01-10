using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int collectedCoins;

    public static int torches;

    bool isBirded = false;



    public static int CollectedCoins
    {
        get
        {
            return collectedCoins;
        }
        set
        {
            collectedCoins = value;
        }
    }

    [SerializeField]
    public Text coinTxt;
    [SerializeField]
    public static Text crystalTxt;
    [SerializeField]
    public GameObject adsManager;
    [SerializeField]
    public GameObject metricaManager;
    [SerializeField]
    public GameObject achievementManager;
    [SerializeField]
    GameObject localiztionManager;

    public static string nextLevelName;
    public static int lvlCollectedCoins;
    public static List<GameObject> deadEnemies;
    [SerializeField]
    GameObject bird;

    public static string currentLvl;
    bool isLevel;

	void Awake()
	{      
        #if UNITY_EDITOR
			Application.targetFrameRate = 1000;
#elif UNITY_ANDROID
        Application.targetFrameRate = 60;
#elif UNITY_IOS
			Application.targetFrameRate = 60;
#endif

        Instantiate(achievementManager);

        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            crystalTxt = GameObject.FindGameObjectWithTag("CrystalTxt").GetComponent<Text>();
        }
        coinTxt = GameObject.Find("CoinTxt").GetComponent<Text>();

        currentLvl = SceneManager.GetActiveScene().name;

        AppMetrica.Instance.ReportEvent("#ENTER in " + currentLvl);
        DevToDev.Analytics.CustomEvent("#ENTER in " + currentLvl);
#if UNITY_EDITOR
        if (!FindObjectOfType<LocalizationManager>())
        {
            Instantiate(localiztionManager);
        }
#endif
    }

    void Start () 
	{      
        if (!PlayerPrefs.HasKey("NoAds"))
        {
            PlayerPrefs.SetInt("NoAds", 0);
        }

        isLevel = currentLvl.Contains("Level") ? true : false;

        Instantiate(adsManager);
        Instantiate(metricaManager);

        CheckLastUnlockedLevel();

        deadEnemies = new List<GameObject>();

        SetMoneyValues();

        if (PlayerPrefs.HasKey("Crystals") && crystalTxt != null)
        {
            crystalTxt.text = PlayerPrefs.GetInt("Crystals").ToString();
        }

        if (!PlayerPrefs.HasKey("Skin"))
        {
            PlayerPrefs.SetString("Skin", "Classic");
        }       
        
        if((SceneManager.GetActiveScene().name != "MainMenu") && (SceneManager.GetActiveScene().name != "Level10") && (SceneManager.GetActiveScene().name != "Map") && (SceneManager.GetActiveScene().name != "AchievementMenu") && (SceneManager.GetActiveScene().name != "Shop") && (SceneManager.GetActiveScene().name != "Level1"))
            SoundManager.PlayRandomMusic ("kid_music", true);
        if (SceneManager.GetActiveScene().name == "Level6")
            SoundManager.PlaySoundLooped("rain sfx");

        if (SceneManager.GetActiveScene().name == "Level1")
            SoundManager.PlayMusic("kid_music_1", true);
        lvlCollectedCoins = 0;
    }

    void Update()
    {        
        if (isLevel && UI.Instance.isActiveAndEnabled)
        {
            coinTxt.text = (" " + collectedCoins);
        }
        if (torches == 0 && isBirded == false)
        {
            ThrowBird();
            AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.torchCollector3);
        }
    }

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    void GetReward(string achieveName, string achievePrefsName)
    {
        if (PlayerPrefs.GetString(achievePrefsName + "rewardType") == "Coins")
        {
            collectedCoins += PlayerPrefs.GetInt(achievePrefsName + "reward");
        }

        if (PlayerPrefs.GetString(achievePrefsName + "rewardType") == "Gems")
        {
            PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + PlayerPrefs.GetInt(achievePrefsName + "reward"));
        }
    }

    void SetMoneyValues()
    {
        if (!PlayerPrefs.HasKey("Coins"))
        {
            collectedCoins = 0;
            PlayerPrefs.SetInt("Coins", collectedCoins);
            PlayerPrefs.SetInt("Millionare Test", collectedCoins);
        }
        else
        {
            collectedCoins = PlayerPrefs.GetInt("Coins");
        }

        if (!PlayerPrefs.HasKey("Crystals"))
        {
            PlayerPrefs.SetInt("Crystals", 0);
        }
    }

    void ThrowBird()
    {
        try
        {
            if (isBirded == false)
            {
                isBirded = true;
                Vector3 firstPoint = new Vector3(Player.Instance.transform.position.x - 10, Player.Instance.transform.position.y, Player.Instance.transform.position.z);
                GameObject tmp = (GameObject)Instantiate(bird, new Vector3(), Quaternion.Euler(0, 0, 0));
                tmp.transform.position = firstPoint;
            }
        }
        catch (UnassignedReferenceException) { }
    }

    void CheckLastUnlockedLevel()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            for (int i = 1; i <= 20; i++)
            {
                if (PlayerPrefs.HasKey("Level" + i))
                {
                    if (PlayerPrefs.GetInt("Level" + i) > 0)
                    {
                        MetricaManager.Instance.lastUnlockedLevel = "Level" + i;
                    }
                }
                
            }
            if (PlayerPrefs.GetString("LastUnlockedLevel") != MetricaManager.Instance.lastUnlockedLevel)
            {
                PlayerPrefs.SetString("LastUnlockedLevel", MetricaManager.Instance.lastUnlockedLevel);
            }
        }
    }

    public static void AddCoins(int value)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + value);
    }

    public static void AddCrystals(int value)
    {
        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + value);
    }

	public static void DestroyDeadEnemies()
	{
		foreach (GameObject enemy in deadEnemies) 
		{
			Destroy (enemy.gameObject);
		}
	}

    [ContextMenu("DeleteAll")]
    void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    [ContextMenu("AddMoney")]
    void AddMoney()
    {
        AddCoins(9999);
        AddCrystals(9999);
    }
}