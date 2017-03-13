using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : InteractiveObject
{
	public override void Start ()
    {
        base.Start();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
            Player.Instance.collectables++;
            animator.SetTrigger("collected");
        }
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);   
    }

    public void PlaySound()
    {
        SoundManager.PlaySound("key_collect");
    }
}
