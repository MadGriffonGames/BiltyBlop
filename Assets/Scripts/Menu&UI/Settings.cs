using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Settings : MonoBehaviour
{

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
