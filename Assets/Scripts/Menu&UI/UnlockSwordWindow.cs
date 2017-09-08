using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSwordWindow : MonoBehaviour {


	// HANDLING ALL WINDOW EVENTS

	[SerializeField]
	GameObject buyCoinsButton;
	[SerializeField]
	GameObject buyCrystalsButton;
	[SerializeField]
	GameObject swordName;
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
	GameObject swordSwipe;
	[SerializeField]
	GameObject swordTransform;

	[SerializeField]
	Transform onebuttonTransform;
	[SerializeField]
	Transform crystalButtonTransform;
	[SerializeField]
	Transform coinButtonTransform;

	private string chosenSwordName;
	private int swordIndex;


	public void SetWindowWithSwordNumber(int swordNumber)
	{
		SwordPrefab sword = SkinManager.Instance.swordPrefabs[swordNumber].gameObject.GetComponent<SwordPrefab>();

		//KidSkin.Instance.ChangeSkin(skinNumber);

		swordIndex = SkinManager.Instance.swordPrefabs [swordNumber].GetComponent<SwordPrefab> ().displayIndex;
		swordName.GetComponent<Text>().text = sword.shopName;
		chosenSwordName = sword.name;
		statsPanel.GetComponentInChildren<SkinStatsPanel>().SetAttackIndicators(sword.attackStat);
		//statsPanel.GetComponentInChildren<SkinStatsPanel>().SetDefendIndicators(sword.armorStat);
		if (sword.isLocked)
		{
			swordTransform.GetComponent<Image> ().sprite = sword.swordSprite;
			ResetButtons();
			if (sword.coinCost == 0 && sword.crystalCost != 0)
			{
				buyCrystalsButton.transform.localPosition = onebuttonTransform.localPosition;
				buyCrystalsButton.GetComponentInChildren<Text>().text = sword.crystalCost.ToString();

				buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
				buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySwordByCrystals(swordNumber));

				buyCoinsButton.gameObject.SetActive(false);
			}
			else if (sword.crystalCost == 0 && sword.coinCost != 0)
			{
				buyCoinsButton.transform.localPosition = onebuttonTransform.localPosition;
				buyCoinsButton.GetComponentInChildren<Text>().text = sword.coinCost.ToString();

				buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
				buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySwordByCoins(swordNumber));

				buyCrystalsButton.gameObject.SetActive(false);               
			}
			else if (sword.crystalCost == 0 && sword.coinCost == 0)
			{
				// if free skin;
			}
			else
			{
				ResetButtons();                
				buyCrystalsButton.GetComponentInChildren<Text>().text = sword.crystalCost.ToString();
				buyCoinsButton.GetComponentInChildren<Text>().text = sword.coinCost.ToString();

				buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
				buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySwordByCrystals(swordNumber));
				buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
				buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySwordByCoins(swordNumber));
			}
		}
	}

	public void ApplySword(string sword, int index)
	{
		SkinManager.Instance.ApplySword (sword, index);
	}

	public void CanBuySwordByCrystals (int swordNumber)
	{
		if (SkinManager.Instance.swordPrefabs[swordNumber].GetComponent<SwordPrefab>().isLocked)
		{
			if (SkinManager.Instance.BuySwordByCrystals(swordNumber))
			{
				// ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА
				HideButtons();
				closeErrorWindowButton.GetComponent<Button>().onClick.RemoveAllListeners();
				closeErrorWindowButton.GetComponent<Button>().onClick.AddListener(() => CloseUnlockSwordWindow());
				swordTransform.SetActive (false);
				ShowErrorWindow("SWORD UNLOCKED");
				ApplySword (chosenSwordName, swordIndex);
			}
			else
			{
				ShowErrorWindow("NOT ENOUGH CRYSTALS");
				swordTransform.SetActive (false);
			}
		}
		else
		{
		}

	}
	public void CanBuySwordByCoins(int swordNumber)
	{
		if (SkinManager.Instance.swordPrefabs[swordNumber].GetComponent<SwordPrefab>().isLocked)
		{
			if (SkinManager.Instance.BuySwordByCoins(swordNumber))
			{
				// ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА
				HideButtons();
				closeErrorWindowButton.GetComponent<Button>().onClick.RemoveAllListeners();
				closeErrorWindowButton.GetComponent<Button>().onClick.AddListener(() => CloseUnlockSwordWindow());
				swordTransform.SetActive (false);
				ShowErrorWindow("SWORD UNLOCKED");
				ApplySword (chosenSwordName, swordIndex);
			}
			else
			{
				ShowErrorWindow("NOT ENOUGH COINS");
				swordTransform.SetActive (false);
			}
		}
		else
		{
		}        
	}

	public void CloseUnlockSwordWindow()
	{
		CloseErrorWindow();
		swordSwipe.GetComponent<SwordsSwipeMenu>().UpdateSwordCards();
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
		swordTransform.SetActive (true);
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

