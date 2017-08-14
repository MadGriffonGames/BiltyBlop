using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class SpiderJumpState : ISpiderState
{
    private Spider enemy;
    bool isJumped = false;

    public void Enter(Spider enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (!isJumped)
        {
            enemy.armature.animation.FadeIn("fast_jump", 1, 1);
            isJumped = true;
        }

        if ((enemy.armature.animation.lastAnimationName == "fast_jump") && enemy.armature.animation.isCompleted)
        {
            enemy.ChangeState(new SpiderGroundAttackState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}

