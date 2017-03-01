using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : InteractiveObject {

    public override void Start()
    {
        base.Start();
    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && player.Health < 3 && other.gameObject.tag != "Sword")
        {
			SoundManager.PlaySound ("heart_collect");
            animator.SetTrigger("collected");
            player.Health ++;
        }
    }

    public void DestroyObject ()
    {
        Destroy(this.gameObject);
    }
}
