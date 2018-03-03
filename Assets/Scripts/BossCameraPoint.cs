using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraPoint : MonoBehaviour
{
    [SerializeField]
    Vector3 ipadPoint;

    void Start ()
    {
        if (GameManager.screenAspect == GameManager.ASPECT_4x3)
        {
            transform.localPosition = ipadPoint;
        }
	}
}
