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

    public virtual IEnumerator TakeDamage() { yield return null; }

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

    [SerializeField]
    protected float movementSpeed = 3.0f;

    protected bool facingRight;//check direction(true if we look right)

    public bool Attack { get; set; }

    // Use this for initialization
    public virtual void Start()
    {
        facingRight = false;
        MyAniamtor = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
}
