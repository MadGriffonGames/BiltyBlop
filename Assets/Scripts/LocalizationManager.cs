using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SimpleJSON;
using UnityEngine.UI;
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
    string language = "EN";
    string jsonString;
    public Dictionary<string, KeyValuePair<string, int>> translation;

    private void Awake()
    {
        //if (language != null)
        //{
        //    DontDestroyOnLoad(this);

        //    translation = new Dictionary<string, KeyValuePair<string, int>>();
        //    pathToJson = Application.streamingAssetsPath + "/" + language + ".json";
        //    jsonString = File.ReadAllText(pathToJson);

        //    translation = JsonConvert.DeserializeObject<Dictionary<string, KeyValuePair<string, int>>>(jsonString);
        //}
    }

    public void UpdateLocaliztion(Text textField)
    {
        //if (textField != null)
        //{
        //    if (translation.ContainsKey(textField.text))
        //    {
        //        string translatedText = translation[textField.text].Key;
        //        Debug.Log(translation[textField.text].GetType());
        //        Debug.Log(translation[textField.text]);
        //        Debug.Log(translatedText);
        //        textField.text = translatedText;
        //        if (true)
        //        {

        //        }
        //    }
        //}
    }
}
