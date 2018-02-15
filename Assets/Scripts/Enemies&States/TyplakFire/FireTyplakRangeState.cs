using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTyplakRangeState : IFireTyplakState
{
    private FireTyplak enemy;

    public void Enter(FireTyplak enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.2f;
    }

    public void Execute()
    {

        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new FireTyplakMeleeState());
        }
        else if (enemy.Target != null)
        {
            enemy.LocalMove();
        }
        else
        {
            enemy.ChangeState(new FireTyplakPatrolState());
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
