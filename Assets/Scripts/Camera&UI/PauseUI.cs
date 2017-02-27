using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void ToLevelMenu()
    {
        SceneManager.LoadScene("LevelSelectMenu");
    }
}
