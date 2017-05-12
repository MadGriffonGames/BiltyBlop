using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragonState
{
    void Execute();
    void Enter(Dragon enemy);
    void Exit();
}
