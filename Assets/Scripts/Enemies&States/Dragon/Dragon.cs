using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Dragon : Boss
{
    private IDragonState currentState;

    [SerializeField]
    public UnityEngine.Transform behindPosRight;
    [SerializeField]
    public UnityEngine.Transform behindPosLeft;
    [SerializeField]
    public GameObject flameFlow;
    [SerializeField]
    public GameObject fallPoint;
    [SerializeField]
    public GameObject flameOffRight;
    [SerializeField]
    public GameObject flameOffLeft;
    float angle;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        ChangeState(new EnterState());
    }

    void Update()
    {
        if (!IsDead)
        {
            currentState.Execute();
        }
    }

    public void ChangeState(IDragonState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void ChangeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public override IEnumerator TakeDamage()
    {
            health -= Player.Instance.damage;
            CameraEffect.Shake(0.2f, 0.3f);
            //SetHealthbar();
            if (IsDead)
            {
                Destroy(gameObject);
            }
        yield return new WaitForSeconds(0.05f);
    }

    public void PlayAnimation(string name)
    {
        armature.animation.Play(name);
    }

    public void Move()
    {
        angle += Time.deltaTime*5;
        angle %= 360;
        float offsetY = Mathf.Sin(angle) * 0.01f;
        transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x + 0.045f * transform.localScale.x * speed, transform.position.y + offsetY, transform.position.z), speed);
    }

}
