using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnterState : MonoBehaviour, ISpiderState
{
    private Spider enemy;

    bool reachRight = false;
    bool reachLeft = false;


    public void Enter(Spider enemy)
    {
        this.enemy = enemy;
        enemy.armature.sortingOrder = 0;
        enemy.armature.animation.FadeIn("appear1", -1, 1);
        enemy.armature.animation.timeScale = 1.5f;
        enemy.speed = 1;

    }

    public void Execute()
    {
        if (enemy.armature.animation.isCompleted)
        {
            enemy.ChangeState(new SpiderStonesState());
        }
    }

    public void Exit()
    {
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }
}
