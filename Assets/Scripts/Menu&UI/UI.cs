using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private static UI instance;

    public static UI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<UI>();
            return instance;
        }
    }

    [SerializeField]
    public GameObject[] gameUI;

    [SerializeField]
    public GameObject LevelEndUI;

    [SerializeField]
    public GameObject DeathUI;

    [SerializeField]
    public GameObject timeRewindUI;

    [SerializeField]
    public GameObject skipVideoButton;

    public GameObject controlsUI;

    private void Start()
    {
        controlsUI = GetComponentInChildren<ControlsUI>().gameObject;     
    }

    public void EnableGameUI(bool enable)
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(enable);
        }
    } 
}
