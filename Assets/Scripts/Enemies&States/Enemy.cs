using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Enemy : MonoBehaviour
{
    public UnityArmatureComponent armature;

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
        armature = GetComponent<UnityArmatureComponent>();
        healthbar[Health - 1].SetActive(true);
        facingRight = false;
        enabled = false;
        MyAniamtor.enabled = false;
        armature.enabled = false;
        armature.armature.cacheFrameRate = 55;
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
        armature.enabled = true;
    }

    public void ForceActivate()
    {
        enabled = true;
        MyAniamtor.enabled = true;
        armature.enabled = true;
    }

    public IEnumerator AnimationDelay()
    {
        float tmp = armature.animation.timeScale;

        armature.animation.timeScale = 0.1f;
        yield return new WaitForSeconds(0.3f);
        armature.animation.timeScale = tmp;
        yield return null;
    }
}
