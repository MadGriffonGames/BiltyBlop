using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpiderStonesState : MonoBehaviour, ISpiderState
{

    private Spider enemy;


    public void Enter(Spider enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;
    }

    public void Execute()
    {
        if (enemy.armature.animation.lastAnimationName == "appear1")
            enemy.armature.animation.FadeIn("Stone_down_preatk", 1, 1);
        if (enemy.armature.animation.lastAnimationName == "Stone_down_preatk" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("stone_down", 1, 1);
            SpawnStones();
        }

        if (enemy.armature.animation.lastAnimationName == "stone_down" && enemy.armature.animation.isCompleted)
            enemy.ChangeState(new SpiderUndergroundState());
    }

    public void Exit()
    {
    }

    void SpawnStones()
    {
        for (int i = 0; i < 5; i++)
            enemy.spiderStones[i].SetActive(true);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }
}