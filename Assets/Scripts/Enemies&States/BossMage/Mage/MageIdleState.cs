﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageIdleState : IMageBossState
{
    private MageBoss enemy;
    bool fromTeleport;
    float timer;
    const float TIMER_AFTER_TELEPORT = 0.5f;
    const float TIMER_AFTER_ATTACK = 0.5f;

    public void Enter(MageBoss enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1;
        enemy.armature.animation.FadeIn("Idle", -1, -1);
        timer = 0;
        if (enemy.lastState != null)
        {
            fromTeleport = enemy.lastState.GetType() == new MageTeleportState().GetType() ? true : false;
        }
        else
        {
            fromTeleport = false;
        }
    }

    public void Execute()
    {
        if (enemy.isActive)
        {
            if (fromTeleport)
            {
                timer += Time.deltaTime;
                if (timer >= TIMER_AFTER_ATTACK)
                {
                    timer = 0;
                    enemy.ChangeState(enemy.GetRandomState());
                }
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= TIMER_AFTER_TELEPORT)
                {
                    timer = 0;
                    enemy.ChangeState(new MageTeleportState());
                }
            }
        }
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
