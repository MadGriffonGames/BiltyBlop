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
    public GameObject LevelEndUI;

    [SerializeField]
    public GameObject DeathUI;
}
