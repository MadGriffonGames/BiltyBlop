using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBossFireball : MageFireball
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.InvertControls();
        }
        base.OnTriggerEnter2D(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.InvertControls();
        }
        base.OnCollisionEnter2D(other);
    }
}
