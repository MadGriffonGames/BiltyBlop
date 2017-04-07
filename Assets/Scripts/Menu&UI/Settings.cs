using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField]
    GameObject musicButton;
    [SerializeField]
    GameObject fxButton;
    
    bool hide = true;

    private void Start()
    {
        musicButton.gameObject.SetActive(false);
        fxButton.gameObject.SetActive(false);
        if (!PlayerPrefs.HasKey("SoundsIsOn"))
        {
            PlayerPrefs.SetInt("SoundsIsOn", 1);
        }
        if (!PlayerPrefs.HasKey("MusicIsOn"))
        {
            PlayerPrefs.SetInt("MusicIsOn", 1);
        }
    }

    public void ShowButtons()
    {
        hide = !hide;
        if (!hide)
        {
            musicButton.gameObject.SetActive(true);
            fxButton.gameObject.SetActive(true);
        }
        else
        {
            musicButton.gameObject.SetActive(false);
            fxButton.gameObject.SetActive(false);
        }
    }
}
