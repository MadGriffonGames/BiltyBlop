using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CheckAchievementIndicator : MonoBehaviour
{
    public const string availableAchievements = "avaliableLoots";
    [SerializeField]
    GameObject availableAchievementsCounter;
    [SerializeField]
    GameObject greenCircleAchieve;

    private void OnEnable()
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
