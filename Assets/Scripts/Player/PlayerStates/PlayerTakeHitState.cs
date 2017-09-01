using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeHitState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("takehit", -1, 1);

        player.SetIndexes();
    }

    public void Execute()
    {
        if (Player.Instance.myArmature.animation.isCompleted)
        {
            Player.Instance.ChangeState(new PlayerIdleState());
        }
    }
     
    public void Exit()
    {
        Player.Instance.takeHit = false;
    }
}
