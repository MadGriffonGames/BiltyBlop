using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
	void Start ()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.gameObject.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.gameObject.GetComponent<CapsuleCollider2D>(), true);
    }

}
