using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanPatrolState : ISnowmanState
{
    private Snowman enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Snowman enemy)
    {
        this.enemy = enemy;
        patrolDuration = enemy.patrolDuration;
        enemy.movementSpeed = 5;
    }

    public void Execute()
    {
        enemy.LocalMove();
        if (enemy.Target != null)
        {
            if (enemy.InMeleeRange)
            {
                enemy.ChangeState(new SnowmanMeleeState());
            }
            else
            {
                enemy.ChangeState(new SnowmanRangeState());
            }
        }
    }

    public void Exit()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            enemy.ChangeDirection();
        }
    }
}
