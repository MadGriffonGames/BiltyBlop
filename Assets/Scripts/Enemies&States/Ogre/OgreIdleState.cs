using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreIdleState : IOgreState
{
    private Ogre enemy;

    public void Enter(Ogre enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1f;
    }

    public void Execute()
    {
        Idle();
        if (enemy.Target == null)
        {
            enemy.ChangeState(new OgrePatrolState());
        }
        if (enemy.Target != null && enemy.canAttack)
        {
            enemy.ChangeState(new OgreMeleeState());
        }
    }
    void Idle()
    {
        if (enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("idle", -1, -1);
        }
    }

    public void Exit()
    {
       
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
