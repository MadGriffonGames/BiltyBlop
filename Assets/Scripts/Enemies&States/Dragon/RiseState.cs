using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseState : IDragonState
{
    private Dragon enemy;

    bool reachRight = false;
    bool reachLeft = false;

    public void Enter(Dragon enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;
    }

    public void Execute()
    {
        string animName = enemy.armature.animation.lastAnimationName;
        if (enemy.armature.animation.lastAnimationName == "WEAKNESS_END" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("RISE", -1, 1);
            enemy.Move(5, 5);
        }
        if (enemy.armature.animation.lastAnimationName == "RISE" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("FLY", -1, -1);
            enemy.enemySpawner.InstantiateEnemies();
        }
        if (enemy.transform.position.y >= enemy.risePoint.transform.position.y)
        {
            enemy.Move(15, 0);
        }
        if (enemy.transform.position.x <= enemy.behindPosLeft.position.x && !reachLeft)
        {
            reachLeft = true;
            enemy.ChangeDirection();
            enemy.transform.rotation = Quaternion.Euler(0, 0, -11);
        }
        if (enemy.transform.position.x >= enemy.behindPosRight.position.x && !reachRight)
        {
            enemy.ChangeDirection();
            enemy.transform.rotation = Quaternion.Euler(0, 0, 11);
            enemy.ChangeState(new FlyState());
        }
        
    }

    public void Exit() { }
}
