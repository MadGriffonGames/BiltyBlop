using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

class SpiderGroundAttackState : ISpiderState
{
    private Spider enemy;
    float timer;
    float platformTime = 5f;
    bool groundAttackedTwice = false;
    bool platformed = false;
    

    bool isAttacked = false;

    public void Enter(Spider enemy)
    {
        timer = Time.time;
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (!isAttacked)
        {
            enemy.wallDestroy = false;
            enemy.armature.animation.FadeIn("ground_attack", 1, 1);
            isAttacked = true;
            CameraEffect.Shake(0.2f, 0.2f);
            enemy.WallFall();
        }

        if (Time.time - timer > platformTime && !groundAttackedTwice)
        {
            platformed = true;
            enemy.armature.animation.FadeIn("ground_attack", 1, 1);
        }

        if (enemy.armature.animation.lastAnimationName == "ground_attack" && isAttacked && enemy.armature.animation.isCompleted && !platformed)
        {
            platformed = true;
            enemy.SpawnPlatforms();
        }

        if (platformed)
        {
            Debug.Log("kek");
            enemy.ChangeState(new SpiderShopDaWoopState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
} 

