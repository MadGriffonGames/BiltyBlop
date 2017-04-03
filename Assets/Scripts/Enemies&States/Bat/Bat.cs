using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Bat : MovingMeleeEnemy
{
    IBatState currentState;
    public Vector3 nextPos;
    [SerializeField]
    public UnityEngine.Transform batTransform;
    [SerializeField]
    GameObject batParticles;
    [SerializeField]
    public UnityEngine.Transform[] pathPoints;
    public Vector3[] pathCordinates;
    public int nextPosNum = 0;
    
    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new BatPatrolState());
        pathCordinates = new Vector3[pathPoints.Length];
        int i = 0;
        foreach (var point in pathPoints)
        {
            pathCordinates[i] = pathPoints[i].localPosition;
            i++;
        }
    }

    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage && !Attack)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
    }

    public void ChangeState(IBatState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public override IEnumerator TakeDamage()
    {
        health -= Player.Instance.damage;
        CameraEffect.Shake(0.5f, 0.4f);
        if (IsDead)
        {
            Player.Instance.monstersKilled++;
            SoundManager.PlaySound("hedgehog_death");
            Instantiate(batParticles, gameObject.transform.position + new Vector3(0, 0.53f, -1f), Quaternion.identity);
            Destroy(transform.parent.gameObject);

        }
        yield return null;
    }

    public void AnimFly()
    {
        armature.animation.timeScale = 2f;
        armature.animation.Play("FLY");
    }
}
