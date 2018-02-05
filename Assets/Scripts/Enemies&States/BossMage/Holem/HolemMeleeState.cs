using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolemMeleeState : IHolemState
{
    private BossHolem enemy;

    private bool canExit = true;
    bool attack = false;
    Vector3 targetPos;

    public void Enter(BossHolem enemy)
    {
        this.enemy = enemy;
        targetPos = enemy.Target.transform.position;
    }

    public void Execute()
    {
        Attack();
        if (enemy.Target == null && canExit)
        {
            enemy.ChangeState(new HolemPatrolState());
        }
        if (enemy.Target != null && !enemy.canAttack && canExit)
        {
            enemy.ChangeState(new HolemIdleState());
        }
    }

    public void Exit()
    {
        enemy.walk = false;
        enemy.isAttacking = false;
        enemy.AttackCollider.enabled = false;
        enemy.armature.animation.timeScale = 1f;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            enemy.Target = null;
            enemy.ChangeDirection();
            enemy.Target = null;
        }
    }

    private void Attack()
    {
        if (enemy.canAttack)
        {
            if (Vector2.Distance(enemy.transform.position, targetPos) <= enemy.meleeRange - 1.5f)
            {
                SpikesAttack();
            }
            else
            {
                HandAttack();
            }
            
        }
    }

    void SpikesAttack()
    {
        if (!attack)
        {

            enemy.armature.animation.timeScale = 1.5f;
            enemy.armature.animation.FadeIn("Spikes_atk_1", -1, 1);
            SoundManager.PlaySound("holem_earthroll");
            enemy.spikes.SetActive(true);
            canExit = false;
            enemy.isAttacking = true;
            attack = true;
        }

        if (enemy.armature.animation.lastAnimationName == "Spikes_atk_1" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("Spikes_atk_2", -1, 1);
            SoundManager.PlaySound("holem_earthroll");
        }
        if (enemy.armature.animation.lastAnimationName == "Spikes_atk_2" && enemy.armature.animation.isCompleted)
        {
            enemy.spikes.GetComponent<Animator>().enabled = true;
            enemy.isAttacking = false;
            enemy.canAttack = false;
            enemy.isTimerTick = true;
            attack = false;
            canExit = true;
        }
    }

    void HandAttack()
    {
        if (!attack)
        {
            enemy.armature.animation.FadeIn("attack", -1, 1);
            SoundManager.PlaySound("holem_swosh");
            enemy.EnableAttackCollider();
            canExit = false;
            enemy.isAttacking = true;
            attack = true;
        }

        if (enemy.armature.animation.lastAnimationName == "attack" && enemy.armature.animation.isCompleted)
        {
            enemy.AttackCollider.enabled = false;
            enemy.isAttacking = false;
            enemy.canAttack = false;
            enemy.isTimerTick = true;
            attack = false;
            canExit = true;
        }
    }
}
