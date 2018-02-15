using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    GameObject bolt;
    [SerializeField]
    GameObject puf;

    public void DisableBolt()
    {
        if (bolt)
        {
            bolt.SetActive(false);
        }
    }

    public void ActivatePuf()
    {
        if (puf)
        {
            puf.SetActive(true);
        }
    }
}
