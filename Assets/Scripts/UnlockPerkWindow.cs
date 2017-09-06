using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPerkWindow : MonoBehaviour {

    // HANDLING ALL WINDOW EVENTS

    [SerializeField]
    GameObject buyCoinsButton;
    [SerializeField]
    GameObject buyCrystalsButton;
    [SerializeField]
    GameObject perkName;
	[SerializeField]
	GameObject perkDescription;
    [SerializeField]
    GameObject errorWindow;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject closeErrorWindowButton;

    [SerializeField]
    Transform onebuttonTransform;
    [SerializeField]
    Transform crystalButtonTransform;
    [SerializeField]
    Transform coinButtonTransform;

	private int perkLvl;
	private int chosenPerkOrderNumber;


    public void SetWindowWithPerkNumber(int perkOrderNumber)
    {
		chosenPerkOrderNumber = perkOrderNumber;
        int perkNumber = 0;
        for (int i = 0; i < PerksSwipeMenu.Instance.perkPrefabs.Length; i++)
        {
            if (PerksSwipeMenu.Instance.perkPrefabs[i].GetComponent<PerkPrefab>().orderNumber == perkOrderNumber)
            {
                perkNumber = i; // number of PERK in PERKPREFABS
                break;
            }
        }
        PerkPrefab perk = PerksSwipeMenu.Instance.perkPrefabs[perkNumber].gameObject.GetComponent<PerkPrefab>();

		perkDescription.GetComponent<Text>().text = perk.description;
		perkLvl = PlayerPrefs.GetInt (PerksSwipeMenu.Instance.perkPrefabs[perkNumber].name);
		perkName.GetComponent<Text>().text = perk.shopName + " (" + perkLvl.ToString() +")";

        // perkNumber - number of chosen perk in perkPrefabs[]

		if (perkLvl < 3)
        {
			int perkCoinCost = perk.upgradeCoinCost [perkLvl];
			int perkCrystalCost = perk.upgradeCrystalCost [perkLvl];
			if (perkCoinCost == 0 && perkCrystalCost != 0)
            {
                buyCrystalsButton.transform.localPosition = onebuttonTransform.localPosition;
                buyCrystalsButton.GetComponentInChildren<Text>().text = perkCrystalCost.ToString();

                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanUpgradePerkByCrystals(perkNumber));

                buyCoinsButton.gameObject.SetActive(false);
            }
            else if (perkCrystalCost == 0 && perkCoinCost != 0)
            {
                buyCoinsButton.transform.localPosition = onebuttonTransform.localPosition;
                buyCoinsButton.GetComponentInChildren<Text>().text = perkCoinCost.ToString();

                buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanUpgradePerkByCoins(perkNumber));

                buyCrystalsButton.gameObject.SetActive(false);
            }
            else if (perkCrystalCost == 0 && perkCoinCost == 0)
            {

            }
            else
            {
                ResetButtons();
                buyCrystalsButton.GetComponentInChildren<Text>().text = perkCrystalCost.ToString();
                buyCoinsButton.GetComponentInChildren<Text>().text = perkCoinCost.ToString();

                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanUpgradePerkByCrystals(perkNumber));
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanUpgradePerkByCoins(perkNumber));
            }
        }
    }

    public void CanUpgradePerkByCrystals(int perkNmber)
    {
        if (PerksSwipeMenu.Instance.CanUpgradePerkByCrystals(perkNmber))
        {
            // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА ПЕРКА
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "PERK UPGRADED";
        }
        else
        {
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "NOT ENOUGH CRYSTALS";
        }
    }
    public void CanUpgradePerkByCoins(int perkNumber)
    {
        if (PerksSwipeMenu.Instance.CanUpgradePerkByCoins(perkNumber))
        {
            // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА ПЕРКА
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "PERK UPGRADED";
        }
        else
        {
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "NOT ENOUGH COINS";
        }
    }

    public void CloseErrorWindow()
    {
        errorWindow.gameObject.SetActive(false);
        fade.gameObject.SetActive(false);
		UpdateWindowParams ();
        closeErrorWindowButton.gameObject.SetActive(false);
    }
    private void ResetButtons()
    {
        buyCrystalsButton.gameObject.SetActive(true);
        buyCoinsButton.gameObject.SetActive(true);
        buyCoinsButton.transform.localPosition = coinButtonTransform.localPosition;
        buyCrystalsButton.transform.localPosition = crystalButtonTransform.transform.localPosition;
    }
	private void UpdateWindowParams()
	{
		SetWindowWithPerkNumber (chosenPerkOrderNumber);
	}
}
