using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTutorial : MonoBehaviour {

	[SerializeField]
	GameObject mainWindow;
	[SerializeField]
	GameObject leftSideWindow;

	[SerializeField]
	GameObject skinsPanel;
	[SerializeField]
	SkinSwipeMenu skinSwipeMenu;
	[SerializeField]
	ShopController shopController;

	[SerializeField]
	Text text;
	[SerializeField]
	RectTransform arrow;
	[SerializeField]
	RectTransform[] shopButtonsTransform;
	[SerializeField]
	GameObject firstActiveShop;
	[SerializeField]
	GameObject frontPlan;
	[SerializeField]
	GameObject fade;


	GameObject tmpCard;

	public SkinSwipeMenu skinShop;

	[SerializeField]
	Button[] shopButtons;

	int currentShop = 0;

	void Start () 
	{
		Tutorial ();
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

	public void MainMenuSkip()
	{
		currentShop++;
		SetArrowPosition ();
		ActivateShopButton (currentShop);
	}

	private void ResetMainWindow ()
	{
		mainWindow.SetActive (true);
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
		mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipGreeting ());
	}

	private void ToFrontPlan(GameObject parentVar,GameObject targetObj)
	{
//		parentVar = targetObj.gameObject.transform.parent.gameObject;
		targetObj.gameObject.transform.SetParent (frontPlan.transform);
		targetObj.transform.localScale = new Vector3 (1.3f, 1.3f, 1);
	}
	private void ToBasePlan(GameObject parentVar,GameObject targetObj)
	{
		targetObj.gameObject.transform.SetParent (parentVar.transform);
		targetObj.transform.localScale = new Vector3 (1f, 1f, 1);
	}
		

	/*
	 * Skip Methods
	*/

	public void SkipGreeting()
	{
		mainWindow.SetActive (false);
		leftSideWindow.SetActive (true);
		leftSideWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		leftSideWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipSkins());
		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "This is Skins";
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 210);
		tmpCard = skinsPanel.transform.GetChild (1).gameObject;
		tmpCard.GetComponentsInChildren<Button> () [0].onClick.RemoveAllListeners ();
		tmpCard.GetComponentsInChildren<Button> () [1].onClick.RemoveAllListeners ();
		ToFrontPlan (skinsPanel, tmpCard);

		arrow.gameObject.SetActive (false);
	}

	public void SkipSkins()
	{
		leftSideWindow.SetActive (false);
		mainWindow.SetActive (true);
		mainWindow.GetComponentsInChildren<Text>()[0].text = "Now Swords";
		mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipSwordsPreview ());
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 150);
		ToBasePlan (skinsPanel, tmpCard);
		skinSwipeMenu.UpdateSkinCards ();

		currentShop++;
		SetArrowPosition ();
		arrow.gameObject.SetActive (true);
	}

	public void SkipSwordsPreview()
	{
		shopController.ActivateShop (currentShop);
		mainWindow.SetActive (false);
		leftSideWindow.SetActive (true);
		leftSideWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		leftSideWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipSwords());
		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "This is swords";
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 0);
	}

	public void SkipSwords()
	{
		leftSideWindow.SetActive (false);
		mainWindow.SetActive (true);
		mainWindow.GetComponentsInChildren<Text>()[0].text = "Now Swords";
		mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipItemsPreview ());
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 150);

		currentShop++;
		SetArrowPosition ();
		arrow.gameObject.SetActive (true);
	}
	public void SkipItemsPreview()
	{
		CloseTutorial ();
	}


	public void CloseTutorial()
	{
		this.gameObject.SetActive (false);
	}
}
