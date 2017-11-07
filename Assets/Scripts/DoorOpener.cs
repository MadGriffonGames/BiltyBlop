using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    GameObject targetGameObject;

	void Update ()
    {
        if (!targetGameObject.activeInHierarchy)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
	}
}
