using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMageIdleState : IceMageState
{

    private IceMage enemy;
    bool isIdle;
    float waitFor = 3.3f;
    bool attacked = false;
    float startTime;
    [SerializeField]
    GameObject fireball;

    public void Enter(IceMage enemy)
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

                attacked = true;
                startTime = Time.time;
                enemy.armature.animation.FadeIn("idle2", -1, -1);
            }

            else if (enemy.armature.animation.lastAnimationName == "idle")
            {
                enemy.armature.animation.FadeIn("idle", -1, -1);
            }
            isIdle = true;
        }

        if (!enemy.fireball.activeInHierarchy && attacked && (Time.time - startTime >= waitFor))
        {
            if (isIdle && enemy.Target != null)
            {
                enemy.ChangeState(new IceMageRangeState());
            }
            //else if (isIdle && enemy.Target == null)
              //  enemy.ChangeState(new IceMageIdleState());
        }

        if (enemy.Target != null && attacked == false)
        {
            enemy.ChangeState(new IceMageRangeState());
        }
    }

    public void Exit() { }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
