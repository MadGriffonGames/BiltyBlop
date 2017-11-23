using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private static LocalizationManager instance;
    public static LocalizationManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<LocalizationManager>();
            return instance;
        }
    }

    string pathToJson;
    string language = "RU";
    string jsonString;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        pathToJson = Application.streamingAssetsPath + "/" + language + ".json";
        jsonString = File.ReadAllText(pathToJson);
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
