using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsSwipeMenu : SwipeMenu
{
	public GameObject gemItem;

	[SerializeField]
	public GameObject errorWindow;
	[SerializeField]
	GameObject fade;
	[SerializeField]
	GameObject closeWindowButton;
	[SerializeField]
	GameObject buyGemWindow;

	public override void Start()
	{
		buttons = new GameObject[panel.transform.childCount];
		distance = new float[buttons.Length];
		for (int i = 0; i < buttons.Length; i++) 
		{
			buttons [i] = panel.GetChild (i).gameObject;	
		}
		SetGemCards ();
		minButtonsNumber = 1;
		base.Start ();
		buttonDistance = (int)DISTANCE;
		onStart = true;
	}

	private void SetGemCards()
	{
		for (int i = 0; i < buttons.Length; i++) 
		{
			GameObject newItem = buttons [i];
			newItem.transform.SetParent (panel, true);

			newItem.transform.localPosition = new Vector3(i * DISTANCE, 0, 0);
			newItem.transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	public override void Update()
	{
		base.Update ();
	}

	public void ActivateBuyGemWindow(string gemID)
	{
		closeWindowButton.SetActive(true);
		fade.SetActive(true);
		buyGemWindow.SetActive(true);
		buyGemWindow.GetComponent<BuyGemWindow>().SetBuyGemWindow(gemID);
	}

	public void ShowErrorWindow(string error)
	{
		fade.SetActive(true);
		errorWindow.SetActive(true);
		closeWindowButton.SetActive(true);
		errorWindow.GetComponentInChildren<Text>().text = error;
	}

	public void CloseBuyGemWindow()
	{
		fade.SetActive(false);
		buyGemWindow.SetActive(false);
		closeWindowButton.SetActive(false);
	}
}
