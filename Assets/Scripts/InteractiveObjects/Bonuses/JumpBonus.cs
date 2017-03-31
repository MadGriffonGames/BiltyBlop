using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBonus : Bonus
{
    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
            Player.Instance.ExecBonusJump(duration);
            MyAnimator.SetTrigger("collected");
            SoundManager.PlaySound("key_collect");
        }
    }

    public void Destroy()
    {
        if (reset)
        {
            MyAnimator.enabled = false;
            spriteRenderer.enabled = false;
            StartCoroutine(Reset());
        }
        else Destroy(this.gameObject);
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(10);
        MyAnimator.Play("BonusIdle");
        MyAnimator.enabled = true;
        spriteRenderer.enabled = true;
    }
}
