using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFGreenRangeState : IEFGreenState {

    private EvilFlowerGreen enemy;

    public void Enter(EvilFlowerGreen enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (enemy.Target != null)
        {
            if (enemy.InShootingRange)
                enemy.MyAniamtor.SetTrigger("attack");
        }
        else
        {
            enemy.ChangeState(new EFGreenIdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}
