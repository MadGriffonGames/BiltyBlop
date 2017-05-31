using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttackState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("jump_attack", 0.03f, 1);
        player.EnableAttackCollider();
    }

    public void Execute()
    {
        if (Player.Instance.myArmature.animation.isCompleted && Player.Instance.OnGround)
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
        Player.Instance.AttackCollider.enabled = false;
        if (Player.Instance.OnGround)
        {
            Player.Instance.gameObject.layer = 8;
        }
    }
}
