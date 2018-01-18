using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgrePatrolState : IOgreState
{
    private Ogre enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Ogre enemy)
    {
        this.enemy = enemy;
        patrolDuration = enemy.patrolDuration;
        enemy.armature.animation.timeScale = 1f;
        enemy.AttackCollider.enabled = false;
    }

    public void Execute()
    {
        enemy.LocalMove();
        if (enemy.Target != null && enemy.InMeleeRange)
        {
            enemy.ChangeState(new OgreMeleeState());
        }
    }

    public void Exit()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            enemy.ChangeDirection();
        }
    }
}
