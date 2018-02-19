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
        Debug.Log(AchievementManager.Instance.tutorialAchieve);
    }

    private void Update()
    {
        if (!tmp)
        {
            tmp = true;
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
}
