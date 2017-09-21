using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlyingPigState
{
    void Execute();
    void Enter(FlyingPig enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
