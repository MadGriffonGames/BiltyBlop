using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    Sprite on;
    [SerializeField]
    Sprite off;
    Image currentImg;

    private void Start()
    {
        currentImg = GetComponent<Image>();
        if (PlayerPrefs.GetInt("MusicIsOn") == 1)
        {
            currentImg.sprite = on;
        }
        else if (PlayerPrefs.GetInt("MusicIsOn") == 0)
        {
            currentImg.sprite = off;
        }
    }

    public void MusicButton()
    {
        if (PlayerPrefs.GetInt("MusicIsOn") == 1)
        {
            PlayerPrefs.SetInt("MusicIsOn", 0);
            SoundManager.MuteMusic(true);
            currentImg.sprite = off;
        }
        else if(PlayerPrefs.GetInt("MusicIsOn") == 0)
        {
            PlayerPrefs.SetInt("MusicIsOn", 1);
            SoundManager.MuteMusic(false);
            currentImg.sprite = on;
        }
    }
}
