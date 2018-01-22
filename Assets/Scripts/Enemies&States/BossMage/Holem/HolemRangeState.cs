using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolemRangeState : MonoBehaviour, IHolemState
{
    private BossHolem enemy;
    bool isPreAttacked;
    bool canExit;

    public void Enter(BossHolem enemy)
    {
        this.enemy = enemy;
        isPreAttacked = false;
        canExit = false;
    }

    public void Execute()
    {
        if (enemy.canAttack)
        {
            if (!isPreAttacked && enemy.InShootingRange)
            {
                canExit = false;
                enemy.isAttacking = true;
                enemy.armature.animation.FadeIn("pre_atk_fireball", -1, 1);
                isPreAttacked = true;
            }
            if (enemy.armature.animation.lastAnimationName == "pre_atk_fireball" && enemy.armature.animation.isCompleted)
            {
                enemy.armature.animation.FadeIn("atk_fireball", -1, 1);
                enemy.ThrowFireball();
            }
            if (enemy.armature.animation.lastAnimationName == "atk_fireball" && enemy.armature.animation.isCompleted)
            {
                enemy.isAttacking = false;
                enemy.canAttack = false;
                enemy.isTimerTick = true;
                isPreAttacked = false;
                canExit = true;
            }
        }
        if (canExit)
        {
            if (enemy.InShootingRange)
            {
                enemy.ChangeState(new HolemIdleState());
            }
            else
            {
                enemy.ChangeState(new HolemPatrolState());
            }
        }
    }

    public void Exit()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            enemy.Target = null;
            enemy.ChangeDirection();
            enemy.Target = null;
        }
    }
}

