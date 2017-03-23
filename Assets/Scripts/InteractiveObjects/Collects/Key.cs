using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractiveObject
{

    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
			SoundManager.PlaySound ("key_collect");
            animator.SetTrigger("collected");
            Player.Instance.GotKey = true;
            KeyUI.Instance.KeyImage.enabled = true;
        }
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}

