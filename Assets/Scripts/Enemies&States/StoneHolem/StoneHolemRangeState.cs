using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHolemRangeState : IStoneHolemState
{

    private StoneHolem enemy;

    public void Enter(StoneHolem enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {

        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new StoneHolemMeleeState());
        }
        else if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new StoneHolemIdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            enemy.Target = null;
            enemy.ChangeDirection();
        }
    }
}
