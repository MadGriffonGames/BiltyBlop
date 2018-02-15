using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPigPatrolState : IFlyingPigState
{
    private FlyingPig enemy;
    float distanceToKidX, distanceToKidY;
    Vector3 target;

    public void Enter(FlyingPig enemy)
    {
        this.enemy = enemy;

        enemy.armature.animation.FadeIn("fly", -1, -1);
        enemy.armature.animation.timeScale = 1;
    }

    public void Execute()
    {
        if (enemy.isActive)
        {
            distanceToKidX = Mathf.Abs(Player.Instance.transform.position.x - enemy.transform.position.x);
            distanceToKidY = Mathf.Abs(Player.Instance.transform.position.y - enemy.transform.position.y);

            target = Player.Instance.target.transform.position - new Vector3(0, 1, 0);

            if (distanceToKidX > 3 || distanceToKidY > 3.5f)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.movementSpeed * Time.deltaTime);
            }

            float xDir = Player.Instance.transform.position.x - enemy.transform.position.x;//xDir shows target from left or right side 

            if ((xDir < 0 && enemy.facingRight) || (xDir > 0 && !enemy.facingRight))
            {
                enemy.ChangeDirection();
            }

            if (distanceToKidX < 5f && enemy.canAttack)
            {
                enemy.ChangeState(new FlyingPigAttackState());
            }
        }
    }

    public void Exit()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }
}
