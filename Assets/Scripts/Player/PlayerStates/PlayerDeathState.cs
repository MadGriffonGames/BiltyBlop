using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : IPlayerState
{
    bool isTookHit;
    bool isDead;

    public void Enter(Player player)
    {
        isTookHit = false;
        isDead = false;
    }

    public void Execute()
    {
        if (!isTookHit)
        {
            isTookHit = true;

            Player.Instance.SetIndexes();
            Player.Instance.myArmature.animation.FadeIn("takehit", -1, 1);
            Player.Instance.SetIndexes();
        }
        if (Player.Instance.myArmature.animation.isCompleted && !isDead)
        {
            isDead = true;
            Player.Instance.InstantiateDeathParticles();            
            Player.Instance.myRigidbody.bodyType = RigidbodyType2D.Kinematic;
            Player.Instance.GetComponent<CapsuleCollider2D>().enabled = false;
            foreach (MeshRenderer sprite in Player.Instance.meshRenderer)
            {
                sprite.enabled = false;
            }
        }
    }

    public void Exit()
    {
        Player.Instance.takeHit = false;
    }
}
