using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISnowmanState
{
    void Execute();
    void Enter(Snowman enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
