using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("idle", -1, -1);
    }

    public void Execute()
    {
        if (Player.Instance.Jump || !Player.Instance.OnGround)
        {
            Player.Instance.ChangeState(new PlayerJumpState());
        }
        if (Player.Instance.OnGround && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            Player.Instance.ChangeState(new PlayerRunState());
        }
        if (Player.Instance.Attack)
        {
            Player.Instance.ChangeState(new PlayerAttackState());
        }
    }

    public void Exit() { }
}
