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
    public string[] rewardType;
    public int[] rewardArray;
    public int reward;
    public int weight;
    public bool isEndless;
    public int counter;
    public bool threeLevel;
    public int targetValue;

    const string itemType0 = "itemType0";
    const string itemType1 = "itemType1";
    const string itemType2 = "itemType2";
    const string targetValue0 = "targetValue0";
    const string targetValue1 = "targetValue1";
    const string targetValue2 = "targetValue2";
    const string weightString = "weight";
    const string isEndlessString = "isEndless";
    const string rewardType0 = "rewardType0";
    const string rewardType1 = "rewardType1";
    const string rewardType2 = "rewardType2";
    const string rewardString = "reward";
    const string reward0 = "0";
    const string reward1 = "1";
    const string reward2 = "2";
    const string unlocked = "unlocked";
    const string multiLevel = "multilevel";



    public Achieve(string _name, string[] _rewardType, int[] _targetValue, int[] _reward)
    {
        threeLevel = true;

        achieveName = _name;
        PlayerPrefs.SetInt(achieveName + multiLevel, 1);
        //------------------------------------------------- NAME ---------------------------------------------------------

        //------------------------- REWARD TYPE --------------------------------------------------------------------------
        rewardType = _rewardType;
        PlayerPrefs.SetString(achieveName + rewardType0, _rewardType[0]);
        PlayerPrefs.SetString(achieveName + rewardType1, _rewardType[1]);
        PlayerPrefs.SetString(achieveName + rewardType2, _rewardType[2]);
        PlayerPrefs.SetInt(achieveName + reward0, _reward[0]);
        PlayerPrefs.SetInt(achieveName + reward1, _reward[1]);
        PlayerPrefs.SetInt(achieveName + reward2, _reward[2]);
        

        //------------------------------------------ TARGET VALUE SECTION STARTS-------------------------------------------


        targetValueArray = _targetValue;
        PlayerPrefs.SetInt(_name + targetValue0, _targetValue[0]);
        PlayerPrefs.SetInt(_name + targetValue1, _targetValue[1]);
        PlayerPrefs.SetInt(_name + targetValue2, _targetValue[2]);

        //------------------------------------------ REWARDS --------------------------------------------------------------

        rewardArray = _reward;

        //--------------------------------------------- WEIGHT ----------------------------------------------------------

        if (PlayerPrefs.HasKey(_name + weightString))
        {
            weight = PlayerPrefs.GetInt(_name + weightString);
        }
        else
        {
            weight = 0;
        }
    }

    public Achieve(string _name, string[] _rewardType, string[] itemType, int[] _targetValue, int[] _reward)
    {
        threeLevel = true;

        achieveName = _name;
        PlayerPrefs.SetInt(achieveName + multiLevel, 1);
        //------------------------------------------------- NAME ---------------------------------------------------------

        //------------------------- REWARD TYPE --------------------------------------------------------------------------
        rewardType = _rewardType;
        PlayerPrefs.SetString(achieveName + rewardType0, _rewardType[0]);
        PlayerPrefs.SetString(achieveName + rewardType1, _rewardType[1]);
        PlayerPrefs.SetString(achieveName + rewardType2, _rewardType[2]);
        PlayerPrefs.SetInt(achieveName + reward0, _reward[0]);
        PlayerPrefs.SetInt(achieveName + reward1, _reward[1]);
        PlayerPrefs.SetInt(achieveName + reward2, _reward[2]);

        //------------------------------------------ TARGET VALUE SECTION STARTS-------------------------------------------

        PlayerPrefs.SetString(achieveName + itemType0, itemType[0]);
        PlayerPrefs.SetString(achieveName + itemType0, itemType[1]);
        PlayerPrefs.SetString(achieveName + itemType0, itemType[2]);


        targetValueArray = _targetValue;
        PlayerPrefs.SetInt(_name + targetValue0, _targetValue[0]);
        PlayerPrefs.SetInt(_name + targetValue1, _targetValue[1]);
        PlayerPrefs.SetInt(_name + targetValue2, _targetValue[2]);

        //------------------------------------------ REWARDS --------------------------------------------------------------

        rewardArray = _reward;
       
        //--------------------------------------------- WEIGHT ----------------------------------------------------------

        if (PlayerPrefs.HasKey(_name + weightString))
        {
            weight = PlayerPrefs.GetInt(_name + weightString);
        }
        else
        {
            weight = 0;
        }      
    }




    public Achieve(string _name, string[] _rewardType, int _targetValue, int _reward)
    {
        if (PlayerPrefs.GetInt(achieveName) < _targetValue)
        {
            threeLevel = false;
            achieveName = _name;
            PlayerPrefs.SetInt(achieveName + multiLevel, 0);
            rewardType = _rewardType;
            targetValue = _targetValue;
            reward = _reward;
        }
        else
            Destroy(this);
    }

    public void Getter()
    {

    }
}
