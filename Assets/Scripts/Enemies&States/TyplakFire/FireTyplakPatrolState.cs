﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTyplakPatrolState : IFireTyplakState
{
    private FireTyplak enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(FireTyplak enemy)
    {
        this.enemy = enemy;
        patrolDuration = enemy.patrolDuration;
        enemy.armature.animation.timeScale = 1.2f;
    }

    public void Execute()
    {
        enemy.LocalMove();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new FireTyplakRangeState());
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
