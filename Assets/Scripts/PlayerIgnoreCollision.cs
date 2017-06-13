using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIgnoreCollision : MonoBehaviour
{
    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }
}
