using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    public bool reset;
    [SerializeField]
    public float duration;
    public Animator MyAnimator { get; private set; }
    public SpriteRenderer spriteRenderer;

    public virtual void Start()
    {
        MyAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyAnimator.enabled = false;
    }

    private void OnBecameVisible()
    {
        MyAnimator.enabled = true;
    }
}
