using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyState : IDragonState
{
    private Dragon enemy;

    bool reachRight = false;
    bool reachLeft = false;

    public void Enter(Dragon enemy)
    {
        this.enemy = enemy;
        //Звук для появления дракона
        enemy.PlayAnimation("FLY_ATTACK");
        enemy.armature.animation.timeScale = 2;
        enemy.speed = 0.75f;
        enemy.ChangeDirection();
        enemy.flameFlow.SetActive(true);
    }

    public void Execute()
    {
        if (!reachRight)
        {
            enemy.Move();
        }
        if (enemy.transform.position.x >= enemy.behindPosRight.position.x)
        {
            reachLeft = false;
            reachRight = true;
            enemy.ChangeDirection();
            enemy.flameFlow.SetActive(true);
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
        if (enemy.transform.position.x >= enemy.flameOffRight.transform.position.x && !reachRight)
        {
            enemy.flameFlow.SetActive(false);
        }
        if (enemy.transform.position.x <= enemy.flameOffLeft.transform.position.x && !reachLeft)
        {
            enemy.flameFlow.SetActive(false);
        }
    }

    public void Exit() { }
}
