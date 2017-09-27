using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Achieve : MonoBehaviour
{
    public string achieveName;
    public int targetValue;
    public string rewardType;
    public int reward;
    //public int weight;
    public string prefsName;
    public bool isEndless;
    public int unlocked;

    const string targetValue0 = "targetValue0";
    const string targetValue1 = "targetValue1";
    const string targetValue2 = "targetValue2";
    const string targetValueString = "targetValue";
    const string weightString = "weight";
    const string rewardTypeString = "rewardType";
    const string rewardString = "reward";
    const string reward1 = "1";
    const string reward2 = "2";
    const string unlockedString = "unlocked";



    public Achieve(string _name, string _prefsName, string _rewardType, int _targetValue, int _reward, bool _isEndless)
    {
        achieveName = _name;
        prefsName = _prefsName;
        PlayerPrefs.SetString(prefsName + rewardTypeString, _rewardType);
        PlayerPrefs.SetInt(prefsName + targetValueString, _targetValue);
        PlayerPrefs.SetInt(prefsName + rewardString, _reward);
        isEndless = _isEndless;


        if (PlayerPrefs.HasKey(prefsName + unlockedString))
        {
            unlocked = PlayerPrefs.GetInt(prefsName + unlockedString);
        }
        else
        {
            PlayerPrefs.SetInt(prefsName + unlockedString, 0);
            unlocked = 0;
        }

        /*isEndless = _isEndless;
        rewardType = _rewardType;
        reward = _reward;
        achieveName = _name;
        prefsName = _prefsName;

        if (PlayerPrefs.HasKey(prefsName + unlockedString))
        {
            unlocked = PlayerPrefs.GetInt(prefsName + unlockedString);
        }
        else unlocked = 0;

        if (PlayerPrefs.HasKey(prefsName + targetValueString))
            targetValue = PlayerPrefs.GetInt(prefsName + targetValueString);
        else
        {
            targetValue = _targetValue;
            PlayerPrefs.SetInt(prefsName + targetValueString, targetValue);
        }

        PlayerPrefs.SetInt(prefsName + rewardString, _reward);*/


        /*if (PlayerPrefs.HasKey(prefsName + targetValue0) && PlayerPrefs.GetInt(prefsName + targetValue0) != _targetValue[0])
        {
            PlayerPrefs.SetInt(prefsName + targetValue0, _targetValue[0]);
        }

        if (PlayerPrefs.HasKey(prefsName + targetValue1) && PlayerPrefs.GetInt(prefsName + targetValue1) != _targetValue[1])
        {
            PlayerPrefs.SetInt(prefsName + targetValue1, _targetValue[1]);
        }

        if (PlayerPrefs.HasKey(prefsName + targetValue2) && PlayerPrefs.GetInt(prefsName + targetValue2) != _targetValue[2])
        {
            PlayerPrefs.SetInt(prefsName + targetValue2, _targetValue[2]);
        }

        
        if (PlayerPrefs.HasKey(prefsName + targetValue0))
        {
            targetValue[0] = PlayerPrefs.GetInt(prefsName + targetValue0);
            targetValue[1] = PlayerPrefs.GetInt(prefsName + targetValue1);
            targetValue[2] = PlayerPrefs.GetInt(prefsName + targetValue2);
        }
        else if (!PlayerPrefs.HasKey(prefsName + targetValue0))
        {
            targetValue = _targetValue; 
            PlayerPrefs.SetInt(prefsName + targetValue0, _targetValue[0]);
            PlayerPrefs.SetInt(prefsName + targetValue1, _targetValue[1]);
            PlayerPrefs.SetInt(prefsName + targetValue2, _targetValue[2]);
        }
        */

        /*if (PlayerPrefs.HasKey(prefsName + weightString))
            weight = PlayerPrefs.GetInt(prefsName + weightString);
        else
        {
            PlayerPrefs.SetInt(prefsName + weightString, 0);
            weight = PlayerPrefs.GetInt(prefsName + weightString);
        }*/

        //if (PlayerPrefs.HasKey(prefsName + rewardTypeString))
        //    rewardType = PlayerPrefs.GetString(prefsName + rewardTypeString);
        //else
        //{
        //    PlayerPrefs.SetString(prefsName + rewardTypeString, _rewardType);
        //    rewardType = PlayerPrefs.GetString(prefsName + rewardTypeString);
        //}

        //if (PlayerPrefs.HasKey(prefsName + reward0) && PlayerPrefs.GetInt(prefsName + reward0) != _reward[0])
        //    PlayerPrefs.SetInt(prefsName + reward0, _reward[0]);

        //if (PlayerPrefs.HasKey(prefsName + reward1) && PlayerPrefs.GetInt(prefsName + reward1) != _reward[1])
        //    PlayerPrefs.SetInt(prefsName + reward1, _reward[1]);

        //if (PlayerPrefs.HasKey(prefsName + reward2) && PlayerPrefs.GetInt(prefsName + reward2) != _reward[2])
        //    PlayerPrefs.SetInt(prefsName + reward2, _reward[2]);

        //try
        //{
        //    if (PlayerPrefs.HasKey(prefsName + reward0))
        //    {
        //        reward[0] = PlayerPrefs.GetInt(prefsName + reward0);
        //        reward[1] = PlayerPrefs.GetInt(prefsName + reward1);
        //        reward[2] = PlayerPrefs.GetInt(prefsName + reward2);
        //    }
        //    else
        //    {
        //        reward = _reward;
        //        PlayerPrefs.SetInt(prefsName + reward0, _reward[0]);
        //        PlayerPrefs.SetInt(prefsName + reward1, _reward[1]);
        //        PlayerPrefs.SetInt(prefsName + reward2, _reward[2]);
        //    }
        //}
        //catch (NullReferenceException e)
        //{
        //    Debug.Log("Massive of Reward is Empty");
        //}

    }

    public Achieve(string _name, string _rewardType, int _targetValue, int _reward, bool _isEndless)
    {
        isEndless = _isEndless;
        achieveName = _name;
        rewardType = _rewardType;
        targetValue = _targetValue;
        reward = _reward;
        if (PlayerPrefs.HasKey(prefsName + unlockedString))
        {
            unlocked = 1;
        }
        else
        {
            unlocked = 0;
            PlayerPrefs.SetInt(prefsName + unlockedString, 1);
        }
        //PlayerPrefs.SetInt(achieveName, 0);
    }

    /*public void RewardUpdate()
    {
        if (weight >= 3)
            Destroy(this);
        weight++;
        PlayerPrefs.SetInt(prefsName + weightString, weight);
    }*/


    public void UnlockAchieve()
    {
        unlocked = 1;
        PlayerPrefs.SetInt(prefsName + unlockedString, 1);
    }

    public void UnlockLevelAchieve()
    {
        unlocked = 1;
    }
}
