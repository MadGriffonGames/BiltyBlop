using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanRangeState : ISnowmanState
{
    private Snowman enemy;
    bool isAlerted;
    bool isCharged;

    public void Enter(Snowman enemy)
    {
        this.enemy = enemy;
        isAlerted = false;
        isCharged = false;
    }

    public void Execute()
    {
        if (!isAlerted)
        {
            enemy.armature.animation.FadeIn("alert", -1, 1);
            isAlerted = true;
            SoundManager.PlaySound("snowman_shock");
        }
        if (enemy.armature.animation.lastAnimationName == "alert" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.timeScale = 2f;
            enemy.armature.animation.FadeIn("charge", -1, -1);
            enemy.AttackCollider.enabled = true;
            isCharged = true;
        }
        if (isCharged)
        {
            enemy.movementSpeed = 12;
            enemy.LocalMove();
            if (enemy.InMeleeRange)
            {
                enemy.ChangeState(new SnowmanMeleeState());
            }
        }
    }

    public void Exit()
    {
        enemy.armature.animation.timeScale = 1f;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            enemy.Target = null;
            enemy.ChangeDirection();
            enemy.ChangeState(new SnowmanPatrolState());
        }
    }
}
