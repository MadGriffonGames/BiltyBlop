using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MonoBehaviour ,IDragonState
{
    private Dragon enemy;

    bool reachFallPoint = false;
    bool fall = false;
    bool grounded = false;

    public void Enter(Dragon enemy)
    {
        this.enemy = enemy;
        enemy.PlayAnimation("FLY");
        enemy.armature.animation.timeScale = 2;
        enemy.speed = 1;
    }

    public void Execute()
    {
        if (!reachFallPoint)
        {
            enemy.Move(10, 0);
        }
        if (enemy.transform.position.x <= enemy.fallPoint.transform.position.x)
        {
            reachFallPoint = true;
            if (!fall)
            {
                enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                enemy.armature.animation.FadeIn("FALL", -1, 1);
            }
            fall = true;
            enemy.Move(5, -7);
        }
        if (enemy.transform.position.y <= enemy.groundPoint.transform.position.y)
        {
            if (!grounded)
            {
                enemy.Move(0, 0);
            }
            grounded = true;
        }
        if (grounded)
        {
            enemy.ChangeState(new GroundState());
        }
    }

    public void Exit() { }
}
