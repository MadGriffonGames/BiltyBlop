using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollIdleState : ITrollState
{
    private Troll enemy;

    public void Enter(Troll enemy)
    {
        this.enemy = enemy;
        enemy.armature.armature.animation.timeScale = 1.5f;

        enemy.armature.animation.FadeIn("Idle", -1, -1);
    }

    public void Execute()
    {
        if (enemy.Target == null || !enemy.InShootingRange)
        {
            enemy.ChangeState(new TrollPatrolState());
        }
        else
        {
            if (enemy.canAttack)
            {
                enemy.ChangeState(new TrollRangeState());
            }
        }
    }

    public void Exit()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
