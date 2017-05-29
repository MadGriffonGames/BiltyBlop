﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("walk", -1, -1);
    }

    public void Execute()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            Player.Instance.ChangeState(new PlayerIdleState());
        }
        if (Player.Instance.Jump && !Player.Instance.OnGround)
        {
            Player.Instance.ChangeState(new PlayerJumpState());
        }
        if (Player.Instance.Attack)
        {
            Player.Instance.ChangeState(new PlayerAttackState());
        }
    }

    public void Exit() { }
}
