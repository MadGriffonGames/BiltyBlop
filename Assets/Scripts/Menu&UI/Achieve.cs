using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Achieve : MonoBehaviour
{
    public string achieveName;
    public int[] targetValueArray;
    public string rewardType;
    public int[] rewardArray;
    public int reward;
    public int weight;
    public bool isEndless;
    public int counter;
    public bool threeLevel;
    public int targetValue;

    const string targetValue0 = "targetValue0";
    const string targetValue1 = "targetValue1";
    const string targetValue2 = "targetValue2";
    const string weightString = "weight";
    const string isEndlessString = "isEndless";
    const string rewardTypeString = "rewardType";
    const string rewardString = "reward";
    const string reward0 = "0";
    const string reward1 = "1";
    const string reward2 = "2";
    const string unlocked = "unlocked";



    public Achieve(string _name, string _rewardType, int[] _targetValue, int[] _reward)
    {
        threeLevel = true;

        achieveName = _name;
        //------------------------------------------------- NAME ---------------------------------------------------------
        //PlayerPrefs.SetString(achieveName, _name);

        //------------------------- REWARD TYPE --------------------------------------------------------------------------
        rewardType = _rewardType;

        //if (!PlayerPrefs.HasKey(_name + rewardTypeString))
        //{
        //    PlayerPrefs.SetString(_name + rewardTypeString, _rewardType);
        //}

        //------------------------------------------ TARGET VALUE SECTION STARTS-------------------------------------------


        targetValueArray = _targetValue;

        if (PlayerPrefs.HasKey(_name + targetValue0))
        {
            PlayerPrefs.SetInt(_name + targetValue0, _targetValue[0]);
        }

        if (PlayerPrefs.HasKey(_name + targetValue1))
        {
            PlayerPrefs.SetInt(_name + targetValue1, _targetValue[1]);
        }

        if (PlayerPrefs.HasKey(_name + targetValue2))
        {
            PlayerPrefs.SetInt(_name + targetValue2, _targetValue[2]);
        }

        //------------------------------------------ REWARDS --------------------------------------------------------------

        rewardArray = _reward;
        //if (!PlayerPrefs.HasKey(_name + reward0))
        //{
        //    PlayerPrefs.SetInt(_name + reward0, _reward[0]);
        //}

        //if (!PlayerPrefs.HasKey(_name + reward1))
        //{
        //    PlayerPrefs.SetInt(_name + reward1, _reward[1]);
        //}

        //if (!PlayerPrefs.HasKey(_name + reward2))
        //{
        //    PlayerPrefs.SetInt(_name + reward2, _reward[2]);
        //}
        //--------------------------------------------- WEIGHT ----------------------------------------------------------

        if (PlayerPrefs.HasKey(_name + weightString))
        {
            weight = PlayerPrefs.GetInt(_name + weightString);
        }
        else
        {
            weight = 0;
        }


        //if (!PlayerPrefs.HasKey(_name + isEndlessString))
        //{
        //    PlayerPrefs.SetInt(_name + isEndlessString, _isEndless);
        //}        
    }


    public Achieve(string _name, string _rewardType, int _targetValue, int _reward)
    {
        if (PlayerPrefs.GetInt(achieveName) < _targetValue)
        {
            threeLevel = false;
            achieveName = _name;
            rewardType = _rewardType;
            targetValue = _targetValue;
            //if (!PlayerPrefs.HasKey(achieveName + "targetValue"))
            //{
            //    PlayerPrefs.SetInt(achieveName + targetValue, _targetValue);
            //}
            reward = _reward;
        }
        else
            Destroy(this);
    }
}
