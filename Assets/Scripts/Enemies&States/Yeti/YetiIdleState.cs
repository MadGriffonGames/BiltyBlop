using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class YetiIdleState : IYetiState
{
    private Yeti enemy;
    bool isIdle;
    float waitFor = 0.9f;
    bool timerStart = false;
    bool attacked = false;
    float startTime;
    [SerializeField]
    GameObject snowball;



    public void Enter(Yeti enemy)
    {
        this.enemy = enemy;
        isIdle = false;
    }

    public void Execute()
    {
        if (!isIdle)
        {
            if (enemy.armature.animation.lastAnimationName == "attack" && enemy.armature.animation.isCompleted)
            {
                enemy.armature.animation.timeScale = 1;
                attacked = true;
                startTime = Time.time;
                enemy.armature.animation.FadeIn("idle2", -1, -1);
                timerStart = true;
            }

            else if (enemy.armature.animation.lastAnimationName == "idle2")
            {
                enemy.armature.animation.FadeIn("idle2", -1, -1);
            }
            isIdle = true;
        }

        if (!enemy.snowball.isActiveAndEnabled && attacked)
        {

            if (isIdle && enemy.Target != null)
            {
                enemy.ChangeState(new YetiRangeState());
            }
            else if (isIdle && enemy.Target == null)
                enemy.ChangeState(new YetiIdleState());
        }

        if (enemy.Target != null && timerStart == false)
        {
            enemy.ChangeState(new YetiRangeState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}

