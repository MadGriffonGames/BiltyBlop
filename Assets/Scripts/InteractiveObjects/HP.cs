using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : InteractiveObject {

    [SerializeField]
    private Player player;

    public override void Start()
    {
        base.Start();
    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && player.Health < 3 && other.gameObject.tag != "SwordCollider")
        {
            animator.SetTrigger("collected");
        }
    }

    public void DestroyObject ()
    {
        Destroy(this.gameObject);
    }
}
