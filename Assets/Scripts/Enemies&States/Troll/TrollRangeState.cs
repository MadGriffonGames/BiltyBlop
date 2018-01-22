using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollRangeState : ITrollState
{
    private Troll enemy;

    public void Enter(Troll enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.FadeIn("charge", -1, -1);
        enemy.movementSpeed = 5;
        SoundManager.PlaySound("troll_sound 1");
    }

    public void Execute()
    {

        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new TrollSelfDestroyState());
        }
        else if (enemy.Target != null)
        {
            enemy.LocalMove();
        }
        else
        {
            enemy.ChangeState(new TrollPatrolState());
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
