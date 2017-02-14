using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;

    private float idleTimer;

    private float idleDuration;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        idleDuration =  enemy.idleDuration;
    }

    public void Execute()
    {
        if (enemy.Target != null)
        {
            enemy.ChangeState(new MeleeState());
        }
    }

    public void Exit() {}

    public void OnTriggerEnter2D(Collider2D other) {}
}
