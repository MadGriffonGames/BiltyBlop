using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IYetiState
{
    void Execute();
    void Enter(Yeti enemy);
    void Exit();
    void OnTriggerEnter2D(Collider2D other);
}