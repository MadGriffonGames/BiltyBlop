using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : MonoBehaviour, IDragonState
{
    private Dragon enemy;

    bool weak = false;

    float timer;

    int fireballs = 0;
    int attacks = 0; 

    public void Enter(Dragon enemy)
    {
        this.enemy = enemy;
        enemy.armature.animation.timeScale = 1.5f;
    }

    public void Execute()
    {
        string animName = enemy.armature.animation.lastAnimationName;
        if (fireballs !=3 && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("GROUND_ATTACK_FULL", -1, 1);
            enemy.ThrowFireball();
            fireballs++;
        }
        if (fireballs == 3 && enemy.armature.animation.lastAnimationName == "GROUND_ATTACK_FULL" && enemy.armature.animation.isCompleted)
        {
            enemy.armature.animation.FadeIn("GROUND_IDLE", -1, 1);
            fireballs = 0;
            attacks++;
        }
        if (attacks == 1)
        {
            enemy.armature.animation.timeScale = 1f;
            enemy.armature.animation.FadeIn("WEAKNESS_START", -1, 1);
            enemy.ChangeState(new WeaknessState());
        }
    }

    public void Exit() { }
}
