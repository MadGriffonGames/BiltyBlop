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
    GameObject errorWindow;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject closeErrorWindowButton;
	[SerializeField]
	Image perkImage;
	[SerializeField]
	PerksSwipeMenu perkSwipeMenu;

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
		perkImage.sprite = perk.perkSprite;
		perkLvl = PlayerPrefs.GetInt (PerksSwipeMenu.Instance.perkPrefabs[perkNumber].name);

        // perkNumber - number of chosen perk in perkPrefabs[]

		if (perkLvl < 3) 
		{
			
			int perkCoinCost = perk.upgradeCoinCost [perkLvl];
			int perkCrystalCost = perk.upgradeCrystalCost [perkLvl];
			if (perkCoinCost == 0 && perkCrystalCost != 0) {
				buyCrystalsButton.SetActive (true);
				buyCrystalsButton.transform.localPosition = onebuttonTransform.localPosition;
				buyCrystalsButton.GetComponentInChildren<Text> ().text = perkCrystalCost.ToString ();

				buyCrystalsButton.gameObject.GetComponent<Button> ().onClick.RemoveAllListeners ();
				buyCrystalsButton.gameObject.GetComponent<Button> ().onClick.AddListener (() => UpgradePerkByCrystals (perkNumber));

				buyCoinsButton.gameObject.SetActive (false);
			} else if (perkCrystalCost == 0 && perkCoinCost != 0) {
				buyCoinsButton.SetActive (true);
				buyCoinsButton.transform.localPosition = onebuttonTransform.localPosition;
				buyCoinsButton.GetComponentInChildren<Text> ().text = perkCoinCost.ToString ();

				buyCoinsButton.gameObject.GetComponent<Button> ().onClick.RemoveAllListeners ();
				buyCoinsButton.gameObject.GetComponent<Button> ().onClick.AddListener (() => UpgradePerkByCoins (perkNumber));

				buyCrystalsButton.gameObject.SetActive (false);
			} else if (perkCrystalCost == 0 && perkCoinCost == 0) {

			} else {
				ResetButtons ();
				buyCrystalsButton.GetComponentInChildren<Text> ().text = perkCrystalCost.ToString ();
				buyCoinsButton.GetComponentInChildren<Text> ().text = perkCoinCost.ToString ();

				buyCrystalsButton.gameObject.GetComponent<Button> ().onClick.RemoveAllListeners ();
				buyCrystalsButton.gameObject.GetComponent<Button> ().onClick.AddListener (() => UpgradePerkByCrystals (perkNumber));
				buyCoinsButton.gameObject.GetComponent<Button> ().onClick.RemoveAllListeners ();
				buyCoinsButton.gameObject.GetComponent<Button> ().onClick.AddListener (() => UpgradePerkByCoins (perkNumber));
			}
		} 
		perkName.GetComponent<Text> ().text = perk.shopName;
		LocalizationManager.Instance.UpdateLocaliztion (perkName.GetComponent<Text> ());
		perkName.GetComponent<Text> ().text += " (" + (perkLvl + 1).ToString () + ")";

    }

	public void SetWindowWithPerkStats(int perkOrderNumber)
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
		perkImage.sprite = perk.perkSprite;
		perkLvl = PlayerPrefs.GetInt (PerksSwipeMenu.Instance.perkPrefabs[perkNumber].name);
		perkName.GetComponent<Text>().text = perk.shopName + " (" + perkLvl.ToString() +")";

		// localization
		perkName.GetComponent<Text> ().text = perk.shopName;
		LocalizationManager.Instance.UpdateLocaliztion (perkName.GetComponent<Text> ());
		perkName.GetComponent<Text> ().text += " (" + (perkLvl + 1).ToString () + ")";

		buyCoinsButton.gameObject.SetActive (false);
		buyCrystalsButton.gameObject.SetActive (false);

		// perkNumber - number of chosen perk in perkPrefabs[]
	}

    public void UpgradePerkByCrystals(int perkNumber)
    {
        if (PerksSwipeMenu.Instance.CanUpgradePerkByCrystals(perkNumber))
        {
            // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА ПЕРКА
			perkLvl++;
			ShowErrorWindow( "perk upgraded");
            AppMetrica.Instance.ReportEvent("#PERK_BOUGHT " + PerksSwipeMenu.Instance.perkPrefabs[perkNumber].name);
            DevToDev.Analytics.CustomEvent("#PERK_BOUGHT " + PerksSwipeMenu.Instance.perkPrefabs[perkNumber].name);
        }
        else
        {
			ShowErrorWindow("not enough crystals");
        }
    }
    public void UpgradePerkByCoins(int perkNumber)
    {
        if (PerksSwipeMenu.Instance.CanUpgradePerkByCoins(perkNumber))
        {
            // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА ПЕРКА
			perkLvl ++;
			ShowErrorWindow("perk upgraded");
            AppMetrica.Instance.ReportEvent("#PERK_BOUGHT " + PerksSwipeMenu.Instance.perkPrefabs[perkNumber].name);
            DevToDev.Analytics.CustomEvent("#PERK_BOUGHT " + PerksSwipeMenu.Instance.perkPrefabs[perkNumber].name);
        }
        else
        {
			ShowErrorWindow("not enough coins");
        }
    }
	private void ShowErrorWindow(string text)
	{
		fade.gameObject.SetActive(true);
		closeErrorWindowButton.gameObject.SetActive(true);
		errorWindow.gameObject.SetActive(true);
		errorWindow.GetComponentInChildren<Text>().text = text;
		LocalizationManager.Instance.UpdateLocaliztion (errorWindow.GetComponentInChildren<Text>());
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
		if (perkLvl < 3)
			SetWindowWithPerkNumber (chosenPerkOrderNumber);
		else
			perkSwipeMenu.CloseUpgradePerkWindow ();
	}
}
