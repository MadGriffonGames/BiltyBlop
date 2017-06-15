using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DragonBones;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private static SkinManager instance;
    public static SkinManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<SkinManager>();
            return instance;
        }
    }

    [SerializeField]
    public GameObject[] skinPrefabs;
}
