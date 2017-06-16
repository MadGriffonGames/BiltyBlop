using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFlowerMeleeState : IEvilFlowerState
{
    private EvilFlower enemy;

    private float attackTimer;
    private float attackCoolDown = 2;
    private bool canAttack = true;
    bool preparated = false;

    public void Enter(EvilFlower enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.FadeIn("PREPARATION", -1, 1);
        enemy.StopIgnore();
    }

    public void Execute()
    {
        if (enemy.armature.animation.lastAnimationName == ("PREPARATION") && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.timeScale = 1.5f;
            enemy.armature.animation.FadeIn("ATTACK", -1, 1);
            enemy.AttackCollider.enabled = true;
            SoundManager.PlaySound("bite");
        }

        if (enemy.armature.animation.lastAnimationName == ("ATTACK") && enemy.armature.animation.isCompleted)
        {
            enemy.AttackCollider.enabled = false;
            enemy.ChangeState(new EvilFlowerIdleState());
        }
    }

    public void Exit() {}

	public void OnTriggerEnter2D(Collider2D other) {}
}
