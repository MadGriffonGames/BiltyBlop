using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Achieve : MonoBehaviour
{
    public string achieveName;
    public int[] targetValue;
    public string rewardType;
    public int[] reward;
    public int weight;
    int step;
    string prefsName;

    //public Achieve(string _name, string _rewardType,int _targetValue, int[] _reward, int _step)
    //{
    //    name = _name;
    //    rewardType = _rewardType;
    //    targetValue = _targetValue;
    //    weight = 0;
    //    reward = _reward;
    //    step = _step;
    //}

    public Achieve(string _name, string _prefsName, string _rewardType, int[] _targetValue, int[] _reward, int _step)
    {
        targetValue = new int[3];
        reward = new int[3];
        achieveName = _name;
        prefsName = _prefsName;
        Debug.Log(PlayerPrefs.HasKey(prefsName + "targetValue0"));
        if (PlayerPrefs.HasKey(prefsName + "targetValue0"))
        {
            targetValue[0] = PlayerPrefs.GetInt(prefsName + "targetValue0");
            targetValue[1] = PlayerPrefs.GetInt(prefsName + "targetValue1");
            targetValue[2] = PlayerPrefs.GetInt(prefsName + "targetValue2");
        }
        else if (!PlayerPrefs.HasKey(prefsName + "targetValue0"))
        {
            targetValue = _targetValue; // PlayerPrefs.GetInt(prefsName + "targetValue");
            PlayerPrefs.SetInt(prefsName + "targetValue0", _targetValue[0]);
            PlayerPrefs.SetInt(prefsName + "targetValue1", _targetValue[1]);
            PlayerPrefs.SetInt(prefsName + "targetValue2", _targetValue[2]);
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
            if (PlayerPrefs.HasKey(prefsName + "0"))
            {
                reward[0] = PlayerPrefs.GetInt(prefsName + "0");
                reward[1] = PlayerPrefs.GetInt(prefsName + "1");
                reward[2] = PlayerPrefs.GetInt(prefsName + "2");
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
        if (weight >= 3)
            Destroy(this);

        weight++;
        PlayerPrefs.SetInt(prefsName + "weight", weight++);
    }

}
