using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpiderRollState : MonoBehaviour, ISpiderState
{
    private Spider enemy;
    float timer;
    float changingTime = 3.5f;
    float timer2;
    bool isUkused = false;


    public void Enter(Spider enemy)
    {
        timer = Time.time;
        timer2 = timer + timer;
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;
        enemy.armature.animation.FadeIn("roll", 1, 1);
    }

    public void Execute()
    {
        if (enemy.armature.animation.lastAnimationName == "roll" && enemy.armature.animation.isCompleted)
            enemy.armature.animation.FadeIn("roll", 1, 1);
       enemy.Move();

        if (Time.time - timer > changingTime && !isUkused)
        {
            enemy.ChangeState(new SpiderUkusState());
            isUkused = true;
        }
    }

    public void Exit()
    {
        
    }


    public void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            enemy.ChangeDirection();
        }
        if (other.gameObject.CompareTag("UkusEdge"))
            enemy.ChangeState(new SpiderUkusState());
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
