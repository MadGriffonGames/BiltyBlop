using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

internal class SpiderIdleState : MonoBehaviour, ISpiderState
{
    private Spider enemy;
    float idlingTime = 2.1f;
    float timer;
    bool isIdling;

    public void Enter(Spider enemy)
    {
        enemy.GetComponent<PolygonCollider2D>().enabled = true;
        timer = Time.time;
        this.enemy = enemy;
        isIdling = false;
    }

    public void Execute()
    {
        if (!isIdling)
        {
            enemy.armature.animation.FadeIn("shoop_weak", -1, -1);
            isIdling = true;
            enemy.stun.SetActive(true);
        }

        if (Time.time - timer > idlingTime)
        {
            enemy.ChangeState(new SpiderStonesState());
        }
    }

    public void Exit()
    {
        enemy.GetComponent<PolygonCollider2D>().enabled = false;
        enemy.stun.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}