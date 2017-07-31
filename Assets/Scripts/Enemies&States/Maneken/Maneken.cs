using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maneken : MonoBehaviour
{
    private IEvilFlowerState currentState;
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    string damageType;
    [SerializeField]
    GameObject[] healthbar;
    [SerializeField]
    int health;
    public bool IsDead
    {
        get { return health <= 0; }
    }
    public bool isNeedDoubleDamage;

    private void Start()
    {
        SetHealthbar();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(damageType))
        {
            if (!isNeedDoubleDamage)
            {
                StartCoroutine(TakeDamage());
            }
            else
            {
                if (Player.Instance.damageBonusNum > 0)
                {
                    StartCoroutine(TakeDamage());
                }
            }
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

    public IEnumerator TakeDamage()
    {
        health -= Player.Instance.damage;
        CameraEffect.Shake(0.2f, 0.3f);
        MakeFX.Instance.MakeHitFX(gameObject.transform.position, new Vector3(1, 1, 1));
        SetHealthbar();
        if (IsDead)
        {
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0.8f, 0, -3), Quaternion.identity);
            SoundManager.PlaySound("wooden_box1");
            gameObject.SetActive(false);
        }
        yield return null;
    }
}
