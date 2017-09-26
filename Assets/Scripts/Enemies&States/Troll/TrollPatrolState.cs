using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollPatrolState : ITrollState
{
    private Troll enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Troll enemy)
    {
        this.enemy = enemy;
        patrolDuration = enemy.patrolDuration;
    }

    public void Execute()
    {
        enemy.LocalMove();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new TrollRangeState());
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
