using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : InteractiveObject
{

    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && Player.Instance.Health < 3 && !other.transform.CompareTag("Sword"))
        {
			SoundManager.PlaySound ("heart_collect");
            animator.SetTrigger("collected");
            Player.Instance.Health++;
            MakeFX.Instance.MakeHeal();
        }
    }

    public void DestroyObject ()
    {
        Destroy(this.gameObject);
    }
}
