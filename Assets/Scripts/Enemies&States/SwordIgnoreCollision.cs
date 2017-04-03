using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIgnoreCollision : MonoBehaviour
{
    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, true);
    }
}
