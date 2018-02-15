using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Translator : MonoBehaviour
{
    Text textField;

    private void Start()
    {
        textField = GetComponent<Text>();
        LocalizationManager.Instance.UpdateLocaliztion(textField);
    }

}
