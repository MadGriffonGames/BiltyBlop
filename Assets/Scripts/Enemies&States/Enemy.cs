using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Target { get; set; }

    [SerializeField]
    protected GameObject[] healthbar;

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

    public bool facingRight;//check direction(true if we look right)

    public bool Attack { get; set; }

    // Use this for initialization
    public virtual void Start()
    {
        MyAniamtor = GetComponent<Animator>();
        healthbar[Health - 1].SetActive(true);
        facingRight = false;
        enabled = false;
        MyAniamtor.enabled = false;
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

    public void SetHealthbar()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i + 1 == health)
            {
                healthbar[i].SetActive(true);
            }
            else
            {
                healthbar[i].SetActive(false);
            }
        }
    }

    private void OnBecameVisible()
    {
        enabled = true;
        MyAniamtor.enabled = true;
    }
}
