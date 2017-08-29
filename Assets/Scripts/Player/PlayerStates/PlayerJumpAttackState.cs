using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttackState : IPlayerState
{
    const int groundLayer = 8;
    const int platformLayer = 9;

    public void Enter(Player player)
    {
        SoundManager.PlaySound("swing3");
        player.myArmature.animation.FadeIn("jump_attack", -1, 1);
        player.EnableAttackCollider();

        player.SetIndexes();
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
        if (Player.Instance.OnGround && Player.Instance.gameObject.layer != platformLayer)
        {
            Player.Instance.gameObject.layer = groundLayer;
        }
    }
}
