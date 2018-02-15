using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHedgehogState
{
    void Execute();
    void Enter(Hedgehog enemy);
    void Exit();
    void OnTriggerEnter2D(Collider2D other);
}
