using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public UnityArmatureComponent armature;

    //[SerializeField]
    //protected GameObject[] healthbar;

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

    public bool Attack { get; set; }

    [SerializeField]
    public float speed;

    public virtual void Start()
    {
        MyAniamtor = GetComponent<Animator>();
        armature = GetComponent<UnityArmatureComponent>();
        //healthbar[Health - 1].SetActive(true);
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

    //public void SetHealthbar()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (i + 1 == health)
    //        {
    //            healthbar[i].SetActive(true);
    //        }
    //        else
    //        {
    //            healthbar[i].SetActive(false);
    //        }
    //    }
    //}
}
