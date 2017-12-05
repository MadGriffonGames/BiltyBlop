using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTutorial : SceneTutorial
{
    [SerializeField]
    GameObject light;

    string TUTORIAL_TEXT = "look at this chest! tap on it to get your reward";

    private void Start()
    {
        if (PlayerPrefs.GetInt("TutorialMode") > 0)
        {
            light.SetActive(false);
            tutorialDescriptionText.text = TUTORIAL_TEXT;
        }
    }

    public void DisableTutorial()
    {
        EnableButtons();
        highlighter.SetActive(false);
        textBar.SetActive(false);
        fade.SetActive(false);
        PlayerPrefs.SetInt("TutorialMode", 0);
    } 
}
