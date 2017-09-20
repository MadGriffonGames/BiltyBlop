using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPigPatrolState : IFlyingPigState
{
    private FlyingPig enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(FlyingPig enemy)
    {

        this.enemy = enemy;
        patrolDuration = enemy.patrolDuration;
        enemy.armature.animation.timeScale = 1.2f;
    }

    public void Execute()
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, Player.Instance.transform.position, enemy.movementSpeed * Time.deltaTime);

        if (Mathf.Abs(Player.Instance.transform.position.x) > Mathf.Abs(enemy.transform.position.x))
        {

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
        }
    }
}
