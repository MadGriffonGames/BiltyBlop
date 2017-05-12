using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterState : IDragonState
{
    private Dragon enemy;

    bool reachRight = false;
    bool reachLeft = false;

    public void Enter(Dragon enemy)
    {
        this.enemy = enemy;
        //Звук для появления дракона
        enemy.PlayAnimation("FLY");
        enemy.armature.animation.timeScale = 2;
        enemy.speed = 1;
    }

    public void Execute()
    {
        FirstEnter();
    }

    public void Exit() {}

    public void FirstEnter()
    {
        if (!reachRight)
        {
            enemy.Move();
        }
        if (enemy.transform.position.x >= enemy.behindPosRight.position.x)
        {
            reachRight = true;
            enemy.ChangeDirection();
            enemy.transform.rotation = Quaternion.Euler(0, 0, 11);
        }
        if (reachRight && !reachLeft)
        {
            enemy.Move();
        }
        if (enemy.transform.position.x <= enemy.behindPosLeft.position.x && !reachLeft)
        {
            reachLeft = true;
            enemy.ChangeDirection();
            enemy.transform.rotation = Quaternion.Euler(0, 0, -11);
        }
        if (reachLeft && reachRight)
        {
            enemy.ChangeState(new FlyState());
        }
    }
}
