using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    const int groundLayer = 8;
    const int platformLayer = 9;

    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("idle", -1, -1);
        if (Player.Instance.OnGround && Player.Instance.gameObject.layer != platformLayer)
        {
            Player.Instance.gameObject.layer = groundLayer;
        }
    }

    public void Execute()
    {
        if (Player.Instance.Jump || !Player.Instance.OnGround)
        {
            Player.Instance.ChangeState(new PlayerJumpState());
        }
        if (Player.Instance.OnGround && Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Player.Instance.mobileInput) > 0)
        {
            Player.Instance.ChangeState(new PlayerRunState());
        }
        if (Player.Instance.Attack)
        {
            Player.Instance.ChangeState(new PlayerAttackState());
        }
        if (Player.Instance.Throw)
        {
            Player.Instance.ChangeState(new PlayerThrowState());
        }
        if (Player.Instance.takeHit)
        {
            Player.Instance.ChangeState(new PlayerTakeHitState());
        }
    }

    public void Exit() { }
}
