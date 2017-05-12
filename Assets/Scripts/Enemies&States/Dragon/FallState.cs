using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IDragonState
{
    private Dragon enemy;

    bool reach = false;

    public void Enter(Dragon enemy)
    {
        this.enemy = enemy;
        //Звук для появления дракона
        enemy.PlayAnimation("FLY");
        enemy.armature.animation.timeScale = 1;
        enemy.speed = 0.75f;
        enemy.flameFlow.SetActive(true);
    }

    public void Execute()
    {
        enemy.Move();
        if (enemy.transform.position.x >= enemy.behindPosRight.position.x)
        {
            
        }
    }

    public void Exit() { }
}
