using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPoints : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] spArray;
    int[] rndArray;
	
	void Start ()
    {
        rndArray = new int[10];
        spArray = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
	}
	
	void Update ()
    {
        

        foreach (var sprite in spArray)
        {

        }
	}
}
