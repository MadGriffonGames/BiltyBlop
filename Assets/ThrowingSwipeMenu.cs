using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowingSwipeMenu : SwipeMenu {

	[SerializeField]
	GameObject throwCard;
	[SerializeField]
	GameObject unlockThrowWindow;
	[SerializeField]
	GameObject fade;
	[SerializeField]
	GameObject closeBuyWindowButton;
	[SerializeField]
	Sprite equipButton;

	private const float DISTANCE = 175f;

	public override void Start () 
	{
		buttons = new GameObject[SkinManager.Instance.throwPrefabs.Length];
		SetThrowCards();
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

	void SetThrowCards()
	{
		for (int i = 0; i < SkinManager.Instance.throwPrefabs.Length; i++)
		{
			for (int j = 0; j < SkinManager.Instance.throwPrefabs.Length; j++)
			{
				if (SkinManager.Instance.throwPrefabs[j].GetComponent<ThrowPrefab>().orderNumber == i)
				{
					GameObject throwCardObj = Instantiate(throwCard, new Vector3(buttonDistance * i, 0, 0),Quaternion.identity) as GameObject;
					ThrowPrefab throwScript = SkinManager.Instance.throwPrefabs[j].GetComponent<ThrowPrefab>();
					throwScript.SetPlayerPrefsParams ();			

					throwCardObj.transform.SetParent(panel);
					throwCardObj.transform.localPosition = new Vector3(i * DISTANCE, 0, 0);
					throwCardObj.transform.localScale = new Vector3(1, 1, 1);
					throwCardObj.gameObject.GetComponentsInChildren<Text>()[0].text = throwScript.shopName;
					LocalizationManager.Instance.UpdateLocaliztion (throwCardObj.gameObject.GetComponentsInChildren<Text>()[0]); // SHOP NAME
					throwCardObj.gameObject.GetComponentsInChildren<Image>()[1].sprite = throwScript.throwSprite;

					if (PlayerPrefs.GetString(throwScript.name) == "Unlocked")
					{
						if (PlayerPrefs.GetString ("Throw") == throwScript.name)
						{
							throwCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equiped";
						} 
						else  
						{
							throwCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equip";
						}
						throwCardObj.gameObject.GetComponentsInChildren<Image> () [2].sprite = equipButton;
						throwCardObj.gameObject.GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ApplyThrow (throwScript.orderNumber));
						throwCardObj.gameObject.GetComponentsInChildren<Button> () [1].onClick.AddListener (() => ApplyThrow (throwScript.orderNumber));
					}
					else
					{
						throwCardObj.gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => ShowUnlockThrowWindow(SkinManager.Instance.NumberOfThrowPrefabByOrder(throwScript.orderNumber))); // wdfsdf
						throwCardObj.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ShowUnlockThrowWindow(SkinManager.Instance.NumberOfThrowPrefabByOrder(throwScript.orderNumber)));
					}
					LocalizationManager.Instance.UpdateLocaliztion (throwCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ()); // buy button
					throwCardObj.GetComponentInChildren<SkinStatsPanel>().SetAttackIndicators(throwScript.attackStat);

					buttons[i] = throwCardObj;
					break;
				}
			}

		}
	}

	public void ShowUnlockThrowWindow(int throwNumber)
	{
		base.OnButtonClickLerp (SkinManager.Instance.throwPrefabs[throwNumber].GetComponent<ThrowPrefab>().orderNumber);
		unlockThrowWindow.gameObject.SetActive(true);
		fade.gameObject.SetActive(true);
		closeBuyWindowButton.gameObject.SetActive(true);

		unlockThrowWindow.GetComponent<UnlockThrowWindow>().SetWindowWithThrowNumber(throwNumber);
	}

	public void CloseUnlockThrowWindow()
	{
		unlockThrowWindow.gameObject.SetActive(false);
		fade.gameObject.SetActive(false);
		closeBuyWindowButton.gameObject.SetActive(false);
	}


	public void UpdateThrowCards()
	{
		for (int i = 0; i < SkinManager.Instance.throwPrefabs.Length; i++)
		{
			for (int j = 0; j < SkinManager.Instance.throwPrefabs.Length; j++) 
			{
				if (SkinManager.Instance.throwPrefabs[j].GetComponent<ThrowPrefab>().orderNumber == i)
				{
					ThrowPrefab throwScript = SkinManager.Instance.throwPrefabs[j].GetComponent<ThrowPrefab>();
					if (PlayerPrefs.GetString(throwScript.name) == "Unlocked")
					{
						buttons[i].gameObject.GetComponentsInChildren<Button>()[0].onClick.RemoveAllListeners();
						buttons[i].gameObject.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
						if (PlayerPrefs.GetString ("Throw") == throwScript.name) 
						{
							buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equiped";
						} 
						else 
						{
							buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "equip";
						}
						buttons [i].gameObject.GetComponentsInChildren<Image> () [2].sprite = equipButton;
						buttons[i].gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => ApplyThrow(throwScript.orderNumber));
						buttons[i].gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ApplyThrow(throwScript.orderNumber));
					}
					LocalizationManager.Instance.UpdateLocaliztion (buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ()); // buy button
				}
			}
		}
	}
	public void ApplyThrow(int throwOrderNumber) // writing to player prefs current skin
	{
		base.OnButtonClickLerp(throwOrderNumber);
		SkinManager.Instance.ApplyThrow(SkinManager.Instance.NameOfThrowPrefabBySwordOrder(throwOrderNumber));
		UpdateThrowCards();
	}
}
