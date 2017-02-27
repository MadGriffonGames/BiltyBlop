using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogRoll : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void StartIgnore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.AttackCollider, true);
    }

    public void StopIgnore()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.AttackCollider, false);
    }
}
