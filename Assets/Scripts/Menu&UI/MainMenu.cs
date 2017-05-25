using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject actCanvas;
    
    public string sceneName { get; set; }

    public void Start()
    {
        SoundManager.PlayMusic("main menu", true);
    }

    public void ToActSelect(string sceneName)
    {
        actCanvas.SetActive(true);
    }

    
}
