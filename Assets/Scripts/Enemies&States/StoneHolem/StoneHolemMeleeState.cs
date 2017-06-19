using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHolemMeleeState : IStoneHolemState
{

    private StoneHolem enemy;

    private float attackTimer;
    private float attackCoolDown = 2;
    private bool canAttack = true;

    public void Enter(StoneHolem enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Attack();
        if (!enemy.InMeleeRange)
        {
            enemy.ChangeState(new StoneHolemRangeState());
        }
        else if (enemy.Target == null)
        {
            enemy.ChangeState(new StoneHolemIdleState());
        }
    }

    public void Exit() { }

    public void OnTriggerEnter2D(Collider2D other) { }

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
        }
    }
}
