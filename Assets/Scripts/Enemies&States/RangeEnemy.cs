using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{

    [SerializeField]
    private float shootingRange;

    public override void Start()
    {
        base.Start();
    }

    public bool InShootingRange
    {
        get
        {
            if (Target != null)//if enemy has a target
            {
                //return distance between enemy and target <= meleeRange (true or false)
                return Vector2.Distance(transform.position, Target.transform.position) <= shootingRange;
            }
            return false;
        }
    }
}
