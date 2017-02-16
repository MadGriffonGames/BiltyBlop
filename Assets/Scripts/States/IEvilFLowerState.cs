using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvilFlowerState
{
    void Execute();
    void Enter(EvilFlower enemy);
    void Exit();
    void OnTriggerEnter2D(Collider2D other);
}
