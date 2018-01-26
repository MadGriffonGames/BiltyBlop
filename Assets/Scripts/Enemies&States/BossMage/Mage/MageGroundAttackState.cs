using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageGroundAttackState : IMageBossState
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
            enemy.armature.armature.animation.FadeIn("ground_attack", -1, 1);
        }
        if (enemy.armature.animation.lastAnimationName == "ground_attack" && enemy.armature.animation.isCompleted)
        {
            enemy.ChangeState(new MageIdleState());
        }
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
