using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialOffer : MonoBehaviour
{
    [SerializeField]
    GameObject starterPackWindow;
    [SerializeField]
    GameObject fade;

    private void Awake()
    {
        string targetLevelName = "Level4";
        string nextLevel = "Level" + (4 + 1).ToString();
        string targetLevelPlusTwo = "Level" + (4 + 2).ToString();

        if (PlayerPrefs.GetString("LastCompletedLevel") == targetLevelName && PlayerPrefs.GetInt(nextLevel) == 1 && PlayerPrefs.GetInt(targetLevelPlusTwo) == 0)
        {
            starterPackWindow.SetActive(true);
            fade.SetActive(true);
        }
    }
}
