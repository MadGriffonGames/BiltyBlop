using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyplakMeleeState : ITyplakState
{
    private Typlak enemy;

    private float attackTimer;
    private float attackCoolDown = 1.5f;
    private bool canExit = true;
    bool preattack = false;

    public void Enter(Typlak enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1f;
        
    }

    public void Execute()
    {
        Attack();
        if (!enemy.InMeleeRange && canExit)
        {
            enemy.ChangeState(new TyplakRangeState());
        }
        else if (enemy.Target == null && canExit)
        {
            enemy.ChangeState(new TyplakPatrolState());
        }
    }

    public void Exit()
    {
        enemy.walk = false;
        enemy.isAttacking = false;
        enemy.AttackCollider.enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {    }

    private void Attack()
    {
        if (!preattack)
        {
            canExit = false;
            enemy.isAttacking = true;
            enemy.armature.animation.FadeIn("preattack", -1, 1);
            preattack = true;
        }
        if (enemy.armature.animation.lastAnimationName == "preattack" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("Attack", -1, 1);
            enemy.AttackCollider.enabled = true;
        }
        if (enemy.armature.animation.lastAnimationName == "Attack" && enemy.armature.animation.isCompleted)
        {
            enemy.AttackCollider.enabled = false;
            enemy.isAttacking = false;
            preattack = false;
            canExit = true;
        }
    }
}
