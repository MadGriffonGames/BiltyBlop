using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEFGreenState
{
    void Execute();
    void Enter(EvilFlowerGreen enemy);
    void Exit();
    void OnTriggerEnter2D(Collider2D other);
}
