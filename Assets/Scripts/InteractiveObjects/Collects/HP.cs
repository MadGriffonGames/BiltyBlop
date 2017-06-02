using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : InteractiveObject
{
    bool collected = false;

    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!collected && other.transform.CompareTag("Player") && Player.Instance.Health < Player.Instance.maxHealth && !other.transform.CompareTag("Sword"))
        {
            collected = true;
            SoundManager.PlaySound ("heart_collect");
            MyAnimator.SetTrigger("collected");
            Player.Instance.Health++;
            MakeFX.Instance.MakeHeal();
            HealthUI.Instance.SetHealthbarUp();
        }
    }

    public void DestroyObject ()
    {
        Destroy(this.gameObject);
    }
}
