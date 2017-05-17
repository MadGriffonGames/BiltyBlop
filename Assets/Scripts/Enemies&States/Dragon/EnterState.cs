using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterState : MonoBehaviour, IDragonState
{
    private Dragon enemy;

    bool reachRight = false;
    bool reachLeft = false;

    public void Enter(Dragon enemy)
    {
        this.enemy = enemy; 
        enemy.PlayAnimation("FLY");
        enemy.armature.animation.timeScale = 2;
        enemy.speed = 1;
        enemy.takeDamageCollider.enabled = false;
    }

    public void Execute()
    {
        FirstEnter();
    }

    public void Exit() {}

    public void FirstEnter()
    {
        if (!reachLeft)
        {
            enemy.Move(15,0);
        }
        if (enemy.transform.position.x >= enemy.behindPosRight.position.x && reachLeft)
        {
            reachRight = true;
            enemy.ChangeDirection();
            enemy.transform.rotation = Quaternion.Euler(0, 0, 11);
        }
        if (!reachRight && reachLeft)
        {
            enemy.Move(15, 0);
        }
        if (enemy.transform.position.x <= enemy.behindPosLeft.position.x && !reachLeft)
        {
            reachLeft = true;
            reachRight = false;
            enemy.ChangeDirection();
            enemy.transform.rotation = Quaternion.Euler(0, 0, -11);
        }
        if (reachLeft && reachRight)
        {
            enemy.ChangeState(new FlyState());
        }
    }
}
