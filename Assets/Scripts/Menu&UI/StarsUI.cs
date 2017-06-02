using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StarsUI : MonoBehaviour
{
    private static StarsUI instance;
    public static StarsUI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<StarsUI>();
            return instance;
        }
    }

    [SerializeField]
    GameObject[] stars;

    public void ShowStar(int value)
    {
        if (value > 3)
        {
            value = 3;
        }
        stars[value-1].SetActive(true);        
    }
}
