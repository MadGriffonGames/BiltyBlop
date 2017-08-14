using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpiderUndergroundState : ISpiderState
{
    private Spider enemy;

    public void Enter(Spider enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;
        enemy.armature.animation.FadeIn("drop_down", 1, 1);
    }

    public void Execute()
    {
        if (enemy.armature.animation.lastAnimationName == "drop_down" && enemy.armature.animation.isCompleted)
            enemy.ChangeState(new SpiderRollState());
    }

    public void Exit()
    {
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }
}
