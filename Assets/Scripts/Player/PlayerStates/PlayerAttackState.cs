using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IPlayerState
{
    public void Enter(Player player)
    {
        player.myArmature.animation.FadeIn("attack", -1, 1);
        SoundManager.PlaySound("swing");
        player.EnableAttackCollider();

        player.SetIndexes();
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
