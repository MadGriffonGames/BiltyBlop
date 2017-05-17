using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneColumn : MonoBehaviour
{
    [SerializeField]
    Transform stopPoint;

	void FixedUpdate ()
    {
        if (transform.position.y <= stopPoint.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 4, -1), 0.1f);
        }
	}
}
