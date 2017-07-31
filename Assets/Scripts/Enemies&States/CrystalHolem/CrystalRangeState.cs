using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRangeState : ICrystalState
{
    private CrystalHolem enemy;

    public void Enter(CrystalHolem enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.2f;
    }

    public void Execute()
    {

        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new CrystalMeleeState());
        }
        else if (enemy.Target != null)
        {
            enemy.LocalMove();
        }
        else
        {
            enemy.ChangeState(new CrystalPatrolState());
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
        }
    }
}
