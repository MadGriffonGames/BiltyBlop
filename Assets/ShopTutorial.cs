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
	GameObject closeByuItemMenuButton;
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

	[SerializeField]
	Button[] shopButtons;

    Text leftSideWindowText;
    Text mainWindowText;

	int currentShop = 0;

	void Start () 
	{
		shopController.ActivateShop (0);
        if (PlayerPrefs.GetInt ("ShopTutorialComplete") == 0 && PlayerPrefs.GetInt ("TutorialMode") > 0) 
		{
			Tutorial ();			
		}
        else 
		{
			this.gameObject.SetActive (false);
		}
        leftSideWindow.SetActive(true);
        leftSideWindowText = leftSideWindow.GetComponentsInChildren<Text>()[0];
        leftSideWindow.SetActive(false);
        
        mainWindowText = mainWindow.GetComponentsInChildren<Text>()[0];   
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

	private void CardToFrontPlan(GameObject parentVar, int number)
	{
		tmpCard = GameObject.Instantiate(parentVar.transform.GetChild (number).gameObject) as GameObject;
		tmpCard.GetComponentsInChildren<Button> () [0].onClick.RemoveAllListeners ();
		tmpCard.GetComponentsInChildren<Button> () [1].onClick.RemoveAllListeners ();
		tmpCard.gameObject.transform.SetParent (frontPlan.transform);
		tmpCard.transform.localScale = new Vector3 (1.3f, 1.3f, 1);
		tmpCard.transform.localPosition = new Vector3 (0, 0, 0);
	}

	private void CardToBasePlan(GameObject parentVar,GameObject targetObj)
	{
		Destroy (tmpCard);
	}
		

	/*
	 * Skip Methods
	*/

	// MAIN WINDOW
	public void SkipGreeting()
	{
		mainWindow.SetActive (false);
		leftSideWindow.SetActive (true);
		leftSideWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		leftSideWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipSkins());
		leftSideWindowText.text = "each armor gives you different 'health' stat";
        LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 210);
		CardToFrontPlan (skinsPanel, 7);

		arrow.gameObject.SetActive (false);
	}

	// SKINS WINDOW
	public void SkipSkins()
	{
		leftSideWindow.SetActive (false);
		mainWindow.SetActive (true);
		mainWindow.GetComponentsInChildren<Text>()[0].text = "what about swords?";
        LocalizationManager.Instance.UpdateLocaliztion(mainWindowText);
        mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipSwordsPreview ());
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 150);
		CardToBasePlan (skinsPanel, tmpCard);
		skinSwipeMenu.UpdateSkinCards ();

		currentShop++;
		shopController.ActivateShop (currentShop);
		SetArrowPosition ();
		arrow.gameObject.SetActive (true);
	}

	// SWORDS PREVIEW
	public void SkipSwordsPreview()
	{
		mainWindow.SetActive (false);
		leftSideWindow.SetActive (true);
		leftSideWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		leftSideWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipSwords());
		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "Swords vary in their damage!";
        LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
        fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 210);
		CardToFrontPlan (swordsPanel, 3);

		arrow.gameObject.SetActive (false);
	}

	// SWORDS WINDOW
	public void SkipSwords()
	{
		leftSideWindow.SetActive (false);
		mainWindow.SetActive (true);
		mainWindow.GetComponentsInChildren<Text>()[0].text = "May be some items?";
        LocalizationManager.Instance.UpdateLocaliztion(mainWindowText);
        mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipItemsPreview ());
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 150);
		CardToBasePlan (swordsPanel, tmpCard);
		swordSwipeMenu.SetSwordCards ();

		currentShop++;
		shopController.ActivateShop (currentShop);
		SetArrowPosition ();
		arrow.gameObject.SetActive (true);
	}

	// ITEMS PREVIEW
	public void SkipItemsPreview()
	{
		mainWindow.SetActive (false);
		leftSideWindow.SetActive (true);
		leftSideWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		leftSideWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipItems());
		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "Potions can give you different advantages for a short time";
        LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
        fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 210);
		CardToFrontPlan (itemsPanel,2);

		arrow.gameObject.SetActive (false);
	}

	// ITEMS WINDOW
	public void SkipItems()
	{
		leftSideWindow.SetActive (false);
		mainWindow.SetActive (true);
		mainWindow.GetComponentsInChildren<Text>()[0].text = "Skills and Perks!";
        LocalizationManager.Instance.UpdateLocaliztion(mainWindowText);
        mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipPerkPreview());
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 150);
		CardToBasePlan (itemsPanel, tmpCard);
		itemSwipeMenu.SetItemCards ();

		currentShop++;
		shopController.ActivateShop (currentShop);
		SetArrowPosition ();
		arrow.gameObject.SetActive (true);
	}

	// PERKS PREVIEW
	public void SkipPerkPreview()
	{
		mainWindow.SetActive (false);
		leftSideWindow.SetActive (true);
		leftSideWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		leftSideWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipPerks());
		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "Perks give a passive advantage throughout the game";
        LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
        fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 210);
		CardToFrontPlan (perkPanel,2);

		arrow.gameObject.SetActive (false);
	}

	// PERK WINDOW
	public void SkipPerks()
	{
		leftSideWindow.SetActive (false);
		mainWindow.SetActive (true);
		mainWindow.GetComponentsInChildren<Text>()[0].text = "And now! Throwing weapons!";
        LocalizationManager.Instance.UpdateLocaliztion(mainWindowText);
        mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipThrowPreview());
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 150);
		CardToBasePlan (perkPanel, tmpCard);

		currentShop++;
		shopController.ActivateShop (currentShop);
		SetArrowPosition ();
		arrow.gameObject.SetActive (true);
	}

	// THROW PREVIEW
	public void SkipThrowPreview()
	{
		mainWindow.SetActive (false);
		leftSideWindow.SetActive (true);
		leftSideWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		leftSideWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipThrow());
		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "Throwing weapons vary in their damage as swords! Beware of Sven!";
        LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
        fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 210);
		CardToFrontPlan (throwPanel, 11);

		arrow.gameObject.SetActive (false);
	}

	public void SkipThrow()
	{
		leftSideWindow.SetActive (false);
		mainWindow.SetActive (true);
		mainWindow.GetComponentsInChildren<Text>()[0].text = "Geeeeeeems!";
        LocalizationManager.Instance.UpdateLocaliztion(mainWindowText);
        mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipGemsPreview());
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 150);
		CardToBasePlan (throwPanel, tmpCard);
		throwSwipeMenu.UpdateThrowCards ();

		currentShop++;
		shopController.ActivateShop (currentShop);
		SetArrowPosition ();
		arrow.gameObject.SetActive (true);
	}

	public void SkipGemsPreview()
	{
		mainWindow.SetActive (false);
		leftSideWindow.SetActive (true);
		leftSideWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		leftSideWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => SkipGems());
		leftSideWindow.GetComponentsInChildren<Text>()[0].text = "You can buy everything in this game with Gems!";
        LocalizationManager.Instance.UpdateLocaliztion(leftSideWindowText);
        fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 210);
		CardToFrontPlan (gemsPanel, 1);

		arrow.gameObject.SetActive (false);
	}

	public void SkipGems()
	{
		leftSideWindow.SetActive (false);
		mainWindow.SetActive (true);
		mainWindow.GetComponentsInChildren<Text>()[0].text = "Lets buy Health Potion!";
        LocalizationManager.Instance.UpdateLocaliztion(mainWindowText);
        mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => BuyHealtPot());
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 150);
		CardToBasePlan (gemsPanel, tmpCard);
		

		currentShop = 2;
		shopController.ActivateShop (currentShop);
		SetArrowPosition ();
		arrow.gameObject.SetActive (true);
	}

	public void BuyHealtPot()
	{
		mainWindow.SetActive (false);
		arrow.gameObject.SetActive (false);
		//leftSideWindow.SetActive (true);
		//leftSideWindow.transform.localPosition = new Vector3 (0, 0, 0);
		//leftSideWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		//leftSideWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => Zag());
		//leftSideWindow.GetComponentsInChildren<Text>()[0].text = "Now buy Health Pot";
		fade.GetComponent<Image> ().color = new Color32 (0, 0, 0, 150);
		tmpCard = GameObject.Instantiate(itemsPanel.transform.GetChild (0).gameObject) as GameObject;
		tmpCard.transform.transform.position = itemsPanel.transform.GetChild (0).gameObject.transform.position;
		tmpCard.gameObject.transform.SetParent (frontPlan.transform);
		tmpCard.transform.localScale = new Vector3 (1.05f, 1.05f, 1);

		tmpCard.GetComponentsInChildren<Button> () [0].onClick.AddListener(() => itemSwipeMenu.ActivateBuyItemWindow("HealthPot"));
		tmpCard.GetComponentsInChildren<Button> () [1].onClick.AddListener(() => itemSwipeMenu.ActivateBuyItemWindow("HealthPot"));

		tmpCard.GetComponentsInChildren<Button> () [0].onClick.AddListener(() => BuyItemWindowMenu());
		tmpCard.GetComponentsInChildren<Button> () [1].onClick.AddListener(() => BuyItemWindowMenu());
	}

	public void BuyItemWindowMenu()
	{
		Destroy (tmpCard);
		buyItemMenu.transform.SetParent (frontPlan.transform);
		leftSideWindow.SetActive (false);
		closeByuItemMenuButton.SetActive (false);

		buyByCrystalsButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
		buyByCrystalsButton.GetComponent<Image> ().color = new Color32 (147,147,147,255);
		buyByCrystalsButton.GetComponentInChildren<Text> ().color = new Color32 (147,147,147,255);

		buyByCoinsButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
		buyByCoinsButton.GetComponentInChildren<Text>().text = "Free";
		buyByCoinsButton.GetComponent<Button> ().onClick.AddListener (() => Inventory.Instance.BuyItem("HealthPot", 3, "Free", Inventory.Instance.GetCoinCost("HealthPot")));
		buyByCoinsButton.GetComponent<Button> ().onClick.AddListener (() => EndOfTutorial());

	}

	public void EndOfTutorial()
	{
		buyByCrystalsButton.GetComponent<Image> ().color = new Color32 (255,255,255,255);
		buyByCrystalsButton.GetComponentInChildren<Text> ().color = new Color32 (255,255,255,255);
		buyByCoinsButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
		buyByCrystalsButton.GetComponent<Button> ().onClick.RemoveAllListeners ();

		buyItemMenu.transform.SetParent (itemSwipeMenu.gameObject.transform);
		buyItemMenu.SetActive (false);
		itemsFade.SetActive (false);
		mainWindow.SetActive (true);
		mainWindow.GetComponentsInChildren<Text>()[0].text = "Congratulations! You get 3 Free Health potions! Good luck! See you soon Warrior!";
        LocalizationManager.Instance.UpdateLocaliztion(mainWindowText);
        mainWindow.GetComponentInChildren<Button> ().onClick.RemoveAllListeners ();
		mainWindow.GetComponentsInChildren<Text>()[1].text = "Close";
        LocalizationManager.Instance.UpdateLocaliztion(mainWindow.GetComponentsInChildren<Text>()[1]);
        mainWindow.GetComponentInChildren<Button> ().onClick.AddListener (() => CloseTutorial());

	}

	public void CloseTutorial()
	{
		this.gameObject.SetActive (false);
		PlayerPrefs.SetInt ("ShopTutorialComplete", 1);
        DevToDev.Analytics.Tutorial(5);
    }
}
