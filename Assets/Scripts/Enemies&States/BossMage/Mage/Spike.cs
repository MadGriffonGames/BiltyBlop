using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public void EndAnim()
    {
        if (transform.parent.gameObject.activeInHierarchy)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
