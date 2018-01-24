using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollPatrolState : ITrollState
{
    private Troll enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Troll enemy)
    {
        this.enemy = enemy;
        patrolDuration = enemy.patrolDuration;
        enemy.armature.animation.FadeIn("Run", -1, -1);
        enemy.movementSpeed = 3;
        enemy.armature.animation.timeScale = 1;
    }

    public void Execute()
    {
        enemy.LocalMove();
        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new TrollSelfDestroyState());
        }
        if (enemy.Target != null && enemy.canAttack)
        {
            enemy.ChangeState(new TrollRangeState());
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
