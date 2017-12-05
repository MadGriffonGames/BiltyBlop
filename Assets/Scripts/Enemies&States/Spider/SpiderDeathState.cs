using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class SpiderDeathState : MonoBehaviour, ISpiderState
{
    Spider enemy;
    bool deadAnimated;

    public void Enter(Spider enemy)
    {
        this.enemy = enemy;
        deadAnimated = false;
    }

    public void Execute()
    {
        if (!deadAnimated)
        {
            enemy.armature.animation.FadeIn("deathworm", -1, 1);
            Player.Instance.stars = 3;
            enemy.levelEnd.SetActive(true);
            Destroy(enemy.enemyDamageRoll);
            Destroy(enemy.enemyDamageStone);
            deadAnimated = true;
            AchievementManager.Instance.CheckLevelAchieve(AchievementManager.Instance.spiderKiller);
        }
    }

    public void Exit()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}

