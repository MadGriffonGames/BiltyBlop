using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("attack", 0.03f, 1);
        player.EnableAttackCollider();
    }

    public void Execute()
    {
        if (Player.Instance.myArmature.animation.isCompleted)
        {
            Player.Instance.ChangeState(new PlayerIdleState());
        }
    }

    public void Exit()
    {
        Player.Instance.Attack = false;
        Player.Instance.AttackCollider.enabled = false;
    }
}
