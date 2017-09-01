using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemShop : MonoBehaviour {

	public GameObject gemItem;
	public Transform spacer;

	[SerializeField]
	public GameObject errorWindow;
	[SerializeField]
	GameObject fade;
	[SerializeField]
	GameObject closeWindowButton;
	[SerializeField]
	GameObject buyGemWindow;

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

