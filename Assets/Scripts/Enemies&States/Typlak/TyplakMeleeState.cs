using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyplakMeleeState : ITyplakState
{

    private Typlak enemy;

    private float attackTimer;
    private float attackCoolDown = 1.5f;
    private bool canAttack = true;

    public void Enter(Typlak enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Attack();
        if (!enemy.InMeleeRange && canAttack)
        {
            enemy.ChangeState(new TyplakRangeState());
        }
        else if (enemy.Target == null && canAttack)
        {
            enemy.ChangeState(new TyplakPatrolState());
        }
    }

    public void Exit() { }

    public void OnCollisionEnter2D(Collision2D other)
    {    }

    private void Attack()
    {
        enemy.MyAniamtor.SetFloat("speed", 0);
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }
        if (canAttack && enemy.Target != null)
        {
            canAttack = false;
            enemy.MyAniamtor.SetTrigger("attack");
        }
    }
}
