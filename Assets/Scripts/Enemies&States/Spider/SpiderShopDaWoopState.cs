using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

class SpiderShopDaWoopState : ISpiderState
{
    private Spider enemy;
    bool isShoped = false;
    float timer;
    float timer2;
    float pauseTime = 1.8f;
    float attackTime = 3.7f;
    bool isLazered = false;


    public void Enter(Spider enemy)
    {
        timer = Time.time;
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (!isShoped)
        {
            isShoped = true;
            enemy.armature.animation.FadeIn("shop_da_woop", 1, 1);
        }

        if (isShoped && Time.time - timer > pauseTime && !isLazered)
        {
            
            enemy.lazer.SetActive(true);
            SoundManager.PlaySound("laserSound");
            isLazered = true;
        }

        if (Time.time - timer > attackTime)
        {
            enemy.lazer.gameObject.SetActive(false);

            enemy.ChangeState(new SpiderIdleState());
        }

    }

    public void Exit()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}