using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkinWindow : MonoBehaviour {


    // HANDLING ALL WINDOW EVENTS

    [SerializeField]
    GameObject buyCoinsButton;
    [SerializeField]
    GameObject buyCrystalsButton;
    [SerializeField]
    GameObject skinName;
    [SerializeField]
    GameObject statsPanel;
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
    GameObject skinSwipe;
	[SerializeField]
	GameObject skinTransform;

    [SerializeField]
    Transform onebuttonTransform;
    [SerializeField]
    Transform crystalButtonTransform;
    [SerializeField]
    Transform coinButtonTransform;

	private string chosenSkinName;
	private int skinIndex;


    public void SetWindowWithSkinNumber(int skinNumber)
    {
        SkinPrefab skin = SkinManager.Instance.skinPrefabs[skinNumber].gameObject.GetComponent<SkinPrefab>();
		KidSkin.Instance.ChangeSkin(skinNumber);
		KidSkin.Instance.SwordInShop ();
		skinIndex = SkinManager.Instance.skinPrefabs [skinNumber].GetComponent<SkinPrefab> ().displayIndex;
        skinName.GetComponent<Text>().text = skin.shopName;
		LocalizationManager.Instance.UpdateLocaliztion (skinName.GetComponent<Text> ());
		chosenSkinName = skin.name;
        statsPanel.GetComponentInChildren<SkinStatsPanel>().SetDefendIndicators(skin.armorStat);
        if (skin.isLocked)
        {
            ResetButtons();
            if (skin.coinCost == 0 && skin.crystalCost != 0)
            {
                buyCrystalsButton.transform.localPosition = onebuttonTransform.localPosition;
                buyCrystalsButton.GetComponentInChildren<Text>().text = skin.crystalCost.ToString();

                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySkinByCrystals(skinNumber));

                buyCoinsButton.gameObject.SetActive(false);
            }
            else if (skin.crystalCost == 0 && skin.coinCost != 0)
            {
                buyCoinsButton.transform.localPosition = onebuttonTransform.localPosition;
                buyCoinsButton.GetComponentInChildren<Text>().text = skin.coinCost.ToString();
                
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySkinByCoins(skinNumber));

                buyCrystalsButton.gameObject.SetActive(false);               
            }
            else if (skin.crystalCost == 0 && skin.coinCost == 0)
            {
                // if free skin;
            }
            else
            {
                ResetButtons();                
                buyCrystalsButton.GetComponentInChildren<Text>().text = skin.crystalCost.ToString();
                buyCoinsButton.GetComponentInChildren<Text>().text = skin.coinCost.ToString();

                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySkinByCrystals(skinNumber));
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySkinByCoins(skinNumber));
            }
        }
    }

	public void ApplySkin(string skin)
    {
		SkinManager.Instance.ApplySkin (skin);
    }

    public void CanBuySkinByCrystals (int skinNumber)
    {
        if (SkinManager.Instance.skinPrefabs[skinNumber].GetComponent<SkinPrefab>().isLocked)
        {
            if (SkinManager.Instance.BuySkinByCrystals(skinNumber))
            {
                // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА СКИНА
                HideButtons();
                closeErrorWindowButton.GetComponent<Button>().onClick.RemoveAllListeners();
                closeErrorWindowButton.GetComponent<Button>().onClick.AddListener(() => CloseUnlockSkinWindow());
				skinTransform.SetActive (false);
				ShowErrorWindow("skin unlocked");
				ApplySkin (chosenSkinName);
            }
            else
            {
                ShowErrorWindow("not enough crystals");
				skinTransform.SetActive (false);
            }
        }
        else
        {
        }
            
    }
    public void CanBuySkinByCoins(int skinNumber)
    {
        if (SkinManager.Instance.skinPrefabs[skinNumber].GetComponent<SkinPrefab>().isLocked)
        {
            if (SkinManager.Instance.BuySkinByCoins(skinNumber))
            {
                // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА СКИНА
                HideButtons();
                closeErrorWindowButton.GetComponent<Button>().onClick.RemoveAllListeners();
                closeErrorWindowButton.GetComponent<Button>().onClick.AddListener(() => CloseUnlockSkinWindow());
				skinTransform.SetActive (false);
				ShowErrorWindow("skin unlocked");
				ApplySkin (chosenSkinName);
            }
            else
            {
				ShowErrorWindow("not enough coins");
				skinTransform.SetActive (false);
            }
        }
        else
        {
        }        
    }

    public void CloseUnlockSkinWindow()
    {
        CloseErrorWindow();
        skinSwipe.GetComponent<SkinSwipeMenu>().UpdateSkinCards();
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
		LocalizationManager.Instance.UpdateLocaliztion (errorWindow.GetComponentInChildren<Text>());
    }

    private void HideButtons()
    {
        buyCoinsButton.SetActive(false);
        buyCrystalsButton.SetActive(false);
    }

    public void CloseErrorWindow()
    {
		skinTransform.SetActive (true);
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
