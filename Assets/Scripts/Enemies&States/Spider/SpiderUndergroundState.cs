using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpiderUndergroundState : MonoBehaviour ,ISpiderState
{
    private Spider enemy;
    float timer;
    float particleTime = 0.2f;

    bool particled = false;

    public void Enter(Spider enemy)
    {
        enemy.armature.sortingOrder = 0;
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;
        enemy.armature.animation.FadeIn("drop_down", -1, 1);
        timer = Time.time;
    }

    public void Execute()
    {
        if (Time.time - timer > particleTime && !particled)
        {
            particled = true;
            Instantiate(enemy.groundParticles, enemy.gameObject.transform.position + new Vector3(3,1), Quaternion.identity);
        }

        if (enemy.armature.animation.lastAnimationName == "drop_down" && enemy.armature.animation.isCompleted)
            enemy.ChangeState(new SpiderRollState());
    }

    public void Exit()
    {
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }
}
