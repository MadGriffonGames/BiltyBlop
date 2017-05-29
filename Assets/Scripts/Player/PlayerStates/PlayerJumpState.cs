using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("jump", -1, -1);
    }

    public void Execute()
    {
        if (Player.Instance.OnGround)
        {
            Player.Instance.ChangeState(new PlayerIdleState());
        }
        if (Player.Instance.Attack)
        {
            Player.Instance.ChangeState(new PlayerJumpAttackState());
        }
    }

    public void Exit()
    {
        Player.Instance.Jump = false;
    }
}
