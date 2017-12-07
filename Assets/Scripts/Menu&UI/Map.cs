using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    GameObject[] maps;
    [SerializeField]
    GameObject availableAchievementsCounter;
    [SerializeField]
    GameObject greenCircleAchieve;
    public const string availableAchievements = "avaliableLoots";

    private void Start()
    {
        SetMap();
        SetAchievementsIndication();
    }

    public void ChangeScene(string sceneName)
    {
        GameManager.nextLevelName = sceneName;
        if (sceneName != "MainMenu")
        {
            PlayerPrefs.SetInt("FromMap", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FromMap", 0);
        }
        SceneManager.LoadScene("Loading");
    }

    void SetMap()
    {
        string lvlName = PlayerPrefs.GetString("LastCompletedLevel");
        string tmp = "" + lvlName[lvlName.Length - 1];
        int lastCompletedLvl = int.Parse(tmp);

        tmp = "" + lvlName[lvlName.Length - 2];
        if (char.IsDigit(tmp[0]))
        {
            lastCompletedLvl += int.Parse(tmp) * 10;
        }

        if (lastCompletedLvl < 10)
        {
            maps[0].SetActive(true);
        }
        else if (lastCompletedLvl < 20)
        {
            maps[1].SetActive(true);
        }
        else if (lastCompletedLvl < 30)
        {
            maps[2].SetActive(true);
        }
    }

    public void SetAchievementsIndication()
    {
        if (PlayerPrefs.GetInt(availableAchievements) != 0)
        {
            greenCircleAchieve.SetActive(true);
            availableAchievementsCounter.GetComponent<Text>().text = PlayerPrefs.GetInt(availableAchievements).ToString();
        }
        else if (PlayerPrefs.GetInt(availableAchievements) == 0 || !PlayerPrefs.HasKey(availableAchievements))
        {
            availableAchievementsCounter.GetComponent<Text>().text = "";
            greenCircleAchieve.SetActive(false);
        }
    }
}
