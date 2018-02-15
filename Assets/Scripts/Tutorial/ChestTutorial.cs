using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTutorial : SceneTutorial
{
    [SerializeField]
    GameObject light;

    private void Start()
    {
        if (isItTimeForTutorial() && PlayerPrefs.GetInt("TutorialMode") > 0 && PlayerPrefs.GetInt(CHEST_TUTORIAL_COMPLETE) == 0)
        {
            light.SetActive(false);
        }
    }
}
