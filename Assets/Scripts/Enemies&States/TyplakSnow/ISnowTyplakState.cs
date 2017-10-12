using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISnowTyplakState 
{
    void Execute();
    void Enter(SnowTyplak enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
