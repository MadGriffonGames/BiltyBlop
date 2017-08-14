using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Spider : Boss
{
    private ISpiderState currentState;
    Rigidbody2D MyRigidbody;

    [SerializeField]
    public GameObject bossUI;
    [SerializeField]
    public RectTransform healthbar;
    [SerializeField]
    public float movementSpeed = 3.0f;
    [SerializeField]
    public GameObject spiderWall;
    [SerializeField]
    public GameObject lazer;

    [SerializeField]
    GameObject[] platforms = new GameObject[3];

    public bool wallDestroy = false;


    [SerializeField]
    public GameObject[] spiderStones = new GameObject[5];



    int maxHealth;
    float firstHBScaleX;
    public bool facingRight;


    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spiderWall.GetComponent<Collider2D>(), true);
        maxHealth = Health;
        firstHBScaleX = healthbar.localScale.x;
        gameObject.SetActive(false);
    }

    public override void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
        bossUI.SetActive(true);
        ChangeState(new SpiderEnterState());
    }

    public void Update()
    {
        currentState.Execute();
    }

    public void ChangeState(ISpiderState newState)
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
        if (transform.localScale.x < 1)
            facingRight = true;
        else facingRight = false;
    }

    public override IEnumerator TakeDamage()
    {
        SoundManager.PlaySound("dragon_damage");
        health -= Player.Instance.damage;
        CameraEffect.Shake(0.2f, 0.1f);
        SetHealthbar();
        yield return null;
    }

    public void SetHealthbar()
    {
        if (Health != 0)
        {
            healthbar.localScale = new Vector3(healthbar.localScale.x - firstHBScaleX * 1 / maxHealth,
                                               healthbar.localScale.y,
                                               healthbar.localScale.z);
        }
        else
        {
            bossUI.SetActive(false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(collision);
    }

    public void PlayAnimation(string name)
    {
        armature.animation.Play(name);
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void Move()
    {
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

    public void WallFall()
    {
        if (this.gameObject.transform.localScale.x > 1)
        {
            GameObject tmp = (GameObject)Instantiate(spiderWall, this.gameObject.transform.position + new Vector3(-6, 10.8f), Quaternion.identity);
            SoundManager.PlaySound("door crash");
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(spiderWall, this.gameObject.transform.position + new Vector3(6, 10.8f), Quaternion.identity);
            SoundManager.PlaySound("door crash");
        }
    }

    public void SpawnPlatforms()
    {
        int j = 2;
        for (int i = 0; i < 3; i++)
        {
            if (this.gameObject.transform.localScale.x > 1)
            {
                GameObject tmp = (GameObject)Instantiate(platforms[i], this.gameObject.transform.position + new Vector3(-6 + j * i, 10.8f), Quaternion.identity);
                j++;
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(platforms[i], this.gameObject.transform.position + new Vector3(6 + j * i, 10.8f), Quaternion.identity);
                j++;
            }
        }
    }
}
