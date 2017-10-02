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
    int health;

	public bool IsDead
	{
		get { return health <= 0; }
	}

	[SerializeField]
	public UnityEngine.Transform healthbar;

	[SerializeField]
	public GameObject healthBarNew;

	[SerializeField]
	public int maxHealth;

	float firstHBScaleX;
   
    public bool isNeedDoubleDamage;

    private void Start()
	{
		health = maxHealth;
		firstHBScaleX = healthbar.localScale.x;
        SetHealthbar();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(damageType))
        {
            if (!isNeedDoubleDamage)
            {
                CheckDamageSource(other.tag);
                StartCoroutine(TakeDamage());
            }
            else
            {
                if (Player.Instance.damageBonusNum > 0)
                {
                    CheckDamageSource(other.tag);
                    StartCoroutine(TakeDamage());
                }
            }
        }
    }

    public void CheckDamageSource(string damageSourceName)
    {
        if (damageSourceName == "Sword")
        {
            health -= Player.Instance.meleeDamage;

        }
        else
        {
            health -= Player.Instance.throwDamage;
        }
    }

	public void SetHealthbar()
	{
		if (health > 0)
		{
			healthbar.localScale = new Vector3(firstHBScaleX * health / maxHealth,
				healthbar.localScale.y,
				healthbar.localScale.z);
			healthBarNew.GetComponentInChildren<TextMesh> ().text = health.ToString () + "/" + maxHealth.ToString ();
		} else
		{
			healthbar.localScale = new Vector3(0,
				healthbar.localScale.y,
				healthbar.localScale.z);
			healthBarNew.GetComponentInChildren<TextMesh> ().text = "0/" + maxHealth.ToString ();
		}
	}

    public IEnumerator TakeDamage()
    {
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
