using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTutorial : MonoBehaviour
{

	[SerializeField]
	GameObject mainWindow;
	[SerializeField]
	GameObject leftSideWindow;

	// PANELS
	[SerializeField]
	GameObject skinsPanel;
	[SerializeField]
	GameObject swordsPanel;
	[SerializeField]
	GameObject itemsPanel;
	[SerializeField]
	GameObject perkPanel;
	[SerializeField]
	GameObject throwPanel;
	[SerializeField]
	GameObject gemsPanel;

	// SWIPE MENUES;
	[SerializeField]
	SkinSwipeMenu skinSwipeMenu;
	[SerializeField]
	SwordsSwipeMenu swordSwipeMenu;
	[SerializeField]
	ItemsSwipeMenu itemSwipeMenu;
	[SerializeField]
	ThrowingSwipeMenu throwSwipeMenu;
	[SerializeField]
	PerksSwipeMenu perkSwipeMenu;
	[SerializeField]
	GemsSwipeMenu gemSwipeMenu;

	// Buy Item Window fields
	[SerializeField]
	GameObject buyItemMenu;
	[SerializeField]
	GameObject closeByuItemMenuButton1;
	[SerializeField]
	GameObject closeByuItemMenuButton2;

	[SerializeField]
	GameObject buyByCoinsButton;
	[SerializeField]
	GameObject buyByCrystalsButton;
	[SerializeField]
	GameObject itemsFade;

	[SerializeField]
	ShopController shopController;

	[SerializeField]
	Text text;
	[SerializeField]
	RectTransform topArrow;
	[SerializeField]
	GameObject cardArrow;
	[SerializeField]
	RectTransform[] shopButtonsTransform;
	[SerializeField]
	GameObject firstActiveShop;
	[SerializeField]
	GameObject frontPlan;
	[SerializeField]
	GameObject fade;

	[SerializeField]
	Sprite nextButton;

	GameObject tmpCard;
	Button tmpButton;

	[SerializeField]
	Button[] shopButtons;

    Text leftSideWindowText;
    Text mainWindowText;

	private static Vector3 arrowPosition1 = new Vector3(13, 112, 0);  // perks arrow
	private static Vector3 arrowPosition2 = new Vector3(8, -98, 0); // stats arrow
	private static Vector3 arrowPosition3 = new Vector3(8, 155, 0); // gems arrow
	private static Vector3 arrowPosition4 = new Vector3(-128, -123, 0);

	private static Vector3 arrowRotation1 = new Vector3(0, 0, 45);
	private static Vector3 arrowRotation2 = new Vector3(0, 0, 135);
	int currentShop = 0;

	void Start () 
	{
		topArrow.gameObject.SetActive (false);
		cardArrow.SetActive (false);
        
        if (PlayerPrefs.GetInt ("ShopTutorialComplete") == 0 && PlayerPrefs.GetInt ("TutorialMode") > 0) 
		{
			Tutorial ();			
		}
        else 
		{
			shopController.ActivateShop (0);
			this.gameObject.SetActive (false);
		}
        leftSideWindow.SetActive(true);
        leftSideWindowText = leftSideWindow.GetComponentsInChildren<Text>()[0];
        leftSideWindow.SetActive(false);
        
        mainWindowText = mainWindow.GetComponentsInChildren<Text>()[0];   
    }

	private void SetArrowPosition()
	{
		topArrow.position = shopButtonsTransform [currentShop].position;
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

	private void CardToFrontPlan(GameObject parentVar, int number)
	{
		tmpCard = GameObject.Instantiate(parentVar.transform.GetChild (number).gameObject) as GameObject;
		tmpCard.GetComponentsInChildren<Button> () [0].onClick.RemoveAllListeners ();
		tmpCard.GetComponentsInChildren<Button> () [1].onClick.RemoveAllListeners ();
		tmpCard.gameObject.transform.SetParent (frontPlan.transform);
		tmpCard.transform.localScale = new Vector3 (1.3f, 1.3f, 1);
		tmpCard.transform.localPosition = new Vector3 (0, 0, 0);
		tmpButton = tmpCard.GetComponentsInChildren<Button> () [1];
		tmpButton.gameObject.GetComponent<Image> ().sprite = nextButton;
		tmpButton.GetComponentInChildren<Text>().text = "next";
		LocalizationManager.Instance.UpdateLocaliztion (tmpButton.GetComponentInChildren<Text> ());
	}

	private void CardToBasePlan(GameObject parentVar,GameObject targetObj)
	{
		Destroy (tmpCard);
	}
		

	/*
	 * Skip Methods
	*/

	// MAIN WINDOW  // setting SKINS
	public void SkipGreeting()
	{
		shopController.ActivateShop (0);
		mainWindow.SetActive (false);
		leftSideWindow.SetActive (true);

		leftSideWindowText.text = "maximum health points";

        LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 210);
		CardToFrontPlan (skinsPanel, 7);
		tmpButton.onClick.AddListener(() => SkipSkins());

		tmpCard.GetComponentInChildren<SkinStatsPanel> ().HighliteStat ();
		cardArrow.SetActive (true);
		cardArrow.transform.localPosition = arrowPosition2;

	}

	// SKINS WINDOW // SETTING SWORDS
	public void SkipSkins()
	{
		CardToBasePlan (skinsPanel, tmpCard);
		skinSwipeMenu.UpdateSkinCards ();
		currentShop++;
		shopController.ActivateShop (currentShop);

		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "your attack damage";

		LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
		CardToFrontPlan (swordsPanel, 3);
		tmpButton.onClick.AddListener(() => SkipSwords());

		tmpCard.GetComponentInChildren<SkinStatsPanel> ().HighliteStat ();
		cardArrow.transform.localPosition = arrowPosition2;

	}

	// SWORDS WINDOW // SETTING MAIN
	public void SkipSwords()
	{
		CardToBasePlan (swordsPanel, tmpCard);
		currentShop++;
		shopController.ActivateShop (currentShop);

		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "get more potions";

		LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
		CardToFrontPlan (itemsPanel,2);
		tmpButton.onClick.AddListener(() => SkipItems());

		tmpCard.GetComponentInChildren<SkinStatsPanel> ().HighliteStat ();
		cardArrow.transform.localPosition = arrowPosition2;

	}

	// ITEMS WINDOW // SETTING Perks
	public void SkipItems()
	{
		CardToBasePlan (itemsPanel, tmpCard);
		currentShop++;
		shopController.ActivateShop (currentShop);

		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "get new skills";

		LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
		CardToFrontPlan (perkPanel,2);
		tmpButton.onClick.AddListener(() => SkipPerks());

		cardArrow.transform.localPosition = arrowPosition1;
		cardArrow.transform.localRotation = Quaternion.Euler(arrowRotation1);
	}


	// PERK WINDOW // SETTING Throw
	public void SkipPerks()
	{
		CardToBasePlan (perkPanel, tmpCard);
		currentShop++;
		shopController.ActivateShop (currentShop);

		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "throw weapon damage";

		LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
		CardToFrontPlan (throwPanel, 11);
		tmpButton.onClick.AddListener(SkipThrow);

		tmpCard.GetComponentInChildren<SkinStatsPanel> ().HighliteStat ();
		cardArrow.transform.localPosition = arrowPosition2;
		cardArrow.transform.localRotation = Quaternion.Euler(arrowRotation2);

	}

	// THROW WINDOW // SETTING GEMS
	public void SkipThrow()
	{
		CardToBasePlan (throwPanel, tmpCard);
		currentShop++;
		shopController.ActivateShop (currentShop);

		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "earn more crystals";

		LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
		CardToFrontPlan (gemsPanel, 1);
		tmpButton.onClick.AddListener(SkipGems);

		cardArrow.transform.localPosition = arrowPosition3;
		cardArrow.transform.localRotation = Quaternion.Euler(arrowRotation1);
	}


	// GEMS WINDOW // setting HP Potion
	public void SkipGems()
	{
		CardToBasePlan (gemsPanel, tmpCard);
		currentShop = 2;
		shopController.ActivateShop (currentShop);
		mainWindow.SetActive (false);
		topArrow.gameObject.SetActive (false);

		leftSideWindow.SetActive (true);
		leftSideWindow.transform.localPosition = new Vector3 (85, 0, 0);

		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "now get some potions for free!";
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 200);

		LocalizationManager.Instance.UpdateLocaliztion (leftSideWindow.GetComponentsInChildren<Text>()[0]);
		cardArrow.SetActive (true);
		cardArrow.transform.localPosition = arrowPosition4;
		cardArrow.transform.localRotation = Quaternion.Euler (arrowRotation2);

		tmpCard = GameObject.Instantiate(itemsPanel.transform.GetChild (0).gameObject) as GameObject;
		tmpCard.transform.transform.position = itemsPanel.transform.GetChild (0).gameObject.transform.position;
		tmpCard.gameObject.transform.SetParent (frontPlan.transform);
		tmpCard.transform.localScale = new Vector3 (1.05f, 1.05f, 1);

		tmpCard.GetComponentsInChildren<Button> () [0].onClick.AddListener(() => itemSwipeMenu.ActivateBuyItemWindow("HealthPot"));
		tmpCard.GetComponentsInChildren<Button> () [1].onClick.AddListener(() => itemSwipeMenu.ActivateBuyItemWindow("HealthPot"));

		tmpCard.GetComponentsInChildren<Button> () [0].onClick.AddListener(() => BuyItemWindowMenu());
		tmpCard.GetComponentsInChildren<Button> () [1].onClick.AddListener(() => BuyItemWindowMenu());

		tmpCard.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text>().text = "free shop";
		LocalizationManager.Instance.UpdateLocaliztion (tmpCard.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ());
	}

	public void BuyItemWindowMenu()
	{
		Destroy (tmpCard);
		cardArrow.SetActive (false);
		buyItemMenu.transform.SetParent (frontPlan.transform);
		leftSideWindow.SetActive (false);
		closeByuItemMenuButton1.SetActive (false);
		closeByuItemMenuButton2.SetActive (false);

		buyByCrystalsButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
		buyByCrystalsButton.GetComponent<Image> ().color = new Color32 (147,147,147,255);
		buyByCrystalsButton.GetComponentInChildren<Text> ().color = new Color32 (147,147,147,255);

		buyByCoinsButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
		buyByCoinsButton.GetComponentInChildren<Text>().text = "0";
		LocalizationManager.Instance.UpdateLocaliztion (buyByCoinsButton.GetComponentInChildren<Text>());

		buyByCoinsButton.GetComponent<Button> ().onClick.AddListener (() => Inventory.Instance.BuyItem("HealthPot", 3, "Free", Inventory.Instance.GetCoinCost("HealthPot")));
		buyByCoinsButton.GetComponent<Button> ().onClick.AddListener (() => EndOfTutorial());

	}

	public void EndOfTutorial()
	{
		buyByCrystalsButton.GetComponent<Image> ().color = new Color32 (255,255,255,255);
		buyByCrystalsButton.GetComponentInChildren<Text> ().color = new Color32 (255,255,255,255);
		buyByCoinsButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
		buyByCrystalsButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
		closeByuItemMenuButton1.SetActive (true);
		closeByuItemMenuButton2.SetActive (true);

		buyItemMenu.transform.SetParent (itemSwipeMenu.gameObject.transform);
		buyItemMenu.SetActive (false);
		itemsFade.SetActive (false);
		mainWindow.SetActive (true);

		//mainWindow.GetComponentsInChildren<Text>()[0].text = "Congratulations! You get 3 Free Health potions! Good luck! See you soon Warrior!";
		mainWindow.GetComponentsInChildren<Text>()[0].text = "Congratulations! You get 3 Free Health potions! Good luck! See you soon Warrior!";

        LocalizationManager.Instance.UpdateLocaliztion(mainWindowText);
        mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentsInChildren<Text>()[1].text = "Close";
        LocalizationManager.Instance.UpdateLocaliztion(mainWindow.GetComponentsInChildren<Text>()[1]);
        mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => CloseTutorial());

	}

	public void CloseTutorial()
	{
		PlayerPrefs.SetInt ("ShopTutorialComplete", 1);
		DevToDev.Analytics.Tutorial(5);
		this.gameObject.SetActive (false);
    }
}
