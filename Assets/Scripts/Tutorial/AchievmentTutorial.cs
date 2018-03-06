using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentTutorial : SceneTutorial
{
    [SerializeField]
    Map map;

    bool tmp = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt(ACHIEVEMENT_TUTORIAL_COMPLETE) == 0 && PlayerPrefs.GetInt(SHOP_TUTORIAL_COMPLETE) == 1)
        {
            PlayerPrefs.SetInt("TutorialMode", 1);
        }

        if (isItTimeForTutorial() && PlayerPrefs.GetInt("TutorialMode") > 0 && PlayerPrefs.GetInt(ACHIEVEMENT_TUTORIAL_COMPLETE) == 0 && PlayerPrefs.GetInt(SHOP_TUTORIAL_COMPLETE) == 1)
        {
            if (map)
            {
                map.SetAchievementsIndication();
            }
        }
    }
}
