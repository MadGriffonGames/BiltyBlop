using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	[SerializeField]
	Text throwText;
	[SerializeField]
	GameObject throwImage;

	private bool onStart = true;

	private void Start()
	{
		
	}

    public void SetThrowBar()

    {
		if (onStart) 
		{			
			throwText.text = "x " + Player.Instance.maxClipSize.ToString();
			throwImage.GetComponent<Image> ().sprite = Resources.Load<GameObject> ("Throwing/ThrowingObject").GetComponent<SpriteRenderer> ().sprite;
			throwBar = new GameObject[Player.Instance.maxClipSize];
			for (int i = 0; i < Player.Instance.maxClipSize; i++) 
			{
				GameObject throwingKnife = Instantiate (throwingObject) as GameObject;


				throwingKnife.transform.SetParent (this.transform);
				throwingKnife.transform.localScale = new Vector3 (1, 1, 1);
				throwBar [i] = throwingKnife;
			}
			onStart = false;
			
		}

		throwText.text = "x " + (Player.Instance.throwingIterator + 1).ToString();
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

	public void SetItems()
	{
		
	}
}
