using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalIdleState : ICrystalState
{
    private CrystalHolem enemy;

    private float idleTimer;
    private float idleDuration;

    public void Enter(CrystalHolem enemy)
    {
        this.enemy = enemy;
        idleDuration = enemy.idleDuration;
        enemy.armature.animation.FadeIn("Idle", -1, -1);
    }

    public void Execute()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new CrystalPatrolState());
        }
        if (enemy.Target != null)
        {
            enemy.ChangeState(new CrystalRangeState());
        }
    }

    public void Exit()
    { }

    public void OnCollisionEnter2D(Collision2D other)
    { }
}
