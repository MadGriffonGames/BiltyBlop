using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPigAttackState : IFlyingPigState
{
    private FlyingPig enemy;
    float distanceToKidX, distanceToKidY;

    public void Enter(FlyingPig enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (enemy.canAttack)
        {
            enemy.canAttack = false;

            enemy.armature.animation.FadeIn("atk", -1, 1);
            SoundManager.PlaySound("vulcan_sound");
            enemy.ThrowFireball();
        }
        else if(enemy.armature.animation.isCompleted)
        {
            enemy.ChangeState(new FlyingPigPatrolState());
        }
    }

    public void Exit()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }
}
