using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject Target { get; set; }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    public override IEnumerator TakeDamage() { yield return null; }
}
