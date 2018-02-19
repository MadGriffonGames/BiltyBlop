using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFireballAttackState : IMageBossState
{
    private MageBoss enemy;

    bool isAttacked;
    bool isPreattacked;

    public void Enter(MageBoss enemy)
    {
        this.enemy = enemy;
        isAttacked = false;
        isPreattacked = false;
        enemy.lastAttackState = enemy.currentState;
    }

    public void Execute()
    {
        if (!isPreattacked)
        {
            isPreattacked = true;
            enemy.armature.armature.animation.FadeIn("Pre_attack_shoot", -1, 1);
        }
        if (!isAttacked && enemy.armature.animation.lastAnimationName == "Pre_attack_shoot" && enemy.armature.animation.isCompleted)
        {
            isAttacked = true;
            enemy.armature.armature.animation.FadeIn("Atk_shoot", -1, 1);
            enemy.ThrowFireballs();
        }
        if (enemy.armature.animation.lastAnimationName == "Atk_shoot" && enemy.armature.animation.isCompleted)
        {
            enemy.ChangeState(new MageIdleState());
        }
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
