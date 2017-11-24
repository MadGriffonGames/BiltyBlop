using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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
    string language;
    string jsonString;
    public Dictionary<string, string> translation;

    private void Awake()
    {
        if (language != null)
        {
            DontDestroyOnLoad(this);

            translation = new Dictionary<string, string>();
            pathToJson = Application.streamingAssetsPath + "/" + language + ".json";
            jsonString = File.ReadAllText(pathToJson);

            translation = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        }
        
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
