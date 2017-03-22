﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonus : Bonus
{
    [SerializeField]
    float duration;

    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
            //animator.SetTrigger("collected");
            Player.Instance.ExecBonusTime(duration);
            animator.SetTrigger("collected");
            SoundManager.PlaySound("key_collect");
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
