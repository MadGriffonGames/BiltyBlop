using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordsSwipeMenu : SwipeMenu {

	[SerializeField]
	GameObject swordCard;
	[SerializeField]
	GameObject unlockSwordWindow;
	[SerializeField]
	GameObject fade;
	[SerializeField]
	GameObject closeBuyWindowButton;
	[SerializeField]
	Sprite equipButton;

	private const float DISTANCE = 175f;

	public override void Start () 
	{
		buttons = new GameObject[SkinManager.Instance.swordPrefabs.Length];
		SetSwordCards();
		distance = new float[buttons.Length];
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i] = panel.GetChild(i).gameObject;
		}

		buttonDistance = (int)DISTANCE;
		minButtonsNumber = 1;
		panel.anchoredPosition = new Vector2(buttons[1].transform.position.x, panel.anchoredPosition.y);
	}

	public override void Update () {
		base.Update ();	
	}

	public void SetSwordCards()
	{
		for (int i = 0; i < SkinManager.Instance.swordPrefabs.Length; i++)
		{
			for (int j = 0; j < SkinManager.Instance.swordPrefabs.Length; j++)
			{
				if (SkinManager.Instance.swordPrefabs[j].GetComponent<SwordPrefab>().orderNumber == i)
				{
					GameObject swordCardObj = Instantiate(swordCard, new Vector3(buttonDistance * i, 0, 0),Quaternion.identity) as GameObject;
					SwordPrefab sword = SkinManager.Instance.swordPrefabs[j].GetComponent<SwordPrefab>();
					sword.SetPlayerPrefsParams ();			

					swordCardObj.transform.SetParent(panel);
					swordCardObj.transform.localPosition = new Vector3(i * DISTANCE, 0, 0);
					swordCardObj.transform.localScale = new Vector3(1, 1, 1);
					swordCardObj.gameObject.GetComponentsInChildren<Text>()[0].text = sword.shopName;
					swordCardObj.gameObject.GetComponentsInChildren<Image>()[1].sprite = sword.swordSprite;

					if (PlayerPrefs.GetString(sword.name) == "Unlocked")
					{
						if (PlayerPrefs.GetString ("Sword") == sword.name)
						{
							swordCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equiped";
						} 
						else  
						{
							swordCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equip";
						}
						swordCardObj.gameObject.GetComponentsInChildren<Image> () [3].sprite = equipButton;
						swordCardObj.gameObject.GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ApplySword (sword.orderNumber));
						swordCardObj.gameObject.GetComponentsInChildren<Button> () [1].onClick.AddListener (() => ApplySword (sword.orderNumber));
						swordCardObj.GetComponentInChildren<SkinStatsPanel> ().TurnOffCoinCost ();
						swordCardObj.GetComponentInChildren<SkinStatsPanel> ().ActivateCheck (true);
					}
					else
					{
						swordCardObj.GetComponentInChildren<SkinStatsPanel> ().SetCoinCost (sword.coinCost);
						swordCardObj.GetComponentInChildren<SkinStatsPanel> ().ActivateCheck (false);
						swordCardObj.gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => ShowUnlockSwordWindow(SkinManager.Instance.NumberOfSwordPrefabBySwordOrder(sword.orderNumber))); // wdfsdf
						swordCardObj.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ShowUnlockSwordWindow(SkinManager.Instance.NumberOfSwordPrefabBySwordOrder(sword.orderNumber)));
					}
					swordCardObj.GetComponentInChildren<SkinStatsPanel>().SetAttackIndicators(sword.attackStat);
					LocalizationManager.Instance.UpdateLocaliztion (swordCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ());
					buttons[i] = swordCardObj;
					break;
				}
			}

		}
	}

	public void ShowUnlockSwordWindow(int swordNumber)
	{
		base.OnButtonClickLerp (SkinManager.Instance.swordPrefabs[swordNumber].GetComponent<SwordPrefab>().orderNumber);
		unlockSwordWindow.gameObject.SetActive(true);
		fade.gameObject.SetActive(true);
		closeBuyWindowButton.gameObject.SetActive(true);

		unlockSwordWindow.GetComponent<UnlockSwordWindow>().SetWindowWithSwordNumber(swordNumber);
	}

	public void CloseUnlockSwordWindow()
	{
		unlockSwordWindow.gameObject.SetActive(false);
		fade.gameObject.SetActive(false);
		closeBuyWindowButton.gameObject.SetActive(false);
	}


	public void UpdateSwordCards()
	{
		for (int i = 0; i < SkinManager.Instance.swordPrefabs.Length; i++)
		{
			for (int j = 0; j < SkinManager.Instance.swordPrefabs.Length; j++) 
			{
				if (SkinManager.Instance.swordPrefabs[j].GetComponent<SwordPrefab>().orderNumber == i)
				{
					SwordPrefab sword = SkinManager.Instance.swordPrefabs[j].GetComponent<SwordPrefab>();
					if (PlayerPrefs.GetString(sword.name) == "Unlocked")
					{
						buttons[i].gameObject.GetComponentsInChildren<Button>()[0].onClick.RemoveAllListeners();
						buttons[i].gameObject.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
						if (PlayerPrefs.GetString ("Sword") == sword.name) 
						{
							buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equiped";
						} 
						else 
						{
							buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equip";
						}
						LocalizationManager.Instance.UpdateLocaliztion (buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ());
						buttons [i].gameObject.GetComponentsInChildren<Image> () [3].sprite = equipButton;
						buttons[i].gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => ApplySword(sword.orderNumber));
						buttons[i].gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ApplySword(sword.orderNumber));
						buttons [i].GetComponentInChildren<SkinStatsPanel> ().TurnOffCoinCost ();
						buttons [i].GetComponentInChildren<SkinStatsPanel> ().ActivateCheck (true);
					}
				}
			}
		}
	}
	public void ApplySword(int swordOrderNumber) // writing to player prefs current skin
	{
		base.OnButtonClickLerp(swordOrderNumber);
		SkinManager.Instance.ApplySword(SkinManager.Instance.NameOfSwordPrefabBySwordOrder(swordOrderNumber), SkinManager.Instance.IndexOfSwordByOrderNumber(swordOrderNumber));
		UpdateSwordCards();
	}


}
