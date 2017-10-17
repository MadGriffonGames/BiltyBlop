using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShamanState
{
    void Execute();
    void Enter(Shaman enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
