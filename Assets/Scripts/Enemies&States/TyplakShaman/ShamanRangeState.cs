using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanRangeState : IShamanState
{
    private Shaman enemy;
    private float attackTimer;
    private float attackCoolDown = 1.5f;
    private bool canExit = true;
    bool preattack = false;
    float timer;
    float delay = 0.1f;
    

    public void Enter(Shaman enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.2f;
        timer = 0;
    }

    public void Execute()
    {
        if (!enemy.isAttacking)
        {
            Attack();
        }
        if (enemy.isAttacking)
        {
            if (!enemy.isIdle)
            {
                enemy.armature.animation.FadeIn("Idle", -1, -1);
                enemy.isIdle = true;
            }
        }
        if (enemy.Target == null)
        {
            if (canExit)
            {
                enemy.ChangeState(new ShamanPatrolState());
            }
        }
    }

    private void Attack()
    {
        if (!preattack)
        {
            canExit = false;           
            enemy.armature.animation.FadeIn("shaman_atk_pre", -1, 1);
            preattack = true;
        }
        if (enemy.armature.animation.lastAnimationName == "shaman_atk_pre" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("shaman_atk", -1, 1);
            enemy.ThrowFireball();
        }

        if (enemy.armature.animation.lastAnimationName == "shaman_atk" && enemy.armature.animation.isCompleted)
        {
            preattack = false;
            canExit = true;
            enemy.isAttacking = true;
            enemy.isTimerTick = true;
        }
    }

    public void Exit()
    {
        enemy.isTimerTick = false;
        enemy.isAttacking = false;
        enemy.isIdle = false;
        enemy.timer = 0;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            Debug.Log(1);
            enemy.Target = null;
            enemy.ChangeDirection();
        }
    }
}
