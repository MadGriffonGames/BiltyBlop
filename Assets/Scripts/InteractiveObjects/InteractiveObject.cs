using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public Animator MyAnimator { get; private set; }

    public virtual void Start ()
    {
        MyAnimator = GetComponent<Animator>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }
}
