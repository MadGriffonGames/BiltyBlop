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
    public float movementSpeed;
    [SerializeField]
    public GameObject[] spiderWall = new GameObject[3];
    [SerializeField]
    public GameObject lazer;
    [SerializeField]
    public GameObject column1;
    [SerializeField]
    public GameObject column2;
    [SerializeField]
    public GameObject rightEdge;
    [SerializeField]
    public Collider2D enemyDamageRoll;
    [SerializeField]
    public Collider2D enemyDamageStone;
    [SerializeField]
    public GameObject cloudParticle;
    [SerializeField]
    public GameObject levelEnd;
    [SerializeField]
    public GameObject stun;


    [SerializeField]
    public GameObject groundParticles;
    [SerializeField]
    public GameObject rollGroundParticles;

    [SerializeField]
    GameObject[] platforms = new GameObject[3];

    [SerializeField]
    public GameObject shadow;
    bool isDead = false;



    [SerializeField]
    public GameObject[] spiderStones = new GameObject[20];



    int maxHealth;
    float firstHBScaleX;
    public bool facingRight;


    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spiderWall[0].GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spiderWall[1].GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spiderWall[2].GetComponent<Collider2D>(), true);
        maxHealth = Health;
        firstHBScaleX = healthbar.localScale.x;
        gameObject.SetActive(false);
    }

    public override void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spiderWall[0].GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spiderWall[1].GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), spiderWall[2].GetComponent<Collider2D>(), true);
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
        bossUI.SetActive(true);
        ChangeState(new SpiderEnterState());
        column1.SetActive(true);
        column2.SetActive(true);
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
        CameraEffect.Shake(0.2f, 0.1f);

        int dmg;

        if (damageSource == "Sword")
        {
            dmg = Player.Instance.meleeDamage;
        }
        else
        {
            dmg = Player.Instance.throwDamage;
        }

        for (int i = 1; i <= dmg; i++)
        {
            SetHealthbar();
        }

        if (health <= 0)
        {
            isDead = true;
            this.ChangeState(new SpiderDeathState());
        }
        yield return null;
    }

    public void SetHealthbar()
    {
        if (Health > 0)
        {
            healthbar.localScale = new Vector3(healthbar.localScale.x - firstHBScaleX / maxHealth,
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

    public void WallFall(int i)
    {
        if (this.gameObject.transform.localScale.x > 1)
        {
            GameObject tmp = (GameObject)Instantiate(spiderWall[i], this.gameObject.transform.position + new Vector3(6.5f, 10.8f), Quaternion.identity);
            SoundManager.PlaySound("door crash");
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(spiderWall[i], this.gameObject.transform.position + new Vector3(-6.5f, 10.8f), Quaternion.identity);
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
                GameObject tmp = (GameObject)Instantiate(platforms[i], this.gameObject.transform.position + new Vector3((8 + j * i), 10.8f), Quaternion.identity);
                j++;
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(platforms[i], this.gameObject.transform.position + new Vector3(-(8 + j * i), 10.8f), Quaternion.identity);
                j++;
            }
        }
    }
}
