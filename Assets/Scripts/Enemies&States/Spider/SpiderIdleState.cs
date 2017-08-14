using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

internal class SpiderIdleState : ISpiderState
{
    private Spider enemy;
    float idlingTime = 2f;
    float timer;
    bool isIdling = false;

    public void Enter(Spider enemy)
    {
        timer = Time.time;
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (!isIdling)
        {
            enemy.armature.animation.FadeIn("idle", -1, 1);
        }

        if (Time.time - timer > idlingTime)
        {
            enemy.ChangeState(new SpiderUndergroundState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}