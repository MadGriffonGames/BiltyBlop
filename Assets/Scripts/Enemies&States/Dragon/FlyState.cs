using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyState : IDragonState
{
    private Dragon enemy;

    bool reachRight = false;
    bool reachLeft = false;

    bool flameOffLeft = false;
    bool flameOffRight = false;

    public void Enter(Dragon enemy)
    {
        SoundManager.PlaySound("breathing_fire1");
        this.enemy = enemy;
        enemy.PlayAnimation("FLY_ATTACK");
        enemy.armature.animation.timeScale = 2;
        enemy.speed = 0.75f;
        enemy.flameFlow.SetActive(true);
    }

    public void Execute()
    {
        if (!reachRight)
        {
            enemy.Move(10, 0);
        }
        if (reachRight && !reachLeft)
        {
            enemy.Move(10, 0);
        }
        if (enemy.transform.position.x <= enemy.behindPosLeft.position.x && !reachLeft)
        {
            SoundManager.PlaySound("breathing_fire1");
            reachLeft = true;
            reachRight = false;
            enemy.ChangeDirection();
            enemy.PlayAnimation("FLY_ATTACK");
            enemy.flameFlow.SetActive(true);
            enemy.transform.rotation = Quaternion.Euler(0, 0, -11);
        }
        if (reachLeft && reachRight)
        {
            enemy.flameFlow.SetActive(false);
            enemy.ChangeDirection();
            enemy.ChangeState(new FallState());
        }
        if (enemy.transform.position.x >= enemy.behindPosRight.position.x && reachLeft)
        {
            reachRight = true;
            enemy.transform.rotation = Quaternion.Euler(0, 0, 11);
        }
        if (enemy.transform.position.x >= enemy.flameOffRight.transform.position.x && reachLeft)
        {
            enemy.flameFlow.SetActive(false);
            if (!flameOffLeft)
            {
                enemy.armature.animation.FadeIn("FLY");
            }
            flameOffLeft = true;
        }
        if (enemy.transform.position.x <= enemy.flameOffLeft.transform.position.x && !reachLeft)
        {
            enemy.flameFlow.SetActive(false);
            if (!flameOffRight)
            {
                enemy.armature.animation.FadeIn("FLY");
            }
            flameOffRight = true;
        }
    }

    public void Exit() { }
}
