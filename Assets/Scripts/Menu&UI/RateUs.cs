using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUs : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("Rated") > 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void RateUsFunc()
    {
        if (Application.systemLanguage.ToString() == "Russian" || Application.systemLanguage.ToString() == "Ukrainian" || Application.systemLanguage.ToString() == "Belarusian")
        {
            Application.OpenURL("https://docs.google.com/forms/d/1RzwBi5aEDaxPxkkPDz91RwuDNApOxV_VFm2UDZDob4s");
        }
        else
        {
            Application.OpenURL("https://docs.google.com/forms/d/1sB0ASWy2K15KBX2QXThl9lED36WnT71OHxl8vWQFL9k");
        }
        PlayerPrefs.SetInt("Rated", 1);
    }
}
