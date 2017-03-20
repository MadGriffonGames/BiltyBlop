using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBatState
{
    void Execute();
    void Enter(Bat enemy);
    void Exit();
    void OnTriggerEnter2D(Collider2D other);
}
