using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTyplakMeleeState : MonoBehaviour, ISnowTyplakState
{
    private SnowTyplak enemy;

    private float attackTimer;
    private float attackCoolDown = 1.5f;
    private bool canExit = true;
    bool preattack = false;
    float timer;
    float delay = 0.1f;

    public void Enter(SnowTyplak enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1f;

    }

    public void Execute()
    {
        Attack();
        if (!enemy.InMeleeRange && canExit)
        {
            enemy.ChangeState(new SnowTyplakRangeState());
        }
        else if (enemy.Target == null && canExit)
        {
            enemy.ChangeState(new SnowTyplakPatrolState());
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
            enemy.armature.animation.FadeIn("atk1_pre", -1, 1);
            preattack = true;
        }
        if (enemy.armature.animation.lastAnimationName == "atk1_pre" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("atk1", -1, 1);
            timer = Time.time;
        }

        if (enemy.armature.animation.lastAnimationName == "atk1" && Time.time - timer > delay)
            enemy.AttackCollider.enabled = true;

        if (enemy.armature.animation.lastAnimationName == "atk1" && enemy.armature.animation.isCompleted)
        {
            enemy.AttackCollider.enabled = false;
            enemy.isAttacking = false;
            preattack = false;
            canExit = true;
        }
    }
}
