using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : InteractiveObject
{
	public override void Start ()
    {
        base.Start();
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
            MyAnimator.SetTrigger("collected");
            GameManager.CollectedCoins++;
            GameManager.lvlCollectedCoins++;
            SoundManager.PlaySound ("coin_collect");
        }   
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
