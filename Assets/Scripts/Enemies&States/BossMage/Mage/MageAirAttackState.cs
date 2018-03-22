using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAirAttackState : IMageBossState
{
    private MageBoss enemy;

    bool isAttacked;

    public void Enter(MageBoss enemy)
    {
        this.enemy = enemy;
        isAttacked = false;
        enemy.lastAttackState = enemy.currentState;
    }

    public void Execute()
    {
        if (!isAttacked)
        {
            isAttacked = true;
            enemy.armature.armature.animation.FadeIn("Casting_spell_bang", -1, 1);
        }
        if (enemy.armature.animation.lastAnimationName == "Casting_spell_bang" && enemy.armature.animation.isCompleted)
        {
            enemy.SpawnFireballs();
            enemy.ChangeState(new MageIdleState());
        }
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
