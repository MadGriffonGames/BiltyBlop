using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogIdleState : IHedgehogState
{
    private Hedgehog enemy;

    private float idleTimer;

    private float idleDuration;

    bool idle = false;

    public void Enter(Hedgehog enemy)
    {
        this.enemy = enemy;
        idleDuration = enemy.idleDuration;
        enemy.movementSpeed = 0;
    }

    public void Execute()
    {
        Idle();
    }

    private void Idle()
    {
        if (!idle)
        {
            enemy.armature.animation.FadeIn("idle", -1, 1);
            idle = true;
        }

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new HedgehogPatrolState());
        }
    }

    public void Exit() { }

    public void OnTriggerEnter2D(Collider2D other) { }
}
