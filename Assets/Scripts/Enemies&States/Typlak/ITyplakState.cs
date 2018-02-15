using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITyplakState
{
    void Execute();
    void Enter(Typlak enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}