﻿using UnityEngine;
using UnityEngine.UI;

public class SkinSwipeMenu : SwipeMenu {

    [SerializeField]
    GameObject skinCard;
    [SerializeField]
    GameObject unlockSkinWindow;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject closeBuyWindowButton;
	[SerializeField]
	Sprite equipButton;

    public override void Start()
    {
		int buttonsLength = SkinManager.Instance.skinPrefabs.Length;
//		for (int i = 0; i < SkinManager.Instance.skinPrefabs.Length; i++) 
//		{
//			if (SkinManager.Instance.skinPrefabs [i].GetComponent<SkinPrefab> ().isAvaliableInShop)
//				buttonsLength++;
//		}
		buttons = new GameObject[buttonsLength];
		SetSkinCards();
        distance = new float[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = panel.GetChild(i).gameObject;
        }
        buttonDistance = (int)DISTANCE;
        minButtonsNumber = 1;
        panel.anchoredPosition = new Vector2(buttons[1].transform.position.x, panel.anchoredPosition.y);
    }

    public override void Update()
    {
        base.Update();
    }

    private void SetSkinCards()
    {
        for (int i = 0; i < SkinManager.Instance.skinPrefabs.Length; i++)
        {
            for (int j = 0; j < SkinManager.Instance.skinPrefabs.Length; j++)
            {
                SkinPrefab skin = SkinManager.Instance.skinPrefabs[j].GetComponent<SkinPrefab>();
				if (skin.orderNumber == i)
                {
                    GameObject skinCardObj = Instantiate(skinCard, new Vector3(buttonDistance * i, 0, 0),Quaternion.identity) as GameObject;

                    skin.SetPlayerPrefsParams ();			
                    skinCardObj.transform.SetParent(panel);
                    skinCardObj.transform.localPosition = new Vector3(i * DISTANCE, 0, 0);
                    skinCardObj.transform.localScale = new Vector3(1, 1, 1);
                    skinCardObj.gameObject.GetComponentsInChildren<Text>()[0].text = skin.shopName;
                    skinCardObj.gameObject.GetComponentsInChildren<Image>()[1].sprite = skin.skinSprite;
					if (skin.isAvaliableInShop) 
					{
						skinCardObj.gameObject.GetComponentsInChildren<Image> () [5].gameObject.SetActive (false);

						if (PlayerPrefs.GetString (skin.name) == "Unlocked") {
							if (PlayerPrefs.GetString ("Skin") == skin.name) {
								skinCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equiped";
							} else {
								skinCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equip";
							}
							skinCardObj.GetComponentInChildren<SkinStatsPanel> ().TurnOffCoinCost ();
							skinCardObj.GetComponentInChildren<SkinStatsPanel> ().ActivateCheck (true);
							skinCardObj.gameObject.GetComponentsInChildren<Image> () [3].sprite = equipButton;
							skinCardObj.gameObject.GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ApplySkin (skin.orderNumber));
							skinCardObj.gameObject.GetComponentsInChildren<Button> () [1].onClick.AddListener (() => ApplySkin (skin.orderNumber));
						} else {
							skinCardObj.GetComponentInChildren<SkinStatsPanel> ().ActivateCheck (false);
							skinCardObj.GetComponentInChildren<SkinStatsPanel> ().SetCoinCost (skin.coinCost);
							skinCardObj.gameObject.GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ShowUnlockSkinWindow (SkinManager.Instance.NumberOfSkinPrefabBySkinOrder (skin.orderNumber)));
							skinCardObj.gameObject.GetComponentsInChildren<Button> () [1].onClick.AddListener (() => ShowUnlockSkinWindow (SkinManager.Instance.NumberOfSkinPrefabBySkinOrder (skin.orderNumber)));
						}
					} else 
					{
						skinCardObj.GetComponentInChildren<SkinStatsPanel> ().TurnOffCoinCost ();
						skinCardObj.GetComponentInChildren<SkinStatsPanel> ().ActivateCheck (false);
						skinCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "locked";
						skinCardObj.gameObject.GetComponentsInChildren<Button>()[0].onClick.RemoveAllListeners();
						skinCardObj.gameObject.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
						skinCardObj.gameObject.GetComponentsInChildren<Image> () [0].color = new Color32 (206,206,206,255);
						skinCardObj.gameObject.GetComponentsInChildren<Image> () [1].color = new Color32 (180,180,180,255);
						skinCardObj.gameObject.GetComponentsInChildren<Image> () [3].color = new Color32 (180,180,180,255);
					}
					LocalizationManager.Instance.UpdateLocaliztion (skinCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ());
					LocalizationManager.Instance.UpdateLocaliztion (skinCardObj.gameObject.GetComponentsInChildren<Text>()[0]);
					skinCardObj.GetComponentInChildren<SkinStatsPanel> ().SetDefendIndicators (skin.armorStat);



                    buttons[i] = skinCardObj;
                    break;
                }
            }
            
        }
    }

    public void UpdateSkinCards()
    {
        for (int i = 0; i < SkinManager.Instance.skinPrefabs.Length; i++)
        {
            for (int j = 0; j < SkinManager.Instance.skinPrefabs.Length; j++) 
            {
				SkinPrefab skin = SkinManager.Instance.skinPrefabs[j].GetComponent<SkinPrefab>();
                if (skin.orderNumber == i)
                {
					buttons[i].gameObject.GetComponentsInChildren<Button>()[0].onClick.RemoveAllListeners();
					buttons[i].gameObject.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
					if (skin.isAvaliableInShop) {
						if (PlayerPrefs.GetString (skin.name) == "Unlocked") {
							if (PlayerPrefs.GetString ("Skin") == skin.name) {
								buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equiped";
							} else {
								buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equip";
							}
							buttons [i].GetComponentInChildren<SkinStatsPanel> ().TurnOffCoinCost ();
							buttons [i].GetComponentInChildren<SkinStatsPanel> ().ActivateCheck (true);
							buttons [i].gameObject.GetComponentsInChildren<Image> () [3].sprite = equipButton;
							buttons [i].gameObject.GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ApplySkin (skin.orderNumber));
							buttons [i].gameObject.GetComponentsInChildren<Button> () [1].onClick.AddListener (() => ApplySkin (skin.orderNumber));
						} else {
							buttons [i].gameObject.GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ShowUnlockSkinWindow (SkinManager.Instance.NumberOfSkinPrefabBySkinOrder (skin.orderNumber)));
							buttons [i].gameObject.GetComponentsInChildren<Button> () [1].onClick.AddListener (() => ShowUnlockSkinWindow (SkinManager.Instance.NumberOfSkinPrefabBySkinOrder (skin.orderNumber)));
						}
					} else 
					{
						buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "locked";
						buttons [i].gameObject.GetComponentsInChildren<Button>()[0].onClick.RemoveAllListeners();
						buttons [i].gameObject.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
						buttons [i].gameObject.GetComponentsInChildren<Image> () [0].color = new Color32 (206,206,206,255);
						buttons [i].gameObject.GetComponentsInChildren<Image> () [1].color = new Color32 (180,180,180,255);
						buttons [i].gameObject.GetComponentsInChildren<Image> () [2].color = new Color32 (180,180,180,255);
					}
					LocalizationManager.Instance.UpdateLocaliztion (buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ());
                }
            }
        }
    }
    public void ApplySkin(int skinOrderNumber) // writing to player prefs current skin
    {
		base.OnButtonClickLerp(skinOrderNumber);
		SkinManager.Instance.ApplySkin(SkinManager.Instance.NameOfSkinPrefabBySkinOrder(skinOrderNumber));
		UpdateSkinCards();
    }

    public void ShowUnlockSkinWindow(int skinNumber)
    {
		base.OnButtonClickLerp (SkinManager.Instance.skinPrefabs[skinNumber].GetComponent<SkinPrefab>().orderNumber);
        unlockSkinWindow.gameObject.SetActive(true);
        fade.gameObject.SetActive(true);
        closeBuyWindowButton.gameObject.SetActive(true);

        unlockSkinWindow.GetComponent<UnlockSkinWindow>().SetWindowWithSkinNumber(skinNumber);
    }
    public void CloseUnlockSkinWindow()
    {
        unlockSkinWindow.gameObject.SetActive(false);
        fade.gameObject.SetActive(false);
        closeBuyWindowButton.gameObject.SetActive(false);
    }

    public override void LerpToButton(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;

        if (Mathf.Abs(panel.anchoredPosition.x - position) < changingDistance)
        {
            tapping = false;
        }
    }



}
