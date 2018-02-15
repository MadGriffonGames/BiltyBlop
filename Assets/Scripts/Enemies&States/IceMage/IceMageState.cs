using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IceMageState
{
    void Execute();
    void Enter(IceMage enemy);
    void Exit();
    void OnTriggerEnter2D(Collider2D other);
}
