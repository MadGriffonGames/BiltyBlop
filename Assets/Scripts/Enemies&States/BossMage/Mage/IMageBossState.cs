using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMageBossState 
{
    void Execute();
    void Enter(MageBoss enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
