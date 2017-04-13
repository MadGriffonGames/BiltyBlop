using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : InteractiveObject
{
    [SerializeField]
    SpriteRenderer mainCoin;
    SpriteRenderer MySR;
    bool collected = false;

    public override void Start ()
    {
        base.Start();
        MySR = GetComponent<SpriteRenderer>();
        enabled = false;
        MyAnimator.enabled = false;
        if (mainCoin == null)
        {
            mainCoin = GameObject.FindGameObjectWithTag("MainCoin").GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (!collected)
        {
            MySR.sprite = mainCoin.sprite;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
            collected = true;
            MyAnimator.enabled = true;
            MyAnimator.SetTrigger("collected");
            GameManager.CollectedCoins++;
            GameManager.lvlCollectedCoins++;
            SoundManager.PlaySound ("coin_collect");
        }   
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
