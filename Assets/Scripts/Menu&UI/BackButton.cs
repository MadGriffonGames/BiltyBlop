using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void ButtonBack()
    {
        if (PlayerPrefs.GetInt("FromMap") > 0)
        {
            GameManager.nextLevelName = "Map";
        }
        else
        {
            GameManager.nextLevelName = "MainMenu";
        }
        SceneManager.LoadScene("Loading");
    }
}
