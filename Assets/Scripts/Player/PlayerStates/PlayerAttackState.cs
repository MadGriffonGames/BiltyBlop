using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IPlayerState
{
    public void Enter(Player player)
    {
        Player.Instance.isAttackTimerActive = true;
        Player.Instance.attackTimer = 0;
        switch (Player.Instance.attackAnimationCounter)
        {
            case 0:
                player.myArmature.animation.FadeIn("attack", -1, 1);
                Player.Instance.attackAnimationCounter++;
                break;
            case 1:
                player.myArmature.animation.FadeIn("attack_2", -1, 1);
                Player.Instance.attackAnimationCounter++;
                break;
            case 2:
                player.myArmature.animation.FadeIn("attack_3", -1, 1);
                Player.Instance.attackAnimationCounter = 0;
                break;
            default:
                break;
        }
        SoundManager.PlaySound("swing");
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
