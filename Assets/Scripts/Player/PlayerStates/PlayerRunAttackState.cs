using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunAttackState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("run_attack", -1, 1);
        player.EnableAttackCollider();
    }

    public void Execute()
    {
        if (Player.Instance.myArmature.animation.isCompleted)
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
    }

    public void Exit()
    {
        Player.Instance.Attack = false;
        Player.Instance.AttackCollider.enabled = false;
    }
}
