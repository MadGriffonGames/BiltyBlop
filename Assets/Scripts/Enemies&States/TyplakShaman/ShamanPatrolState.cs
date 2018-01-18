using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanPatrolState : IShamanState
{
    private Shaman enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Shaman enemy)
    {
        this.enemy = enemy;
        patrolDuration = enemy.patrolDuration;
    }

    public void Execute()
    {
        enemy.LocalMove();
        if (enemy.Target != null && enemy.InShootingRange)
        {
            enemy.ChangeState(new ShamanRangeState());
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
            enemy.Target = null;
            enemy.ChangeDirection();
        }
    }
}
