using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SceneTutorial : MonoBehaviour
{
    [SerializeField]
    int targetLevelNum;
    [SerializeField]
    Button[] buttonsToDisable;
    [SerializeField]
    protected GameObject highlighter;
    [SerializeField]
    protected GameObject textBar;
    [SerializeField]
    protected GameObject fade;
    [SerializeField]
    protected Text tutorialDescriptionText;

    string targetLevelName;

    protected void Awake()
    {
        if (PlayerPrefs.GetInt("TutorialMode") > 0)
        {
            PlayerPrefs.SetInt("TutorialMode", 0);
        }
        else
        {
            targetLevelName = "Level" + targetLevelNum.ToString();
            if (isItTimeForTutorial())
            {
                DisableButtons();
                highlighter.SetActive(true);
                textBar.SetActive(true);
                fade.SetActive(true);
                PlayerPrefs.SetInt("TutorialMode", 1);
            }
        }
    }

    void DisableButtons()
    {
        for (int i = 0; i < buttonsToDisable.Length; i++)
        {
            buttonsToDisable[i].enabled = false;
        }
    }

    protected void EnableButtons()
    {
        for (int i = 0; i < buttonsToDisable.Length; i++)
        {
            buttonsToDisable[i].enabled = true;
        }
    }

    bool isItTimeForTutorial()
    {
        string nextLevel = "Level" + (targetLevelNum + 1).ToString();
        string targetLevelPlusTwo = "Level" + (targetLevelNum + 2).ToString();

        return PlayerPrefs.GetString("LastCompletedLevel") == targetLevelName && PlayerPrefs.GetInt(nextLevel) == 1 && PlayerPrefs.GetInt(targetLevelPlusTwo) == 0;
    }
}
