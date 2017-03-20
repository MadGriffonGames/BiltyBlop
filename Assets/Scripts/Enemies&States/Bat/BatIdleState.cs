using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIdleState : IBatState
{
    private Bat enemy;

    private float idleTimer;

    private float idleDuration;

    public void Enter(Bat enemy)
    {
        this.enemy = enemy;
        idleDuration = enemy.idleDuration;
    }

    public void Execute()
    {
        Idle();
    }

    private void Idle()
    {
        enemy.MyAniamtor.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new BatPatrolState());
        }
    }

    public void Exit() { }

    public void OnTriggerEnter2D(Collider2D other) { }
}
