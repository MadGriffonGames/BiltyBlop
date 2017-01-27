using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;

    private float patrolTimer;

    private float patrolDuration;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        patrolDuration = enemy.patrolDuration;
    }

    public void Execute()
    {
        Patrol();
        enemy.Move();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
