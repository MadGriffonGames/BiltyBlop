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
    public int[] reward;
    public int weight;
    int step;
    string prefsName;

    public Achieve(string _name, string _rewardType,int _targetValue, int[] _reward, int _step)
    {
        name = _name;
        rewardType = _rewardType;
        targetValue = _targetValue;
        weight = 0;
        reward = _reward;
        step = _step;
    }

    public Achieve(string _name, string _prefsName, string _rewardType, int _targetValue, int[] _reward, int _step)
    {
        achieveName = _name;
        prefsName = _prefsName;
        if (PlayerPrefs.HasKey(prefsName + "targetValue"))
            targetValue = PlayerPrefs.GetInt(prefsName + "targetValue");
        else
        {
            PlayerPrefs.SetInt(prefsName + "targetValue", _targetValue);
            targetValue = PlayerPrefs.GetInt(prefsName + "targetValue");
        }

        if (PlayerPrefs.HasKey(prefsName + "weight"))
            weight = PlayerPrefs.GetInt(prefsName + "weight");
        else
        {
            PlayerPrefs.SetInt(prefsName + "weight", 0);
            weight = PlayerPrefs.GetInt(prefsName + "weight");
        }

        if (PlayerPrefs.HasKey(prefsName + "rewardType"))
            rewardType = PlayerPrefs.GetString(prefsName + "rewardType");
        else
        {
            PlayerPrefs.SetString(prefsName + "rewardType", _rewardType);
            rewardType = PlayerPrefs.GetString(prefsName + "rewardType");
        }

        if (PlayerPrefs.HasKey(prefsName + "step"))
            step = PlayerPrefs.GetInt(prefsName + "step");
        else
        {
            PlayerPrefs.SetInt(prefsName + "step", _step);
            step = PlayerPrefs.GetInt(prefsName + "step");
        }


        try
        {
            if (!PlayerPrefs.HasKey(prefsName + "0"))
            {
                reward = _reward;
                Debug.Log(reward[1]);
            }
            else
            {
                reward = _reward;
                PlayerPrefs.SetInt(prefsName + "0", _reward[0]);
                PlayerPrefs.SetInt(prefsName + "1", _reward[1]);
                PlayerPrefs.SetInt(prefsName + "2", _reward[2]);
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Massive of Reward is Empty");
        }
    }

    public void RewardUpdate()
    {
        weight++;
        if (weight >= 4)
            Destroy(this);
        PlayerPrefs.SetInt(prefsName + "weight", weight++);
        targetValue += step;
        PlayerPrefs.SetInt(prefsName + "targetValue", targetValue);
    }

}
