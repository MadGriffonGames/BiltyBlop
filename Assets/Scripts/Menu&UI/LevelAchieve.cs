using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;


public class LevelAchieve : MonoBehaviour
{
    public int targetValue;
    public string achieveName;
    public string rewardType;
    public int reward;


    public LevelAchieve(string _name, string _rewardType, int _targetValue, int _reward)
    {
        
        if (PlayerPrefs.GetInt(_name) < _targetValue)
        {
            PlayerPrefs.SetInt(_name, 0);
            achieveName = _name;
            rewardType = _rewardType;
            targetValue = _targetValue;
            reward = _reward;
            PlayerPrefs.SetInt(achieveName + "targetValue", _targetValue);
        }
        else
            Destroy(this);
    }
}