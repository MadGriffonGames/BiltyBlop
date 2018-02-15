using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void Execute();
    void Enter(Player player);
    void Exit();
}
