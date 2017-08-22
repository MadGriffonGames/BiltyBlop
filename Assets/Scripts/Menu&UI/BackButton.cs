using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void ButtonBack()
    {
        GameManager.nextLevelName = "MainMenu";
        SceneManager.LoadScene("Loading");
    }
}
