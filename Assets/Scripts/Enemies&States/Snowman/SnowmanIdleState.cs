using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanIdleState : ISnowmanState
{
    private Snowman enemy;
    bool isIdle;
    bool isTimeChecking;
    bool canExit;
    float timer;

    public void Enter(Snowman enemy)
    {
        this.enemy = enemy;

        enemy.armature.animation.timeScale = 1;

        isIdle = false;
        isTimeChecking = false;
        canExit = true;

        timer = 0;
    }

    public void Execute()
    {
        if (!isIdle)
        {
            enemy.armature.animation.FadeIn("pre_weak", -1, 1);
            isIdle = true;
            canExit = false;
        }
        if (enemy.armature.animation.lastAnimationName == "pre_weak" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("weak", -1, -1);
            isTimeChecking = true;
        }
        if (isTimeChecking)
        {
            timer += Time.deltaTime;
        }
        if (timer >= enemy.idleDuration)
        {
            canExit = true;
        }
        if (canExit)
        {
            enemy.ChangeDirection();
            enemy.ChangeState(new SnowmanPatrolState());
        }
    }

    public void Exit()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }
}
