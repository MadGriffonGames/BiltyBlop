using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogPatrolState : IHedgehogState
{
    private Hedgehog enemy;

    public void Enter(Hedgehog enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        enemy.MyAniamtor.SetFloat("speed", 1);
        enemy.Move();
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
            enemy.MyAniamtor.SetFloat("speed", 0);
            enemy.ChangeState(new HedgehogIdleState());
        }
    }
}
