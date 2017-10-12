using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTutorial : MonoBehaviour {

	[SerializeField]
	GameObject hintWindow;
	[SerializeField]
	Text text;
	[SerializeField]
	RectTransform arrow;
	[SerializeField]
	RectTransform[] shopButtonsTransform;
	[SerializeField]
	GameObject firstActiveShop;

	[SerializeField]
	Button[] shopButtons;

	int currentShop = 0;

	void Start () 
	{
		Tutorial ();
		//firstActiveShop.SetActive (false);
	}

	void Update () 
	{
		
	}

	public void SetTutorialText(string text)
	{
		
	}

	private void SetArrowPosition()
	{
		arrow.position = shopButtonsTransform [currentShop].position;
	}

	public void SkipButton()
	{
		currentShop++;
		SetArrowPosition ();
	}

	public void ActivateShopButton(int buttonNumber)
	{
		for (int i = 0; i < shopButtons.Length; i++) 
		{
			if (i == buttonNumber)
				shopButtons [i].gameObject.SetActive (true);
			else
				shopButtons [i].gameObject.SetActive (false);
		}
	}

	private void Tutorial()
	{
		ActivateShopButton (0);
	}
	public void ActivateSkinsTutorial()
	{
		hintWindow.SetActive (false);
	}
}
