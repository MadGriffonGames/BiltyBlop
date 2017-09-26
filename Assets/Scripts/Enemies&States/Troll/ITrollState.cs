using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrollState
{
    void Execute();
    void Enter(Troll enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
