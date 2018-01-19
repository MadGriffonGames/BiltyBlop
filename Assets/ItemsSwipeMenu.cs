using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsSwipeMenu : SwipeMenu {

	public GameObject item;

	[SerializeField]
	public GameObject errorWindow;
	[SerializeField]
	GameObject fade;
	[SerializeField]
	GameObject closeWindowButton;
	[SerializeField]
	GameObject buyItemWindow;

	const string itemsFolder = "Sprites/UI/InventoryUI/";
	private const float DISTANCE = 175f;

	bool onStart1;

	public override void Start ()
	{
		onStart1 = true;
        buttons = new GameObject[Inventory.Instance.itemsNames.Length];
    }

	public override void Update ()
	{
		if (onStart1 && Inventory.Instance.isActiveAndEnabled) {
			//buttons = new GameObject[Inventory.Instance.itemsNames.Length];
			SetItemCards();
			distance = new float[buttons.Length];
			for (int i = 0; i < buttons.Length; i++)
			{
				buttons[i] = panel.GetChild(i).gameObject;
			}
			buttonDistance = (int)DISTANCE;
			minButtonsNumber = 1;
			panel.anchoredPosition = new Vector2(buttons[1].transform.position.x, panel.anchoredPosition.y);
			onStart1 = false;
		}
		base.Update ();
	}  

	public void SetItemCards()
	{
		for (int i = 0; i < Inventory.Instance.itemsNames.Length; i++) 
		{
			GameObject newItem = Instantiate (item) as GameObject;
			newItem.transform.SetParent (panel, true);

			newItem.transform.localPosition = new Vector3(i * DISTANCE, 0, 0);
			newItem.transform.localScale = new Vector3 (1, 1, 1);
			newItem.GetComponentsInChildren<Text> () [0].text = Inventory.Instance.GetItemShopName (Inventory.Instance.itemsNames [i]); // shopName
			newItem.GetComponentsInChildren<Text> () [1].text = Inventory.Instance.itemsNames [i];                                     // itemName

			LocalizationManager.Instance.UpdateLocaliztion (newItem.GetComponentsInChildren<Text> () [0]); // shop name
			LocalizationManager.Instance.UpdateLocaliztion (newItem.GetComponentsInChildren<Text> () [2]); // buy button

			newItem.GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ActivateBuyItemWindow (newItem.GetComponentsInChildren<Text> () [1].text));
			newItem.GetComponentsInChildren<Button> () [1].onClick.AddListener (() => ActivateBuyItemWindow (newItem.GetComponentsInChildren<Text> () [1].text));
			newItem.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(itemsFolder + Inventory.Instance.itemsNames[i]);
			newItem.GetComponentsInChildren<Image>()[4].sprite = Resources.Load<Sprite>(itemsFolder + Inventory.Instance.itemsNames[i]);
			newItem.GetComponentInChildren<SkinStatsPanel> ().SetCoinCost (Inventory.Instance.GetCoinCost(Inventory.Instance.itemsNames[i]));
			newItem.GetComponentInChildren<SkinStatsPanel> ().ActivateCheck (false);
			buttons [i] = newItem;
		}
	}

	public void ActivateBuyItemWindow(string itemName)
	{
		closeWindowButton.SetActive(true);
		fade.SetActive(true);
		buyItemWindow.SetActive(true);
		buyItemWindow.GetComponent<BuyItemWindow>().SetBuyItemWindow(itemName);
	}

	public void ShowErrorWindow(string error)
	{
		fade.SetActive(true);
		errorWindow.SetActive(true);
		closeWindowButton.SetActive(true);
		errorWindow.GetComponentInChildren<Text>().text = error;
	}

	public void CloseBuyItemWindow()
	{
		fade.SetActive(false);
		buyItemWindow.SetActive(false);
		closeWindowButton.SetActive(false);
	}


}
