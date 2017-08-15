using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpiderState
{
    void Execute();
    void Enter(Spider enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}

