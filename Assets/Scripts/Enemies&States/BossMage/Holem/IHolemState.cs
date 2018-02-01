using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHolemState
{
    void Execute();
    void Enter(BossHolem enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
