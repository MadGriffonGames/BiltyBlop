using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MetricaManager : MonoBehaviour
{
    private static MetricaManager instance;
    public static MetricaManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<MetricaManager>();
            return instance;
        }
    }

    public string lastUnlockedLevel;
    public string currentLevel;

    public int deaths;

    public bool isTimerActive;
    public float levelCompleteTime;

	void Start ()
    {
        isTimerActive = true;

        deaths = 0;
        levelCompleteTime = 0;

        currentLevel = SceneManager.GetActiveScene().name;
	}

	void Update ()
    {
        if (isTimerActive)
        {
            levelCompleteTime += Time.deltaTime;
        }
	}
}
