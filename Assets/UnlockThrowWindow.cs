using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockThrowWindow : MonoBehaviour {


	// HANDLING ALL WINDOW EVENTS

	[SerializeField]
	GameObject buyCoinsButton;
	[SerializeField]
	GameObject buyCrystalsButton;
	[SerializeField]
	GameObject throwName;
	[SerializeField]
	GameObject statsPanel;
	[SerializeField]
	GameObject applyButton;
	[SerializeField]
	GameObject errorWindow;
	[SerializeField]
	GameObject fade;
	[SerializeField]
	GameObject closeErrorWindowButton;
	[SerializeField]
	GameObject closeWindowButton;
	[SerializeField]
	GameObject windowFade;
	[SerializeField]
	GameObject throwSwipe;
	[SerializeField]
	GameObject throwTransform;

	[SerializeField]
	Transform onebuttonTransform;
	[SerializeField]
	Transform crystalButtonTransform;
	[SerializeField]
	Transform coinButtonTransform;

	private string chosenThrowName;


	public void SetWindowWithThrowNumber(int throwNumber)
	{
		ThrowPrefab throwScript = SkinManager.Instance.throwPrefabs[throwNumber].gameObject.GetComponent<ThrowPrefab>();

		throwName.GetComponent<Text>().text = throwScript.shopName;
		chosenThrowName = throwScript.name;
		statsPanel.GetComponentInChildren<SkinStatsPanel>().SetAttackIndicators(throwScript.attackStat);
		//statsPanel.GetComponentInChildren<SkinStatsPanel>().SetDefendIndicators(sword.armorStat);
		if (throwScript.isLocked)
		{
			throwTransform.GetComponent<Image> ().sprite = throwScript.throwSprite;
			ResetButtons();
			if (throwScript.coinCost == 0 && throwScript.crystalCost != 0)
			{
				buyCrystalsButton.transform.localPosition = onebuttonTransform.localPosition;
				buyCrystalsButton.GetComponentInChildren<Text>().text = throwScript.crystalCost.ToString();

				buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
				buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuyThrowByCrystals(throwNumber));

				buyCoinsButton.gameObject.SetActive(false);
			}
			else if (throwScript.crystalCost == 0 && throwScript.coinCost != 0)
			{
				buyCoinsButton.transform.localPosition = onebuttonTransform.localPosition;
				buyCoinsButton.GetComponentInChildren<Text>().text = throwScript.coinCost.ToString();

				buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
				buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuyThrowByCoins(throwNumber));

				buyCrystalsButton.gameObject.SetActive(false);               
			}
			else if (throwScript.crystalCost == 0 && throwScript.coinCost == 0)
			{
				// if free THROW;
			}
			else
			{
				ResetButtons();                
				buyCrystalsButton.GetComponentInChildren<Text>().text = throwScript.crystalCost.ToString();
				buyCoinsButton.GetComponentInChildren<Text>().text = throwScript.coinCost.ToString();

				buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
				buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuyThrowByCrystals(throwNumber));
				buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
				buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuyThrowByCoins(throwNumber));
			}
		}
	}

	public void ApplyThrow(string throwName)
	{
		SkinManager.Instance.ApplyThrow (throwName);
	}

	public void CanBuyThrowByCrystals (int throwNumber)
	{
		if (SkinManager.Instance.throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().isLocked)
		{
			if (SkinManager.Instance.BuyThrowByCrystals(throwNumber))
			{
				// ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА
				HideButtons();
				closeErrorWindowButton.GetComponent<Button>().onClick.RemoveAllListeners();
				closeErrorWindowButton.GetComponent<Button>().onClick.AddListener(() => CloseUnlockThrowWindow());
				ShowErrorWindow("SWORD UNLOCKED");
				ApplyThrow (chosenThrowName);
			}
			else
			{
				ShowErrorWindow("NOT ENOUGH CRYSTALS");
			}
		}
		else
		{
		}

	}
	public void CanBuyThrowByCoins(int throwNumber)
	{
		if (SkinManager.Instance.throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().isLocked)
		{
			if (SkinManager.Instance.BuyThrowByCoins(throwNumber))
			{
				// ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА
				HideButtons();
				closeErrorWindowButton.GetComponent<Button>().onClick.RemoveAllListeners();
				closeErrorWindowButton.GetComponent<Button>().onClick.AddListener(() => CloseUnlockThrowWindow());
				throwTransform.SetActive (false);
				ApplyThrow (chosenThrowName);
			}
			else
			{
				ShowErrorWindow("NOT ENOUGH COINS");
			}
		}
		else
		{
		}        
	}

	public void CloseUnlockThrowWindow()
	{
		CloseErrorWindow();
		throwSwipe.GetComponent<ThrowingSwipeMenu>().UpdateThrowCards();
		windowFade.SetActive(false);
		closeWindowButton.SetActive(false);
		gameObject.SetActive(false);
	}

	private void ShowErrorWindow(string text)
	{
		fade.gameObject.SetActive(true);
		closeErrorWindowButton.gameObject.SetActive(true);
		errorWindow.gameObject.SetActive(true);
		errorWindow.GetComponentInChildren<Text>().text = text;
	}

	private void HideButtons()
	{
		buyCoinsButton.SetActive(false);
		buyCrystalsButton.SetActive(false);
	}

	public void CloseErrorWindow()
	{
		fade.gameObject.SetActive(false);
		closeErrorWindowButton.gameObject.SetActive(false);
		errorWindow.gameObject.SetActive(false);
	}
	private void ResetButtons()
	{
		buyCrystalsButton.gameObject.SetActive(true);
		buyCrystalsButton.gameObject.GetComponentInChildren<Text>().text = "";

		buyCoinsButton.gameObject.SetActive(true);
		buyCoinsButton.gameObject.GetComponentInChildren<Text>().text = "";

		buyCoinsButton.transform.localPosition = coinButtonTransform.localPosition;
		buyCrystalsButton.transform.localPosition = crystalButtonTransform.transform.localPosition;
	}
}


