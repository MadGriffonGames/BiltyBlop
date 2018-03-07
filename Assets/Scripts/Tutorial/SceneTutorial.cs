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
    [SerializeField]
    GameObject targetObject;
    [SerializeField]
    Transform frontPlan;
    [SerializeField]
    string description;
    [SerializeField]
    int fontSize;
    [SerializeField]
    int tutorialType;

    string targetLevelName;

    public const string CHEST_TUTORIAL_COMPLETE = "ChestTutorialComplete";
    public const string SHOP_TUTORIAL_COMPLETE = "ShopTutorialComplete";
    public const string ACHIEVEMENT_TUTORIAL_COMPLETE = "AchievementTutorialComplete";

    protected void Awake()
    {
        targetLevelName = "Level" + targetLevelNum.ToString();

        if (isTutorialAvailable())
        {
            if (isItTimeForTutorial())
            {
                if (frontPlan)
                {
                    MoveToFrontPlan(targetObject);
                }
                DisableButtons();
                highlighter.SetActive(true);
                textBar.SetActive(true);
                tutorialDescriptionText.text = description;
                if (fade)
                {
                    fade.SetActive(true);
                }
                if (fontSize != 0)
                {
                    tutorialDescriptionText.fontSize = fontSize;
                }
                PlayerPrefs.SetInt("TutorialMode", 1);
            }
        }       
        else if(isItTimeForTutorial())
        {
            PlayerPrefs.SetInt("TutorialMode", 0);
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

    protected bool isItTimeForTutorial()
    {
        string nextLevel = "Level" + (targetLevelNum + 1).ToString();
        string targetLevelPlusTwo = "Level" + (targetLevelNum + 2).ToString();

        return PlayerPrefs.GetString("LastCompletedLevel") == targetLevelName && PlayerPrefs.GetInt(nextLevel) == 1 && (PlayerPrefs.GetInt(targetLevelPlusTwo) == 0 || GameManager.developmentBuild);
    }

    public bool isTutorialAvailable()
    {
        switch (tutorialType)
        {
            case 1:
                return PlayerPrefs.GetInt(CHEST_TUTORIAL_COMPLETE) == 0;

            case 2:
                return PlayerPrefs.GetInt(SHOP_TUTORIAL_COMPLETE) == 0;

            case 3:
                return PlayerPrefs.GetInt(ACHIEVEMENT_TUTORIAL_COMPLETE) == 0 && PlayerPrefs.GetInt(SHOP_TUTORIAL_COMPLETE) == 1;

            default:
                return false;
        }
    }

    void MoveToFrontPlan(GameObject targetObject)
    {
        targetObject.transform.SetParent(frontPlan);
    }

    public void DisableTutorial()
    {
        EnableButtons();
        highlighter.SetActive(false);
        textBar.SetActive(false);
        if (fade)
        {
            fade.SetActive(false);
        }
        PlayerPrefs.SetInt("TutorialMode", 0);
        switch (targetLevelNum)
        {
            case 1:
                PlayerPrefs.SetInt(CHEST_TUTORIAL_COMPLETE, 1);
                break;

            case 2:
                PlayerPrefs.SetInt(SHOP_TUTORIAL_COMPLETE, 1);
                break;

            case 3:
                PlayerPrefs.SetInt(ACHIEVEMENT_TUTORIAL_COMPLETE, 1);
                break;

            default:
                break;
        }
           
    }
}
