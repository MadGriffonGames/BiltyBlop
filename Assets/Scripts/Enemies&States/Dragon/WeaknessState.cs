using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessState : IDragonState
{
    private Dragon enemy;

    bool dead = false;

    public void Enter(Dragon enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;
        enemy.takeDamageCollider.enabled = true;
        enemy.enemyDamageCollider.enabled = false;
    }

    public void Execute()
    {
        string animName = enemy.armature.animation.lastAnimationName;
        if (!enemy.IsDead)
        {
            if (enemy.armature.animation.lastAnimationName == "WEAKNESS_START" && enemy.armature.animation.isCompleted)
            {
                enemy.armature.animation.FadeIn("WEAKNESS_IDLE", -1, 1);
                enemy.stun.SetActive(true);
            }
            if (enemy.armature.animation.lastAnimationName == "WEAKNESS_IDLE" && enemy.armature.animation.isCompleted)
            {
                enemy.stun.SetActive(false);
                enemy.armature.animation.FadeIn("WEAKNESS_END", -1, 1);
                enemy.takeDamageCollider.enabled = false;
                enemy.ChangeState(new RiseState());
            }
        }
        else
        {
            enemy.takeDamageCollider.enabled = false;
			enemy.enemyDamageCollider.enabled = false;
            enemy.stun.SetActive(false);
            Player.Instance.stars = 3;
            if (!dead)
            {
                enemy.armature.animation.timeScale = 0.9f;
                CameraEffect.Shake(0.2f, 1f);
                enemy.armature.animation.FadeIn("DEATH", -1, 1);
                GameManager.CollectedCoins += 200;
                GameManager.lvlCollectedCoins += 200;
            }
            dead = true;
            if (enemy.armature.animation.lastAnimationName == "DEATH" && enemy.armature.animation.isCompleted)
            {
                enemy.armature.animation.FadeIn("DEATH_IDLE", -1, -1);
                enemy.levelEnd.SetActive(true);
            }
        }
    }

    public void Exit() { }
}
