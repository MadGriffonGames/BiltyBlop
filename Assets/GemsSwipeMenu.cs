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

	private int[] listeners;

	public override void Start()
	{
		listeners = new int[] {3,4,2,1,0};    // соответствие карточек и их порядка в PurchaseManager
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
		//PurchaseManager purchaseManager = PurchaseManager.Instance;

		buttons[0].GetComponent<Button> ().onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [0]));
		buttons[1].GetComponent<Button> ().onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [1]));
		buttons[2].GetComponent<Button> ().onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [2]));
		buttons[3].GetComponent<Button> ().onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [3]));
		buttons[4].GetComponent<Button> ().onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [4]));

		buttons[0].GetComponentsInChildren<Button> ()[1].onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [0]));
		buttons[1].GetComponentsInChildren<Button> ()[1].onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [1]));
		buttons[2].GetComponentsInChildren<Button> ()[1].onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [2]));
		buttons[3].GetComponentsInChildren<Button> ()[1].onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [3]));
		buttons[4].GetComponentsInChildren<Button> ()[1].onClick.AddListener (() => PurchaseManager.Instance.BuyConsumable(listeners [4]));
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
