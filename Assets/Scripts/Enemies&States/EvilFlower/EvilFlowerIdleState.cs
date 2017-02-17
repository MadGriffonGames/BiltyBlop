using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFlowerIdleState : IEvilFlowerState
{
    private EvilFlower enemy;

    public void Enter(EvilFlower enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (enemy.Target != null)
        {
            enemy.ChangeState(new EvilFlowerMeleeState());
        }
    }

    public void Exit() {}

    public void OnTriggerEnter2D(Collider2D other) {}
}
