using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFlowerIdleState : IEvilFlowerState
{
    private EvilFlower enemy;
    bool idle = false;

    public void Enter(EvilFlower enemy)
    {
        this.enemy = enemy;
        enemy.StopIgnore();
        enemy.AttackCollider.enabled = false;
    }

    public void Execute()
    {
        Idle();
    }
    private void Idle()
    {
        if (!idle)
        {
            enemy.armature.animation.FadeIn("Idle", -1, -1);
            idle = true;
        }
        if (enemy.Target != null)
        {
            enemy.ChangeState(new EvilFlowerMeleeState());
        }
    }

    public void Exit() {}

    public void OnTriggerEnter2D(Collider2D other) {}
}
