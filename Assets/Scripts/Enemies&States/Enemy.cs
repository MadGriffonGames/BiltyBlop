using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Target { get; set; }

    public bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    public bool TakingDamage { get; set; }

    public Animator MyAniamtor { get; private set; }

    [SerializeField]
    protected int health;

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    [SerializeField]
    public List<string> damageSources;

    protected bool facingRight;//check direction(true if we look right)

    public bool Attack { get; set; }

    // Use this for initialization
    public virtual void Start()
    {
        facingRight = false;
        MyAniamtor = GetComponent<Animator>();
    }

    public virtual IEnumerator TakeDamage()
    {
        yield return null;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
}
