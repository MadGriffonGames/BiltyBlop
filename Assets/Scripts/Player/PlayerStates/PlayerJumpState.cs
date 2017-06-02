using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    const int groundLayer = 8;
    const int platformLayer = 9;

    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("jump", -1, -1);
    }

    public void Execute()
    {
        if (Player.Instance.OnGround)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                Player.Instance.ChangeState(new PlayerRunState());
            }
            else
            {
                Player.Instance.ChangeState(new PlayerIdleState());
            }
        }
        if (Player.Instance.Attack)
        {
            Player.Instance.ChangeState(new PlayerJumpAttackState());
        }
    }

    public void Exit()
    {
        Player.Instance.Jump = false;
        if (Player.Instance.OnGround && Player.Instance.gameObject.layer != platformLayer)
        {
            Player.Instance.gameObject.layer = groundLayer;
        }
    }
}
