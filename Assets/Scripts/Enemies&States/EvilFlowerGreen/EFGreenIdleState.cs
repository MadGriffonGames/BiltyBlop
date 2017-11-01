using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFGreenIdleState : IEFGreenState
{

    private EvilFlowerGreen enemy;
    bool isIdle;
    float timer;

    public void Enter(EvilFlowerGreen enemy)
    {
        this.enemy = enemy;
        isIdle = false;
        timer = 0;
    }

    public void Execute()
    {
        timer += Time.deltaTime;

        if (!isIdle)
        {
            enemy.armature.animation.timeScale = 1;
            enemy.armature.animation.FadeIn("idle", -1, -1);
            isIdle = true;
        }

        if (enemy.Target != null && timer >= 2)
        {
            timer = 0;
            enemy.ChangeState(new EFGreenRangeState());
        }
    }

    public void Exit() { }

    public void OnTriggerEnter2D(Collider2D other) { }

}

