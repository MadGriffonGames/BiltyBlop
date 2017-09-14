using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreMeleeState : IOgreState
{
    private Ogre enemy;

    private float attackTimer;
    private float attackCoolDown = 1.5f;
    private bool canExit = true;
    bool preattack = false;
    float timer;
    float delay = 0.1f;

    public void Enter(Ogre enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;

    }

    public void Execute()
    {
        Attack();
        if (enemy.Target == null && canExit)
        {
            enemy.ChangeState(new OgrePatrolState());
        }
    }

    public void Exit()
    {
        enemy.walk = false;
        enemy.isAttacking = false;
        enemy.AttackCollider.enabled = false;
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
        if (!preattack)
        {
            canExit = false;
            enemy.isAttacking = true;
            enemy.armature.animation.FadeIn("pre_atk", -1, 1);
            preattack = true;
        }
        if (enemy.armature.animation.lastAnimationName == "pre_atk" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("atk", -1, 1);
            enemy.AttackCollider.enabled = true;
        }            

        if (enemy.armature.animation.lastAnimationName == "atk" && enemy.armature.animation.isCompleted)
        {
            enemy.AttackCollider.enabled = false;
            enemy.isAttacking = false;
            preattack = false;
            canExit = true;
        }
    }
}
