using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFGreenRangeState : IEFGreenState {

    private EvilFlowerGreen enemy;
    bool prepeared = false;
    float attackTime;

    public void Enter(EvilFlowerGreen enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 2f;
        enemy.armature.animation.FadeIn("PREPARATION", -1, 1);

    }

    public void Execute()
    {
        if ((enemy.armature.animation.lastAnimationName == ("PREPARATION") || enemy.armature.animation.lastAnimationName == "IDLE") && enemy.armature.animation.isCompleted)
        {
            SoundManager.PlaySound("spit");
            enemy.armature.animation.timeScale = 1.5f;
            attackTime += Time.deltaTime;
            enemy.armature.animation.FadeIn("ATTACK", -1, 1);
            enemy.ThrowSeed();
            enemy.acidFx.SetActive(true);
        }
        if ((enemy.armature.animation.lastAnimationName == ("ATTACK")) && enemy.armature.animation.isCompleted)
            enemy.ChangeState(new EFGreenIdleState());           
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}
