using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolemPatrolState : IHolemState
{
    private BossHolem enemy;

    public void Enter(BossHolem enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.FadeIn("Walk", -1, -1);
        enemy.movementSpeed = 0.8f;
        enemy.armature.animation.timeScale = 1;
    }

    public void Execute()
    {
        enemy.LocalMove();
        
        if (enemy.Target != null && enemy.canAttack)
        {
            if (enemy.InMeleeRange)
            {
                enemy.ChangeState(new HolemMeleeState());
            }
            else if (enemy.InShootingRange)
            {
                enemy.ChangeState(new HolemRangeState());
            }
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
