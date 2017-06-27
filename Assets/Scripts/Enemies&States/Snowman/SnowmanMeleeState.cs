using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanMeleeState : ISnowmanState
{
    private Snowman enemy;

    private bool canExit = true;
    bool preattack = false;

    public void Enter(Snowman enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Attack();
        if (enemy.Target == null && canExit)
        {
            enemy.ChangeState(new SnowmanPatrolState());
        }
        else if (!enemy.InMeleeRange && canExit)
        {
            enemy.ChangeState(new SnowmanRangeState());
        }
    }

    public void Exit()
    {
        enemy.walk = false;
        enemy.isAttacking = false;
        enemy.AttackCollider.enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D other)
    { }

    private void Attack()
    {
        if (!preattack)
        {
            canExit = false;
            enemy.isAttacking = true;
            enemy.armature.animation.FadeIn("pre_attack", -1, 1);
            preattack = true;
        }
        if (enemy.armature.animation.lastAnimationName == "pre_attack" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("attack", -1, 1);
            enemy.AttackCollider.enabled = true;
        }
        if (enemy.armature.animation.lastAnimationName == "attack" && enemy.armature.animation.isCompleted)
        {
            enemy.AttackCollider.enabled = false;
            enemy.isAttacking = false;
            preattack = false;
            canExit = true;
        }
    }
}
