using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Dragon : Boss
{
    private IDragonState currentState;
    Rigidbody2D MyRigidbody;

    [SerializeField]
    public UnityEngine.Transform behindPosRight;
    [SerializeField]
    public UnityEngine.Transform behindPosLeft;
    [SerializeField]
    public GameObject flameFlow;
    [SerializeField]
    public GameObject flameOffRight;
    [SerializeField]
    public GameObject flameOffLeft;
    [SerializeField]
    public GameObject fallPoint;
    [SerializeField]
    public GameObject risePoint;
    [SerializeField]
    public GameObject groundPoint;
    [SerializeField]
    public GameObject fireball;
    [SerializeField]
    public Collider2D takeDamageCollider;

    float angle;
    bool roar = true;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
        gameObject.SetActive(false);
    }

    public override void Start()
    {
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Roar());
    }

    void Update()
    {
        if (!roar)
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
        yield return new WaitForSeconds(0.05f);
    }

    public void PlayAnimation(string name)
    {
        armature.animation.Play(name);
    }

    public void Move(float x, float y)
    {
        MyRigidbody.velocity = new Vector2(x*transform.localScale.x/1.9f, y);
    }

    IEnumerator Roar()
    {
        //DRAGON SOUND HERE
        yield return new WaitForSeconds(1);
        roar = false;
        ChangeState(new EnterState());
    }

    public void ThrowFireball()
    {
        if (this.gameObject.transform.localScale.x > 0)
        {
            GameObject tmp = (GameObject)Instantiate(fireball, transform.position + new Vector3(-1.8f, 0.9f, -5), Quaternion.identity);
            tmp.GetComponent<FireBall>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(fireball, transform.position + new Vector3(-1.8f, 0.9f, -5), Quaternion.Euler(0, 0, 180));
            tmp.GetComponent<FireBall>().Initialize(Vector2.left);
        }
    }
}
