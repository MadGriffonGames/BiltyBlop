using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : IPlayerState
{
    public void Enter(Player player)
    {
        player.InstantiateDeathParticles();
        player.InstantiateGrave();
        player.myRigidbody.bodyType = RigidbodyType2D.Kinematic;
        //player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        foreach (MeshRenderer sprite in player.meshRenderer)
        {
            sprite.enabled = false;
        }
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        Player.Instance.takeHit = false;
    }
}
