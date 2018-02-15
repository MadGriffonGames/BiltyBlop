using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFlowerMeleeState : IEvilFlowerState
{
    private EvilFlower enemy;

    private float attackCoolDown = 2;
    private bool preAttacked;
    bool preparated = false;

    public void Enter(EvilFlower enemy)
    {
        this.enemy = enemy;
        
        preAttacked = false;
    }

    public void Execute()
    {
        if (!preAttacked && !enemy.isAttacked)
        {
            preAttacked = true;
            enemy.armature.animation.FadeIn("pre_atk", -1, 1);
        }
        if (enemy.armature.animation.lastAnimationName == ("pre_atk") && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("atk", -1, 1);
            enemy.AttackCollider.enabled = true;
            SoundManager.PlaySound("bite");
        }

        if (enemy.armature.animation.lastAnimationName == ("atk") && enemy.armature.animation.isCompleted)
        {
            enemy.AttackCollider.enabled = false;
            enemy.isAttacked = true;
            enemy.ChangeState(new EvilFlowerIdleState());
        }
    }

    public void Exit() {}

	public void OnTriggerEnter2D(Collider2D other) {}
}
