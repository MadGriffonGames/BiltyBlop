using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollRangeState : MonoBehaviour, ITrollState
{
    private Troll enemy;
    bool isPreAttacked;
    bool canExit;

    public void Enter(Troll enemy)
    {
        this.enemy = enemy;
        isPreAttacked = false;
        canExit = false;
    }

    public void Execute()
    {
        if (enemy.canAttack)
        {
            if (!isPreAttacked && enemy.InShootingRange)
            {
                enemy.armature.animation.timeScale = 1.8f;
                canExit = false;           
                enemy.isAttacking = true;
                enemy.armature.animation.FadeIn("pre_atk", -1, 1);
                isPreAttacked = true;
            }
            if (enemy.armature.animation.lastAnimationName == "pre_atk" && enemy.armature.animation.isCompleted)
            {
                enemy.armature.animation.FadeIn("range_atk", -1, 1);
                InstantiateBomb();
            }

            if (enemy.armature.animation.lastAnimationName == "range_atk" && enemy.armature.animation.isCompleted)
            {
                enemy.isAttacking = false;
                enemy.canAttack = false;
                enemy.isTimerTick = true;
                isPreAttacked = false;
                canExit = true;
            }
        }
        if (canExit)
        {
            if (enemy.InShootingRange)
            {
                enemy.ChangeState(new TrollIdleState());
            }
            else
            {
                enemy.ChangeState(new TrollPatrolState());
            }
        }
    }

    public void Exit()
    {

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

    void InstantiateBomb()
    {
        int direction = enemy.facingRight ? 1 : -1;

        GameObject tmp = Instantiate(enemy.bomb, enemy.transform.position + new Vector3(1 * direction, 1f, 0), Quaternion.identity);
        tmp.transform.parent = null;
        tmp.GetComponent<TrollBomb>().blowTime = 1.5f;
        tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(5 * direction, 4);
    }
}
