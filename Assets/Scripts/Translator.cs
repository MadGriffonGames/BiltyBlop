using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Translator : MonoBehaviour
{
    Text textField;

    private void Start()
    {
#if UNITY_EDITOR
        textField = GetComponent<Text>();

        //LocalizationManager.Instance.UpdateLocaliztion(textField);
#endif
    }

}
