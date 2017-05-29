using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttackState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.timeScale = 1.2f;
        player.myArmature.animation.FadeIn("jump_attack", 0.03f, 1);
    }

    public void Execute()
    {
        if (Player.Instance.OnGround)
        {
            Player.Instance.ChangeState(new PlayerIdleState());
        }
        if (Player.Instance.myArmature.animation.isCompleted && !Player.Instance.OnGround)
        {
            Player.Instance.ChangeState(new PlayerJumpState());
        }
    }

    public void Exit()
    {
        Player.Instance.Attack = false;
    }
}
