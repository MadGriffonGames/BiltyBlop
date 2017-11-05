using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFGreenRangeState : IEFGreenState
{

    private EvilFlowerGreen enemy;
    float attackTime;

    public void Enter(EvilFlowerGreen enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 2f;
        enemy.armature.animation.FadeIn("pre_atk", -1, 1);

    }

    public void Execute()
    {
        if ((enemy.armature.animation.lastAnimationName == ("pre_atk") || enemy.armature.animation.lastAnimationName == "IDLE") && enemy.armature.animation.isCompleted)
        {
            SoundManager.PlaySound("spit");
            attackTime += Time.deltaTime;
            enemy.armature.animation.FadeIn("atk", -1, 1);
            enemy.ThrowSeed();
        }
        if ((enemy.armature.animation.lastAnimationName == ("atk")) && enemy.armature.animation.isCompleted)
        {
            enemy.ChangeState(new EFGreenIdleState());
        }
                     
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}
