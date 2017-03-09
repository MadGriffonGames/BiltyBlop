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
}
