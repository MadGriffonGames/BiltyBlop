using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMageDeathState : IceMageState
{

    private IceMage enemy;
    bool isDead = false;

    public void Enter(IceMage enemy)
    {
        
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 2.5f;
        enemy.armature.animation.FadeIn("death", 1, 1);
        enemy.fireball.SetActive(false);
        enemy.gameObject.SetActive(false);
        enemy.deathPic.gameObject.SetActive(true);
    }

    public void Execute()
    {

    }

    public void Exit()
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}

