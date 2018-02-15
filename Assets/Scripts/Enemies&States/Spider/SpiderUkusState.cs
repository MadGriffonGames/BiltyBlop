using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpiderUkusState : ISpiderState
{
    private Spider enemy;
    bool isUkused = false;
    bool isWeak = false;
    float timer;


    public void Enter(Spider enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (!isUkused)
        {
            enemy.armature.animation.timeScale = 1.3f;
            enemy.armature.animation.FadeIn("ukus", -1, 1);
            isUkused = true;
        }

        if (!isWeak && enemy.armature.animation.lastAnimationName == "ukus" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("ukus_weak", -1, 1);
            isWeak = true;
        }

        if (enemy.armature.animation.lastAnimationName == "ukus_weak" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("ukus2", -1, 1);
            timer = Time.time;
        }

        if (Time.time - timer > 1f && enemy.armature.animation.lastAnimationName == "ukus2" && enemy.armature.animation.isCompleted)
        {
            enemy.ChangeState(new SpiderJumpState());
        }
    }

    public void Exit()
    {
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
    }
}