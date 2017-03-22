using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMeleeEnemy : Enemy
{
    [SerializeField]
    public float movementSpeed = 3.0f;

    [SerializeField]
    private EdgeCollider2D attackCollider;

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    public float patrolDuration;

    [SerializeField]
    public float idleDuration;

    public override void Start()
    {
        base.Start();
    }

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)//if enemy has a target
            {
                //return distance between enemy and target <= meleeRange (true or false)
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Move()
    {
        if (!Attack)
        {
            MyAniamtor.SetFloat("speed", 1);
            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
    }

    public void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;//xDir shows target from left or right side 
            if ((xDir < 0 && facingRight) || (xDir > 0 && !facingRight))
            {
                ChangeDirection();
            }
        }
    }

    public EdgeCollider2D AttackCollider
    {
        get
        {
            return attackCollider;
        }
    }

    public void MeleeAttack()
    {
        AttackCollider.enabled = true;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.tag == "Edge")
        {
            ChangeDirection();
        }
    }

    public override IEnumerator TakeDamage() { yield return null; }
}
