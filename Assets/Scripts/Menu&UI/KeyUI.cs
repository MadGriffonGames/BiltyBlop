using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    private static KeyUI instance;

    public static KeyUI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<KeyUI>();
            return instance;
        }
    }

    [SerializeField]
    public Image KeyImage;

    void Start()
    {
        KeyImage.enabled = false;
    }

}
