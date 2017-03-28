using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    public bool reset;
    [SerializeField]
    public float duration;
    public Animator animator { get; private set; }
    public SpriteRenderer spriteRenderer;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
