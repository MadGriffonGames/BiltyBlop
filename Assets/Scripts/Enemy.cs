using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField]
    public float idleDuration;

    [SerializeField]
    private float meleeRange;

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

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    [SerializeField]
    private GameObject leafParticle;

    public override void Start ()
    {
        base.Start();
        ChangeState(new IdleState());
	}

	void Update ()
    {
        if (!IsDead)
        {
            if (!TakingDamage && !Attack)
            {
                currentState.Execute();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

   public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D (other);
        currentState.OnTriggerEnter2D (other);
    }

    public override IEnumerator TakeDamage()
    {
        health -= 1;
        if (!IsDead)
            MyAniamtor.SetTrigger("damage");
        else
        {
            MyAniamtor.SetTrigger("death");
            Instantiate(leafParticle, transform.localPosition + new Vector3(-0.4f, 0, 0), Quaternion.identity);
            Destroy(gameObject);
        }
        yield return null;
    }
}
