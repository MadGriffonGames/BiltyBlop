﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageDeathState : IMageBossState
{
    private MageBoss enemy;

    bool isDead;

    public void Enter(MageBoss enemy)
    {
        this.enemy = enemy;
        isDead = false;
    }

    public void Execute()
    {
        if (!isDead)
        {
            isDead = true;
            enemy.mageCollider.enabled = false;
            enemy.damageCollider.enabled = false;
            enemy.armature.armature.animation.FadeIn("death", -1, 1);
        }
        if (enemy.armature.animation.lastAnimationName == "death" && enemy.armature.animation.isCompleted)
        {
            enemy.runeStone.SetActive(true);
        }
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
