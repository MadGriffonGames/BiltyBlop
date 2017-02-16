using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFGreenIdleState : IEFGreenState
{

    private EvilFlowerGreen enemy;

    public void Enter(EvilFlowerGreen enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (enemy.Target != null)
        {
            enemy.ChangeState(new EFGreenRangeState());
        }
    }

    public void Exit() { }

    public void OnTriggerEnter2D(Collider2D other) { }

}

