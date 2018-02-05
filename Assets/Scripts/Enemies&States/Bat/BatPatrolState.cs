using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPatrolState : IBatState
{
    public Bat enemy;

    public void Enter(Bat enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        enemy.nextPos = enemy.pathCordinates[enemy.nextPosNum];
        enemy.armature.animation.timeScale = 1.6f;
        Move();
    }

    public void Exit() { }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void Move()
    {
        enemy.batTransform.localPosition = Vector3.MoveTowards(enemy.batTransform.localPosition, enemy.nextPos, enemy.movementSpeed * Time.deltaTime);
        if (Vector3.Distance(enemy.batTransform.localPosition, enemy.nextPos) <= 0)
        {
            ChangePoint();
            ChangeDirection();
        }
    }

    public void ChangePoint()
    {
        if (enemy.nextPosNum != enemy.pathPoints.Length - 1)
        {
            enemy.nextPosNum++;
            enemy.nextPos = enemy.pathCordinates[enemy.nextPosNum];
        }
        else
        {
            enemy.nextPosNum = 0;
            enemy.nextPos = enemy.pathCordinates[enemy.nextPosNum];
        }
    }

    void ChangeDirection()
    {
        if (!enemy.facingRight && enemy.transform.localPosition.x < enemy.nextPos.x)
        {
            enemy.ChangeDirection();
        }
        if (enemy.facingRight && enemy.transform.localPosition.x > enemy.nextPos.x)
        {
            enemy.ChangeDirection();
        }
    }
}
