using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CollectsUI : MonoBehaviour
{
    private static CollectsUI instance;

    public static CollectsUI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<CollectsUI>();
            return instance;
        }
    }

    [SerializeField]
    GameObject[] stars;

    private void Start()
    {
        
    }

    public void ShowStars(int value)
    {
        Debug.Log("I am here");
        Debug.Log(value);
        stars[value-1].SetActive(true);
    }
}
