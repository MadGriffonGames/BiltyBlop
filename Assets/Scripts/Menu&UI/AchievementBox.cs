using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBox : MonoBehaviour {


    [SerializeField]
    public string achievementName;
    [SerializeField]
    public string achievementPrefsName;
    [SerializeField]
    GameObject getBtn;
    [SerializeField]
    GameObject text;


	// Use this for initialization
	void Start () {
        text.GetComponent<Text>().text +=  " " + PlayerPrefs.GetInt(achievementName) + "/" + PlayerPrefs.GetInt(achievementPrefsName + "targetValue");
        IsAvailable();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IsAvailable()
    {
        if (PlayerPrefs.GetInt(achievementPrefsName + "unlocked") == 1)
        {
            getBtn.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            getBtn.gameObject.GetComponent<Button>().enabled = true;
        }

    }
}
