using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowState : IPlayerState
{
    public void Enter(Player player)
    {
        if (Input.GetAxis("Horizontal") != 0 || Player.Instance.mobileInput != 0)
        {
            player.myArmature.animation.FadeIn("throw_run", -1, 1);
        }
        else
        {
            player.myArmature.animation.FadeIn("throw_run", -1, 1);
        }
        player.ThrowWeapon();
        Player.Instance.Throw = false;
    }

    public void Execute()
    {
        if (Player.Instance.myArmature.animation.isCompleted && Player.Instance.OnGround)
        {
            if (Input.GetAxis("Horizontal") == 0 && Player.Instance.mobileInput == 0)
            {
                Player.Instance.ChangeState(new PlayerIdleState());
            }
            else
            {
                Player.Instance.ChangeState(new PlayerRunState());
            }
        }
        else if(Player.Instance.myArmature.animation.isCompleted && !Player.Instance.OnGround)
        {
            Player.Instance.ChangeState(new PlayerJumpState());
        }
    }

    public void Exit()
    {
        
    }
}
