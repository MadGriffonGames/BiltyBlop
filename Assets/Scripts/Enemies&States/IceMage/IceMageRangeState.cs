using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IceMageRangeState : IceMageState
{
    private IceMage enemy;
    bool isAttacking = false;

    public void Enter(IceMage enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 2f;
        enemy.armature.animation.FadeIn("attack", -1, 1);
    }

    public void Execute()
    {
        if (enemy.isTakingDamage == true)
        {
            enemy.ChangeState(new IceMageDeathState());
        }
        if ((enemy.armature.animation.lastAnimationName == ("attack") || enemy.armature.animation.lastAnimationName == "idle") && enemy.armature.animation.isCompleted)
        {
            SoundManager.PlaySound("spit");
            enemy.armature.animation.timeScale = 1.5f;
            enemy.fireball.SetActive(true);
            enemy.ChangeState(new IceMageIdleState());
        }

        if (enemy.isDead)
            enemy.fireball.SetActive(false);
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}