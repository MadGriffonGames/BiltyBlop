using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolemIdleState : IHolemState
{
    private BossHolem enemy;

    public void Enter(BossHolem enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.FadeIn("Idle", -1, -1);
    }

    public void Execute()
    {
        if (enemy.Target == null)
        {
            enemy.ChangeState(new HolemPatrolState());
        }
        else
        {
            if (enemy.canAttack)
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
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
