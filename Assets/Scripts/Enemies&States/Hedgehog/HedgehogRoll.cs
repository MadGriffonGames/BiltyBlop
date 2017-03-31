using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogRoll : MonoBehaviour
{
    public void StartIgnore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, true);
    }

    public void StopIgnore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, false);
    }
}
