using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField]
    Sprite on;
    [SerializeField]
    Sprite off;
    Image currentImg;

    private void Start()
    {
        currentImg = GetComponent<Image>();
        if (PlayerPrefs.GetInt("SoundsIsOn") == 1)
        {
            currentImg.sprite = on;
        }
        else
        {
            currentImg.sprite = off;
        }
    }

    public void SoundsButton()
    {
        if (PlayerPrefs.GetInt("SoundsIsOn") == 1)
        {
            PlayerPrefs.SetInt("SoundsIsOn", 0);
            SoundManager.MuteSound(true);
            currentImg.sprite = off;
        }
        else
        {
            PlayerPrefs.SetInt("SoundsIsOn", 1);
            SoundManager.MuteSound(false);
            currentImg.sprite = on;
        }
    }
}
