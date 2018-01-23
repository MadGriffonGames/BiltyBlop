using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFireballAttackState : IMageBossState
{
    private MageBoss enemy;

    bool isAttacked;

    public void Enter(MageBoss enemy)
    {
        this.enemy = enemy;
        isAttacked = false;
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
            enemy.SpawnFireballs(false);
        }
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
