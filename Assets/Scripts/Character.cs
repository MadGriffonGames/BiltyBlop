using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract IEnumerator TakeDamage();

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

    public abstract bool IsDead { get; }

    [SerializeField]
    private EdgeCollider2D attackCollider;

    public EdgeCollider2D AttackCollider
    {
        get
        {
            return attackCollider;
        }
    }

    [SerializeField]
    public List<string> damageSources;

    [SerializeField]
    protected float movementSpeed = 3.0f;

    protected bool facingRight;//chek direction(true if we look right)

    public bool Attack { get; set; }

    // Use this for initialization
    public virtual void Start ()
    {
        facingRight = true;
        MyAniamtor = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void MeleeAttack()
    {
        AttackCollider.enabled = true;
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
