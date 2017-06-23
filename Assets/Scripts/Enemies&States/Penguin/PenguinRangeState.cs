using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   public class PenguinRangeState : IPenguinState
{
    private Penguin enemy;
    bool pre_attacked = false;
    bool attacked = false;

    public void Enter(Penguin enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.2f;
    }

    public void Execute()
    {
        if (enemy.InShootingRange)
        {
            enemy.ChangeState(new PenguinThrowState());
        }

        else if (enemy.Target!=null)
        {
            pre_attacked = false;
            attacked = false;
            enemy.LocalMove();
        }

        else if (enemy.Target == null && enemy.Attacked)
        {
            enemy.ChangeState(new PenguinPatrolState());
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
