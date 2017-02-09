using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionObject : InteractiveObject
{
    [SerializeField]
    private HP hp;

    [SerializeField]
    private Coin coin;

    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword")
        {
            //animator.SetTrigger("collected");
            if (Random.Range(1, 100) < 80)//spawner of hearts or coins
                Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
            else
                Instantiate(hp, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
