using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFlowerIdleState : IEvilFlowerState
{
    private EvilFlower enemy;
    bool isIdle;

    public void Enter(EvilFlower enemy)
    {
        this.enemy = enemy;
        isIdle = false;
        enemy.AttackCollider.enabled = false;
    }

    public void Execute()
    {
        Idle();
    }
    private void Idle()
    {
        if (!isIdle)
        {
            enemy.armature.animation.FadeIn("IDLE", -1, -1);
            isIdle = true;
        }
        if (enemy.Target != null)
        {
            enemy.ChangeState(new EvilFlowerMeleeState());
        }
    }

    public void Exit() {}

    public void OnTriggerEnter2D(Collider2D other) {}
}
