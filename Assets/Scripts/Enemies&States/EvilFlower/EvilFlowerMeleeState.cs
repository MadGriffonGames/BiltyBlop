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

    public void Enter(EvilFlower enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Attack();
        if(enemy.Target == null)
        {
            enemy.ChangeState(new EvilFlowerIdleState());
        }
    }

    public void Exit() {}

	public void OnTriggerEnter2D(Collider2D other) {}

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if (canAttack)
        {
            canAttack = false;
            enemy.MyAniamtor.SetTrigger("attack");
        }
    }
}
