using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTeleportState : IMageBossState
{
    private MageBoss enemy;
    bool isMoving;
    bool isTeleported;
    bool isAppearing;

    public void Enter(MageBoss enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1;
        isMoving = false;
        isTeleported = false;
        isAppearing = false; ;
    }

    public void Execute()
    {
        Teleport();
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }

    void Teleport()
    {
        if (!isMoving)
        {
            isMoving = true;
            enemy.mageCollider.enabled = false;
            enemy.damageCollider.enabled = false;
            enemy.armature.armature.animation.FadeIn("idle_teleport", -1, 1);
        }
        if (enemy.armature.animation.lastAnimationName == "idle_teleport" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("teleport", -1, 1);
        }
        if (enemy.armature.animation.lastAnimationName == "teleport" && enemy.armature.animation.isCompleted)
        {
            if (!isTeleported)
            {
                enemy.transform.position = enemy.GetTeleportPoint();
            }
            enemy.armature.animation.FadeIn("appear", -1, 1);
            enemy.Stop();
        }
        else if(enemy.armature.animation.lastAnimationName == "teleport" && !enemy.armature.animation.isCompleted)
        {
            enemy.Move();
        }
        if (enemy.armature.animation.lastAnimationName == "appear" && enemy.armature.animation.isCompleted)
        {
            enemy.mageCollider.enabled = true;
            enemy.damageCollider.enabled = true;
            enemy.ChangeState(new MageIdleState());
        }
    }
}
