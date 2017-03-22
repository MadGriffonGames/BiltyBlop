using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBonus : Bonus
{
    [SerializeField]
    float force;
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
            Player.Instance.ExecBonusJump(duration, force);
            animator.SetTrigger("collected");
            SoundManager.PlaySound("key_collect");
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
