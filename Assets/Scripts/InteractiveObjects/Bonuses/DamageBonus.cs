using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBonus : Bonus
{
    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
            Player.Instance.ExecBonusDamage(duration);
            animator.SetBool("collected", true);
            SoundManager.PlaySound("key_collect");
        }
    }

    public void Destroy()
    {
        if (reset)
        {
            animator.enabled = false;
            spriteRenderer.enabled = false;
            StartCoroutine(Reset());
        }
        else Destroy(this.gameObject);
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(10);
        animator.Play("BonusIdle");
        animator.enabled = true;
        spriteRenderer.enabled = true;
    }
}
