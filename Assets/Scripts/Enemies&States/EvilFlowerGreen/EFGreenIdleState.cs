using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFGreenIdleState : IEFGreenState
{

    private EvilFlowerGreen enemy;
    bool isIdle;

    public void Enter(EvilFlowerGreen enemy)
    {
        this.enemy = enemy;
        isIdle = false;
    }

    public void Execute()
    {
        if (!isIdle)
        {
            enemy.armature.animation.timeScale = 1;
            enemy.armature.animation.FadeIn("IDLE", -1, -1);
            isIdle = true;
        }

        if (enemy.Target != null)
        {
            enemy.ChangeState(new EFGreenRangeState());
        }
    }

    public void Exit() { }

    public void OnTriggerEnter2D(Collider2D other) { }

}

