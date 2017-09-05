using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingUI : MonoBehaviour
{
    private static ThrowingUI instance;
    public static ThrowingUI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<ThrowingUI>();
            return instance;
        }
    }

    GameObject[] throwBar;
	[SerializeField]
	GameObject throwingObject;

    public void SetThrowBar()

    {
		throwBar = new GameObject[Player.Instance.clipSize];
		for (int i = 0; i < Player.Instance.clipSize; i++) 
		{
			GameObject throwingKnife = Instantiate (throwingObject) as GameObject;
			throwingKnife.transform.SetParent (this.transform);
			throwBar [i] = throwingKnife;
		}
        for (int i = 0; i < Player.Instance.clipSize; i++)
        {
            if (i < Player.Instance.throwingIterator + 1)
            {
                throwBar[i].SetActive(true);
            }
            else
            {
                throwBar[i].SetActive(false);
            }
        }
    }
}
