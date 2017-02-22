using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : InteractiveObject
{

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.CheckpointPosition = this.gameObject.transform.localPosition;
        }
    }
}
