using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeFX : MonoBehaviour
{
    private static MakeFX instance;

    public static MakeFX Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<MakeFX>();
            return instance;
        }
    }

    [SerializeField]
    GameObject dust;

	[SerializeField]
    GameObject death;

    [SerializeField]
    GameObject heal;

    [SerializeField]
    GameObject damageBonus;

    [SerializeField]
    GameObject immortalBonus;

    [SerializeField]
    GameObject timeBonus;

    [SerializeField]
    GameObject speedBonus;

    [SerializeField]
    GameObject jumpBonus;


    public void MakeDust()
    {
        Instantiate(dust, transform.localPosition + new Vector3(0, -0.7f, 0), Quaternion.identity);
    }

	public void MakeDeath()
	{
		Instantiate(death, transform.localPosition + new Vector3(-0.25f, 0, 0), Quaternion.identity);
	}

    public void MakeHeal()
    {
        GameObject tmp = Instantiate(heal, transform.localPosition + new Vector3(0, -0.2f, 0), Quaternion.identity);
        tmp.transform.SetParent(Player.Instance.transform);
    }

    public void MakeDamageBonus(float time)
    {
        GameObject tmp = Instantiate(damageBonus, transform.localPosition + new Vector3(0, -0.7f, 1), Quaternion.Euler(-90,0,0));
        ParticleSystem ps = tmp.GetComponent<ParticleSystem>();
        var ma = ps.main;
        ma.duration = time;
        tmp.transform.SetParent(Player.Instance.transform);
    }

    public void MakeTimeBonus(float time)
    {
        GameObject tmp = Instantiate(timeBonus, transform.localPosition + new Vector3(0, -0.7f, 1), Quaternion.Euler(-90, 0, 0));
        ParticleSystem ps = tmp.GetComponent<ParticleSystem>();
        var ma = ps.main;
        //ma.duration = time;
        tmp.transform.SetParent(Player.Instance.transform);
    }

    public void MakeImmortalBonus(float time)
    {
        GameObject tmp = Instantiate(immortalBonus, transform.localPosition + new Vector3(0, -0.7f, 1), Quaternion.Euler(-90, 0, 0));
        ParticleSystem ps = tmp.GetComponent<ParticleSystem>();
        var ma = ps.main;
        ma.duration = time;
        tmp.transform.SetParent(Player.Instance.transform);
    }

    public void MakeSpeedBonus(float time)
    {
        GameObject tmp = Instantiate(speedBonus, transform.localPosition + new Vector3(0, -0.7f, 1), Quaternion.Euler(-90, 0, 0));
        ParticleSystem ps = tmp.GetComponent<ParticleSystem>();
        var ma = ps.main;
        ma.duration = time;
        tmp.transform.SetParent(Player.Instance.transform);
    }

    public void MakeJumpBonus(float time)
    {
        GameObject tmp = Instantiate(jumpBonus, transform.localPosition + new Vector3(0, -0.7f, 1), Quaternion.Euler(-90, 0, 0));
        ParticleSystem ps = tmp.GetComponent<ParticleSystem>();
        var ma = ps.main;
        ma.duration = time;
        tmp.transform.SetParent(Player.Instance.transform);
    }


}
