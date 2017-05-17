using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonShadow : MonoBehaviour
{
    [SerializeField]
    GameObject dragon;
	
	void Update ()
    {
        transform.position = new Vector3(dragon.transform.position.x + 2, transform.position.y, -2);
	}
}
