using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialOffer : MonoBehaviour
{
    [SerializeField]
    GameObject starterPackWindow;
    [SerializeField]
    GameObject pack1Window;
    [SerializeField]
    GameObject pack1WithNoAdsWindow;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    int[] targetLevels;

    const string WINDOW_SHOWN_AFTER = "WindowAfterLevel";

    private void Awake()
    {
        CheckTargetLevel();
    }

    void CheckTargetLevel()
    {
        string lastLevel = PlayerPrefs.GetString("LastCompletedLevel");
        int lastLevelNum = int.Parse(lastLevel.Remove(0, 5));

        for (int i = 0; i < targetLevels.Length; i++)
        {
            if (targetLevels[i] == lastLevelNum)
            {
                EnablePackWindow(lastLevel);
            }
        }
    }

    void EnablePackWindow(string lastLevel)
    {
        if (PlayerPrefs.GetInt(WINDOW_SHOWN_AFTER + lastLevel) == 0)
        {
            fade.SetActive(true);
            if (PlayerPrefs.GetInt("StarterPackBought") == 0)
            {
                starterPackWindow.SetActive(true);
                PlayerPrefs.SetInt(WINDOW_SHOWN_AFTER + lastLevel, 1);
            }
            else if (PlayerPrefs.GetInt("Pack1_NoAdsBought") == 0)
            {
                pack1WithNoAdsWindow.SetActive(true);
                PlayerPrefs.SetInt(WINDOW_SHOWN_AFTER + lastLevel, 1);
            }
            else if (PlayerPrefs.GetInt("Pack1Bought") == 0)
            {
                pack1Window.SetActive(true);
                PlayerPrefs.SetInt(WINDOW_SHOWN_AFTER + lastLevel, 1);
            }
        }
    }
}
