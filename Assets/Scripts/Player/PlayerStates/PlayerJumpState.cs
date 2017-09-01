using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    const int groundLayer = 8;
    const int platformLayer = 9;

    public void Enter(Player player)
    {
        player.Jump = true;
        player.myArmature.animation.FadeIn("jump", -1, -1);

        player.SetIndexes();
    }

    public void Execute()
    {
        if (Player.Instance.OnGround)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Player.Instance.mobileInput) > 0)
            {
                Player.Instance.ChangeState(new PlayerRunState());
            }
            else
            {
                Player.Instance.ChangeState(new PlayerIdleState());
            }
        }
        if (Player.Instance.Throw)
        {
            Player.Instance.ChangeState(new PlayerThrowState());
        }
        if (Player.Instance.Attack)
        {
            Player.Instance.ChangeState(new PlayerJumpAttackState());
        }
        if (Player.Instance.DoubleJump && Player.Instance.canJump && Player.Instance.myRigidbody.velocity.y < 6.5f)
        {
            Player.Instance.myArmature.animation.FadeIn("double_jump_start", -1, 1);
        }
        if (Player.Instance.myArmature.animation.isCompleted && Player.Instance.myArmature.animation.lastAnimationName == "double_jump_start")
        {
            Player.Instance.myArmature.animation.FadeIn("jump", -1, -1);
        }
    }

    public void Exit()
    {
        Player.Instance.Jump = false;
        Player.Instance.canJump = true;
        if (Player.Instance.OnGround && Player.Instance.gameObject.layer != platformLayer)
        {
            Player.Instance.gameObject.layer = groundLayer;
        }
    }
}
