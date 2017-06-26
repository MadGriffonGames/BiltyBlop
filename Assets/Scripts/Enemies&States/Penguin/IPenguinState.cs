using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPenguinState
{
    void Execute();
    void Enter(Penguin enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}