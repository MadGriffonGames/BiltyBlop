using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrystalState
{
    void Execute();
    void Enter(CrystalHolem enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
