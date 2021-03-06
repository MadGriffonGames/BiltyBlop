﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentTutorial : SceneTutorial
{
    [SerializeField]
    Map map;

    void Start ()
    {
        if (isItTimeForTutorial() && PlayerPrefs.GetInt("TutorialMode") > 0 && PlayerPrefs.GetInt(ACHIEVEMENT_TUTORIAL_COMPLETE) == 0)
        {
            if (PlayerPrefs.GetInt("TutorialAchieve") == 0)
            {
                AchievementManager.Instance.CheckLevelAchieve(AchievementManager.Instance.tutorialAchieve);
            }
            if (map)
            {
                map.SetAchievementsIndication();
            }
        }
	}
}
