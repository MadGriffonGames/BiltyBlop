using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class YetiRangeState : IYetiState
{

    private Yeti enemy;
    float attackTime;
    bool isAttacking = false;


    public void Enter(Yeti enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 2f;
        enemy.armature.animation.FadeIn("pre_attack", -1, 1);
    }

    public void Execute()
    {
        if ((enemy.armature.animation.lastAnimationName == ("pre_attack") || enemy.armature.animation.lastAnimationName == "idle2") && enemy.armature.animation.isCompleted)
        {
            SoundManager.PlaySound("spit");
            enemy.armature.animation.timeScale = 1.5f;
            enemy.armature.animation.FadeIn("attack", -1, 1);
            enemy.ThrowSnowball();
        }
        if ((enemy.armature.animation.lastAnimationName == ("attack")) && enemy.armature.animation.isCompleted)
            enemy.ChangeState(new YetiIdleState());
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}

