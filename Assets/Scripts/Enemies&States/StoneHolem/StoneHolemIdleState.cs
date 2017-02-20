using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHolemIdleState : IStoneHolemState
{

    private StoneHolem enemy;

    private float idleTimer;

    private float idleDuration;

    public void Enter(StoneHolem enemy)
    {
        this.enemy = enemy;
        idleDuration = enemy.idleDuration;
    }

    public void Execute()
    {
        Idle();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new StoneHolemRangeState());
        }
    }

    private void Idle()
    {
        enemy.MyAniamtor.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new StoneHolemPatrolState());
        }
    }

    public void Exit() {}

    public void OnTriggerEnter2D(Collider2D other) {}
}
