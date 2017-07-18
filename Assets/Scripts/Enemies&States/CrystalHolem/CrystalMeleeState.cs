using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMeleeState : MonoBehaviour, ICrystalState
{

    private CrystalHolem enemy;

    private float attackTimer;
    private float attackCoolDown = 1.5f;
    private bool canExit;
    bool isAttacked;
    bool isPreattacked;

    public void Enter(CrystalHolem enemy)
    {
        this.enemy = enemy;
        canExit = false;
        isAttacked = false;
        isPreattacked = false;
        enemy.armature.animation.timeScale = 1f;

    }

    public void Execute()
    {
        Attack();
        if (!enemy.InMeleeRange && canExit)
        {
            enemy.ChangeState(new CrystalRangeState());
        }
        else if (enemy.Target == null && canExit)
        {
            enemy.ChangeState(new CrystalPatrolState());
        }
    }

    public void Exit()
    {
        enemy.walk = false;
    }

    public void OnCollisionEnter2D(Collision2D other)
    { }

    private void Attack()
    {
        if (!isPreattacked)
        {
            enemy.armature.animation.timeScale = 2;
            enemy.armature.animation.FadeIn("pre_attack", -1, 1);
            enemy.EnableAttackCollider();
            enemy.isAttacking = true;
            isPreattacked = true;
        }
        if (!isAttacked && enemy.armature.animation.lastAnimationName == "pre_attack" && enemy.armature.animation.isCompleted)
        {
            SoundManager.PlaySound("holem_groundhit");
            enemy.armature.animation.timeScale = 1;
            enemy.armature.animation.FadeIn("attack", -1, 1);
            enemy.crystals.CrystalAttack();
            isAttacked = true;
        }
        if (enemy.armature.animation.lastAnimationName == "attack" && enemy.armature.animation.isCompleted)
        {
            canExit = true;
            enemy.isAttacking = false;
            enemy.AttackCollider.enabled = false;
        }
        if (isAttacked && enemy.armature.animation.lastAnimationName == "attack" && enemy.armature.animation.isCompleted)
        {
            enemy.ChangeState(new CrystalIdleState());
        }
    }
}
