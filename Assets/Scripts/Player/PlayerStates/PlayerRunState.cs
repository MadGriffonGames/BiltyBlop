using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IPlayerState
{
    bool isRuning = false;

    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("run", -1, -1);
    }

    public void Execute()
    {
        if (Player.Instance.isRewinding && !isRuning)
        {
            Player.Instance.myArmature.animation.FadeIn("run", -1, -1);
            isRuning = true;
        }
        if (Input.GetAxis("Horizontal") == 0 && Player.Instance.mobileInput == 0)
        {
            Player.Instance.ChangeState(new PlayerIdleState());
        }
        if (!Player.Instance.OnGround)
        {
            Player.Instance.ChangeState(new PlayerJumpState());
        }
        if (Player.Instance.Attack && Mathf.Abs(Player.Instance.myRigidbody.velocity.x) >= 6.8f)
        {
            Player.Instance.ChangeState(new PlayerRunAttackState());
        }
        else if (Player.Instance.Attack && Mathf.Abs(Player.Instance.myRigidbody.velocity.x) < 6.8f)
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

    public void Exit()
    {
        isRuning = false;
    }
}
