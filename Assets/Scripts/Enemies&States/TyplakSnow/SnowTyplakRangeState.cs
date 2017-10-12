using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTyplakRangeState : ISnowTyplakState
{
    private SnowTyplak enemy;

    public void Enter(SnowTyplak enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.2f;
    }

    public void Execute()
    {

        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new SnowTyplakMeleeState());
        }
        else if (enemy.Target != null)
        {
            enemy.LocalMove();
        }
        else
        {
            enemy.ChangeState(new SnowTyplakPatrolState());
        }
    }

    public void Exit()
    {

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
