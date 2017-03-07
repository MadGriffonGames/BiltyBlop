using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    [SerializeField]
    private EdgeCollider2D attackCollider;

    [SerializeField]
    private float meleeRange;

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)//if enemy has a target
            {
                //return distance between enemy and target <= meleeRange (true or false)
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

    public EdgeCollider2D AttackCollider
    {
        get
        {
            return attackCollider;
        }
    }

    public void MeleeAttack()
    {
        AttackCollider.enabled = true;
    }
}
