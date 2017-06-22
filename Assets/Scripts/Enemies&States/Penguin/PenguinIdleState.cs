using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PenguinIdleState : IPenguinState
{
    private Penguin enemy;
    bool idled = false;
    float waitFor = 0.5f;
    bool timerStart = false;
    float startTime;

    public void Enter(Penguin enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.2f;
    }

    public void Execute()
    {
        Debug.Log("idled");
        if (!idled)
        {
            if (enemy.armature.animation.lastAnimationName == "attack" && enemy.armature.animation.isCompleted)
            {
                startTime = Time.time;
                enemy.armature.animation.FadeIn("idle", -1, -1);
                timerStart = true;
            }
            idled = true;
        }

        if (timerStart && (Time.time - startTime > waitFor))
        {
            if (idled && enemy.Target != null)
            {
                enemy.ChangeState(new PenguinRangeState());
            }
            else if (idled && enemy.Target == null)
                enemy.ChangeState(new PenguinPatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }

    public IEnumerator Idle()
    {
        enemy.armature.animation.FadeIn("idle", -1, 1);
        yield return new WaitForSeconds(2f);
    }
}

