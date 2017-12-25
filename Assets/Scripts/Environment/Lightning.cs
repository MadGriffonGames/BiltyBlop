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
        bolt.SetActive(false);
    }

    public void ActivatePuf()
    {
        puf.SetActive(true);
    }
}
