using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkeletonPatrolState : IShieldSkeletonState
{
    private ShieldSkeleton enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(ShieldSkeleton enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        enemy.LocalMove();

        if (enemy.Target != null)
        {
            if (enemy.InMeleeRange && !enemy.isTimerTick)
            {
                enemy.ChangeState(new ShieldSkeletonMeleeState());
            }
        }
    }

    public void Exit()
    {
        enemy.walk = false;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            enemy.ChangeDirection();
        }
    }
}
