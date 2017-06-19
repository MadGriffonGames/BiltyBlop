using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

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

    public abstract IEnumerator TakeDamage();

    public bool TakingDamage { get; set; }

    public UnityArmatureComponent myArmature;

    [SerializeField]
    protected int health;

    public abstract bool IsDead { get; }

    [SerializeField]
    private PolygonCollider2D attackCollider;

    public PolygonCollider2D AttackCollider
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

    Dictionary<string, GameObject> skins;

    public virtual void Start ()
    {
        facingRight = true;

        skins = new Dictionary<string, GameObject>();
        foreach (GameObject skin in SkinManager.Instance.skinPrefabs)
        {
            skins[skin.name] = skin;
        }

        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        Destroy(myArmature.gameObject);
        string skinName = PlayerPrefs.GetString("Skin", "Classic");
        if (skins.ContainsKey(skinName))
        {
            GameObject skinPrefab = Instantiate(skins[skinName], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
            skinPrefab.transform.localScale = new Vector3(1, 1, 1);
            //getting component here, cuz if you try to get armature outside "if statement" you get "old" component(i don't know why :) )
            myArmature = skinPrefab.GetComponent<UnityArmatureComponent>();
        }
        else
        {
            GameObject skinPrefab = Instantiate(skins["Classic"], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
            skinPrefab.transform.localScale = new Vector3(1, 1, 1);
            //getting component here, cuz if you try to get armature outside "if statement" you get "old" component(i don't know why :) )
            myArmature = skinPrefab.GetComponent<UnityArmatureComponent>();
        }
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
