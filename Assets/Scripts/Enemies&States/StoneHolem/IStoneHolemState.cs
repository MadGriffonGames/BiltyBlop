using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoneHolemState
{
    void Execute();
    void Enter(StoneHolem enemy);
    void Exit();
    void OnTriggerEnter2D(Collider2D other);
}
