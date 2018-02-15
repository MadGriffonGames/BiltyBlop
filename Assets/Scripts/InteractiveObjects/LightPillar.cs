using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : InteractiveObject
{

    [SerializeField]
    GameObject fire1;

    [SerializeField]
    GameObject fire2;

    public void FireOn()
    {
        
        fire1.gameObject.SetActive(true);
        fire2.gameObject.SetActive(true);
    }

}
