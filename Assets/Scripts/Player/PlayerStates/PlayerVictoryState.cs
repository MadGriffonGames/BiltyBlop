using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictoryState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("victory", -1, 1);

        player.SetIndexes();
    }

    public void Execute()
    {
        if (Player.Instance.myArmature.animation.isCompleted)
        {
            Player.Instance.myArmature.animation.FadeIn("victory_idle", -1, 1);
            Player.Instance.SetIndexes();
        }
    }

    public void Exit()
    {
        
    }
}
