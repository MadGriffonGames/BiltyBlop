using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public struct LocalizedString
    {
        public string text;
        public int fontSize;
    }

    private static LocalizationManager instance;
    public static LocalizationManager Instance
    {
        get
        {
            return instance;
        }
    }
    string pathToJson;
    string language;
    string jsonString;
    public Dictionary<string, LocalizedString> translation;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }

        instance = GetComponent<LocalizationManager>();

        DontDestroyOnLoad(this);

        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            language = "RU";
        }
        else
        {
            language = "EN";
        }

        if (language != null)
        {
            translation = new Dictionary<string, LocalizedString>();
#if UNITY_EDITOR
            pathToJson = Application.dataPath + "/StreamingAssets" + "/" + language + ".json";
			jsonString = File.ReadAllText(pathToJson);
#elif UNITY_IOS
            pathToJson = Application.dataPath + "/Raw" + "/" + language + ".json";
            jsonString = File.ReadAllText(pathToJson);
#elif UNITY_ANDROID
            pathToJson = "jar:file://" + Application.dataPath + "!/assets/" + language + ".json";
            WWW www = new WWW(pathToJson);
            while (!www.isDone) { }
            jsonString = www.text;
#endif
            translation = JsonConvert.DeserializeObject<Dictionary<string, LocalizedString>>(jsonString);
			Debug.Log (translation);

        }
    }

    public void UpdateLocaliztion(Text textField)
    {
        if (textField != null)
        {
            if (translation.ContainsKey(textField.text))
            {
                if (translation[textField.text].fontSize > 0)
                {
					
                    textField.fontSize = translation[textField.text].fontSize;
                }
				textField.text = translation [textField.text].text;
            }
            else
            {
                Debug.Log("Can't find key: " + textField.text);
            }
        }
    }
}
