using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PenguinThrowState : IPenguinState
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
        enemy.StopMoving();
        enemy.armature.animation.timeScale = 1.5f;
        if (!pre_attacked && enemy.Target != null)
        {
            enemy.armature.animation.FadeIn("pre_attack", -1, 1);
            pre_attacked = true;
        }
        if (pre_attacked && (enemy.armature.animation.lastAnimationName == "pre_attack") && enemy.armature.animation.isCompleted)
        {
            SoundManager.PlaySound("throw_sound2");
            enemy.armature.animation.FadeIn("attack", -1, 1);
            attacked = true;
            enemy.ThrowThreezubets();
        }

        if ((enemy.armature.animation.lastAnimationName == "attack") && (enemy.armature.animation.isCompleted))
            enemy.Attacked = true;

        if (attacked && (enemy.armature.animation.lastAnimationName == "attack") && (enemy.armature.animation.isCompleted))
        {
            enemy.ChangeState(new PenguinIdleState());
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
