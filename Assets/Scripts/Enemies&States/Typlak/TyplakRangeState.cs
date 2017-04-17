using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyplakRangeState : ITyplakState
{

    private Typlak enemy;

    public void Enter(Typlak enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {

        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new TyplakMeleeState());
        }
        else if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new TyplakPatrolState());
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
