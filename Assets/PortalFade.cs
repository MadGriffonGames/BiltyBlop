using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFade : MonoBehaviour
{

    [SerializeField]
    public GameObject fade;

    private static PortalFade instance;
    public static PortalFade Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<PortalFade>();
            return instance;
        }
    }

    
}
