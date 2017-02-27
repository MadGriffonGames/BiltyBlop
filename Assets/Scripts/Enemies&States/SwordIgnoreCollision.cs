using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIgnoreCollision : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.AttackCollider, true);
    }
}
