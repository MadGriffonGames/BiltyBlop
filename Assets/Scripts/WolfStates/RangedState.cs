using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {

        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }
        else
            if (enemy.Target != null)
            {
                enemy.Move();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
    }

    public void Exit()
    {

    }

	public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }
}
