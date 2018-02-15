using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;


public class LevelAchieve
{
    public int targetValue;
    public string achieveName;
    public string rewardType;
    public int reward;


    public LevelAchieve(string _name, string _rewardType, int _targetValue, int _reward)
    {
        if (PlayerPrefs.GetInt(_name) < _targetValue)
        {
            achieveName = _name;
            PlayerPrefs.SetInt(achieveName, 0);
            PlayerPrefs.SetString(achieveName + "rewardType", _rewardType);
            PlayerPrefs.SetInt(achieveName + "reward", _reward);
            rewardType = _rewardType;
            targetValue = _targetValue;
            reward = _reward;
            PlayerPrefs.SetInt(achieveName + "targetValue", _targetValue);
        }
        else
        {
            PlayerPrefs.SetInt(_name, _targetValue);
            //Destroy(this);
        }
    }
}