using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class CrystalBornState : ICrystalState
{
    private CrystalHolem enemy;

    public void Enter(CrystalHolem enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (enemy.armature.animation.isCompleted)
        {
            enemy.ChangeState(new CrystalPatrolState());
        }
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
