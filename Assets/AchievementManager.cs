using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

    int killCounterTyplak;
    GameObject achievementUI;
    string typlakText = "Typlakation";
    string penguinText = "King of Penguins";

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckTyplak()
    {
        Debug.Log(PlayerPrefs.GetInt("Dead Typlak"));
        PlayerPrefs.SetInt("Dead Typlak", PlayerPrefs.GetInt("Dead Typlak") + 1);
        Debug.Log(PlayerPrefs.GetInt("Dead Typlak"));
        if (PlayerPrefs.GetInt("Dead Typlak") == 65)
        {
            GameManager.CollectedCoins += 100;
            achievementUI.GetComponent<AchievementUI>().AchievementAppear(typlakText);
            StartCoroutine(achievementUI.GetComponent<AchievementUI>().AchievementDisappear());
        }
    }

    public void CheckPenguin()
    {
        PlayerPrefs.SetInt("Dead Penguin", PlayerPrefs.GetInt("Dead Penguin") + 1);
        if (PlayerPrefs.GetInt("Dead Penguin") == 3)
        {
            GameManager.CollectedCoins += 150;
            achievementUI.GetComponent<AchievementUI>().AchievementAppear(penguinText);
            StartCoroutine(achievementUI.GetComponent<AchievementUI>().AchievementDisappear());
        }
    }

}
