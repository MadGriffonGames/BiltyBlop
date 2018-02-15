using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIgnoreCollision : MonoBehaviour
{
    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, true);
        for (int i = 0; i < Player.Instance.clipSize; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.throwingClip[i].GetComponent<Collider2D>(), true);
        }
    }

    private void OnEnable()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.AttackCollider, true);
        for (int i = 0; i < Player.Instance.clipSize; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.throwingClip[i].GetComponent<Collider2D>(), true);
        }
    }
}
