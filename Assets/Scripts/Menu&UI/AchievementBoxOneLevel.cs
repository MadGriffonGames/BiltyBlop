using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBoxOneLevel : MonoBehaviour
{


    [SerializeField]
    public string achievementName;

    [SerializeField]
    GameObject getBtn;


    [SerializeField]
    GameObject text;

    [SerializeField]
    GameObject inProgress;
    [SerializeField]
    GameObject doneImg;


    [SerializeField]
    GameObject gold;

    [SerializeField]
    RectTransform status;


    int gotReward;
    const string btn = "btn";
    const string medal = "medal";

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.SetInt(achievementName + medal, 0);
        //PlayerPrefs.SetInt(achievementName + btn, 0);


        UpdateStatus(PlayerPrefs.GetInt(achievementName), PlayerPrefs.GetInt(achievementName + "targetValue"));
        if (!PlayerPrefs.HasKey(achievementName + btn))
            PlayerPrefs.SetInt(achievementName + btn, 0);
        if (PlayerPrefs.GetInt(achievementName + btn) == 0)
        {
            text.GetComponent<Text>().text += " " + PlayerPrefs.GetInt(achievementName) + "/" + PlayerPrefs.GetInt(achievementName + "targetValue");
        }
        if (PlayerPrefs.GetInt(achievementName + btn) == 1)
        {
            doneImg.gameObject.SetActive(true);
        }


        GetInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void GetInfo()
    {
            int weight = PlayerPrefs.GetInt(achievementName + "weight");
            int currentValue = PlayerPrefs.GetInt(achievementName);
            int targetValue = PlayerPrefs.GetInt(achievementName + "targetValue");

        UpdateStatus(PlayerPrefs.GetInt(achievementName), PlayerPrefs.GetInt(achievementName + "targetValue"));

        if (PlayerPrefs.GetInt(achievementName + medal) == 1)
            {
                gold.SetActive(true);
            }


        if (currentValue < targetValue)
        {
            inProgress.gameObject.SetActive(true);
        }


        if (currentValue >= targetValue && PlayerPrefs.GetInt(achievementName + btn) == 0)
            {
                inProgress.gameObject.SetActive(false);
                getBtn.gameObject.SetActive(true);
                getBtn.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                getBtn.gameObject.GetComponent<Button>().enabled = true;
            }
        }


    public void GetReward()
    {
        int currentValue = PlayerPrefs.GetInt(achievementName);
        int targetValue = PlayerPrefs.GetInt(achievementName + "targetValue");
        UpdateStatus(currentValue, targetValue);
        PlayerPrefs.SetInt(achievementName + btn, 1);
        getBtn.gameObject.SetActive(false);
        inProgress.SetActive(false);
        PlayerPrefs.SetInt(achievementName + medal, 1);
        gold.gameObject.SetActive(true);
        doneImg.gameObject.SetActive(true);
    }

    void UpdateStatus(int currentValue, int currenTargetValue)
    {
        float newX;

        if (currentValue >= currenTargetValue)
            newX = 1;
        else
        {
            newX = (float)currentValue / (float)currenTargetValue;
        }
        status.localScale = new Vector3(newX, status.localScale.y, status.localScale.z);
    }
}