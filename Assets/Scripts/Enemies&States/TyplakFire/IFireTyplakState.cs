using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFireTyplakState
{
    void Execute();
    void Enter(FireTyplak enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
