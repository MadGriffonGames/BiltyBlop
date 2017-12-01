using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    GameObject[] maps;

    private void Start()
    {
        SetMap();
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
}
