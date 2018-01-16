using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkeletonMeleeState : IShieldSkeletonState
{
    private ShieldSkeleton enemy;

    private bool canExit;
    bool isAttacked;

    public void Enter(ShieldSkeleton enemy)
    {
        canExit = true;
        isAttacked = false;
        this.enemy = enemy;

        enemy.armature.animation.timeScale = 1f;
    }

    public void Execute()
    {
        Attack();
        if (enemy.Target == null && canExit)
        {
            enemy.ChangeState(new ShieldSkeletonPatrolState());
        }
    }

    public void Exit()
    {
        enemy.armature.animation.timeScale = 1f;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            enemy.ChangeDirection();
        }
    }

    private void Attack()
    {
        enemy.isTimerTick = true;

        if (!isAttacked)
        {
            enemy.armature.animation.FadeIn("hit", -1, 1);

            isAttacked = true;
            canExit = false;
        }

        if (enemy.armature.animation.lastAnimationName == "hit" && enemy.armature.animation.isCompleted)
        {
            canExit = true;

            enemy.ChangeState(new ShieldSkeletonPatrolState());
        }
    }
}
