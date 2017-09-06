using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PerksSwipeMenu : SwipeMenu {

    private static Vector3 normalButttonScale = new Vector3(1, 1, 1);
    private static Vector3 increasedButttonScale = new Vector3(1.1f, 1.1f, 1);

    private const string PREFABS_FOLDER = "Perks/";

    public GameObject[] perkPrefabs;
    [SerializeField]
    GameObject perkCard;
    [SerializeField]
    GameObject unlockPerkWindow;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject closeUnlockPerkWindow;

    private const float DISTANCE = 175f;
	private const int MAX_PERK_LVL = 3;

    private static PerksSwipeMenu instance;
    public static PerksSwipeMenu Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<PerksSwipeMenu>();
            return instance;
        }
    }

    public override void Start ()
    {
        SetPerkCards();
		UpdatePerkMenu ();
        
    }
    public override void Update()
    {
        if (minButtonsNumber == 0 && tapping)
        {
            MakeActiveButton(minButtonsNumber);
            minButtonsNumber = 1;
        }
        else if (minButtonsNumber == buttons.Length - 1 && tapping)
        {
            MakeActiveButton(minButtonsNumber);
            minButtonsNumber = buttons.Length - 2;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            distance[i] = Mathf.Abs( Mathf.Abs(center.GetComponent<RectTransform>().anchoredPosition.x - panel.GetComponent<RectTransform>().anchoredPosition.x) - buttons[i].GetComponent<RectTransform>().anchoredPosition.x);
        }
        float minDistance = Mathf.Min(distance);

        if (!tapping)
        {
            for (int i = 1; i < buttons.Length - 1; i++)
            {
                if (minDistance == distance[i] && !onStart)
                {
                    minButtonsNumber = i;
                }
            }
        }
        if (!dragging || tapping)
        {
            LerpToButton(minButtonsNumber * -buttonDistance);
        }
    }

    private void SetPerkCards()
    {
        perkPrefabs = Resources.LoadAll<GameObject>(PREFABS_FOLDER);
        for (int i = 0; i < perkPrefabs.Length; i++)
        {
            for (int j = 0; j < perkPrefabs.Length; j++)
            {
                if (perkPrefabs[j].GetComponent<PerkPrefab>().orderNumber == i)
                {
                    GameObject perkCardObj = Instantiate(perkCard) as GameObject;
                    PerkPrefab perk = perkPrefabs[j].GetComponent<PerkPrefab>();
					perk.SetPlayerPrefsParams ();

                    perkCardObj.transform.SetParent(panel);
                    perkCardObj.transform.localPosition = new Vector3(i*DISTANCE, 0, 0);
                    perkCardObj.transform.localScale = new Vector3(1, 1, 1);
					perkCardObj.gameObject.GetComponentsInChildren<Text>()[0].text = perk.shopName + " (" + PlayerPrefs.GetInt(perkPrefabs[j].name).ToString() + ")";
                    //perkCardObj.gameObject.GetComponentsInChildren<Text>()[1].text = perk.description;

                    perkCardObj.gameObject.GetComponentsInChildren<Image>()[1].sprite = perk.perkSprite;
					if (PlayerPrefs.GetInt(perkPrefabs[j].name) == 3)
                    {
						// PERK IS FULLY UPGRADED
                        perkCardObj.gameObject.GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = "UPGRADED";
                    }
                    else
                    {
                        perkCardObj.gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => ShowUpgradePerkWindow(perk.orderNumber));
						perkCardObj.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ShowUpgradePerkWindow(perk.orderNumber));
                        perkCardObj.gameObject.GetComponent<Image>().color = new Color32(188, 188, 188, 255);
                    }
                }
            }
        }
    }

    public void ShowUpgradePerkWindow(int perkOrderNumber)
    {
        fade.SetActive(true);
        closeUnlockPerkWindow.SetActive(true);
        unlockPerkWindow.SetActive(true);
        unlockPerkWindow.GetComponent<UnlockPerkWindow>().SetWindowWithPerkNumber(perkOrderNumber);
    }
	public void ShowUpgradePerkWindowWithStats(int perkOrderNumber)
	{
		
	}
    public void CloseUpgradePerkWindow()
    {
		UpdatePerkMenu ();
        fade.SetActive(false);
        closeUnlockPerkWindow.SetActive(false);
        unlockPerkWindow.SetActive(false);
    }

    public override void LerpToButton(int position)
    {
        base.LerpToButton(position);
    }
    public override void OnButtonClickLerp(int buttonNumber)
    {
        if (buttonNumber == 0)
        {
            minButtonsNumber = 1;
        }
        else if (buttonNumber == buttons.Length - 1)
        {
            minButtonsNumber = buttons.Length - 2;
        } else
        {
            minButtonsNumber = buttonNumber;
        }
        tapping = true;
        MakeActiveButton(buttonNumber);
    }


    public void MakeActiveButton(int buttonNumber)
    {
        buttons[buttonNumber].gameObject.transform.localScale = increasedButttonScale;
        buttons[buttonNumber].GetComponentsInChildren<Text>()[1].enabled = true;

        MakeOtherButtonsInactive(buttonNumber);
    }
    public void MakeInactiveButton(int buttonNumber)
    {
        buttons[buttonNumber].gameObject.transform.localScale = normalButttonScale;
        buttons[buttonNumber].GetComponentsInChildren<Text>()[1].enabled = false;
    }
    private void MakeOtherButtonsInactive(int activeButton)
    {
        for (int i = 0; i < buttons.Length; i++)
        {

            if (i != activeButton)
            {
                MakeInactiveButton(i);
            }
        }
    }

    // Unlocking Perks if can (+payment) or returning FALSE
    public bool CanUpgradePerkByCoins(int perkNumber)
    {
		int perkLevel = PlayerPrefs.GetInt(perkPrefabs[perkNumber].GetComponent<PerkPrefab>().name);
		int coinCost = perkPrefabs[perkNumber].GetComponent<PerkPrefab>().upgradeCoinCost[perkLevel];
		if (PlayerPrefs.GetInt("Coins") >= coinCost && perkLevel < MAX_PERK_LVL)
        {
			PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - coinCost);
			if (!perkPrefabs [perkNumber].GetComponent<PerkPrefab> ().CanUpgradePerk ()) 
			{
				// IF FINAL LVL OF PERK
				buttons [perkPrefabs [perkNumber].GetComponent<PerkPrefab> ().orderNumber].GetComponentsInChildren<Button> () [1].gameObject.GetComponentInChildren<Text> ().text = "ACTIVE";
				buttons [perkPrefabs [perkNumber].GetComponent<PerkPrefab> ().orderNumber].GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
			} 
			else 
			{
				// UPDATING STATUS OF PERK
			}
				
            ShopController.Instance.UpdateMoneyValues();
            return true;
        }
        else
            return false;  
    }
    public bool CanUpgradePerkByCrystals(int perkNumber)
    {
		int perkLevel = PlayerPrefs.GetInt(perkPrefabs[perkNumber].GetComponent<PerkPrefab>().name);
		int crystalCost = perkPrefabs[perkNumber].GetComponent<PerkPrefab>().upgradeCrystalCost[perkLevel];
		if (PlayerPrefs.GetInt("Crystals") >= crystalCost && perkLevel < MAX_PERK_LVL)
		{
			PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") - crystalCost);

			if (!perkPrefabs [perkNumber].GetComponent<PerkPrefab> ().CanUpgradePerk ()) 
			{
				// IF FINAL LVL OF PERK
				buttons [perkPrefabs [perkNumber].GetComponent<PerkPrefab> ().orderNumber].GetComponentsInChildren<Button> () [1].gameObject.GetComponentInChildren<Text> ().text = "ACTIVE";
				buttons [perkPrefabs [perkNumber].GetComponent<PerkPrefab> ().orderNumber].GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
			} 
			else 
			{
				// UPDATING STATUS OF PERK

			}

            ShopController.Instance.UpdateMoneyValues();
            return true;
        }
        else
            return false;
    }

	public void UpdatePerkMenu()
	{
		buttons = new GameObject[panel.transform.childCount];
		distance = new float[buttons.Length];
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i] = panel.GetChild(i).gameObject;
		}
		buttonDistance = (int)DISTANCE;//(int)Mathf.Abs(buttons[1].transform.position.x - buttons[0].transform.position.x);

		minButtonsNumber = 1;
		panel.anchoredPosition = new Vector2(buttons[1].transform.position.x, panel.anchoredPosition.y);


		// make ACTIVE or UNLOCK cards
		for (int i = 0; i < buttons.Length; i++)
		{
			int perkOrderNumber = perkPrefabs[i].GetComponent<PerkPrefab>().orderNumber;
			if (PlayerPrefs.GetInt(perkPrefabs[i].name) == 3)
			{
				buttons[perkOrderNumber].GetComponent<Image>().color = new Color32(167, 167, 167, 255);
				buttons[perkOrderNumber].GetComponentsInChildren<Button>()[1].gameObject.GetComponentInChildren<Text>().text = "UPGRADED";
				buttons [perkOrderNumber].GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ShowUpgradePerkWindowWithStats (perkOrderNumber));
			}
			else
			{
				buttons[perkOrderNumber].GetComponentsInChildren<Text>()[0].text = perkPrefabs[i].GetComponent<PerkPrefab>().shopName + " (" + PlayerPrefs.GetInt(perkPrefabs[i].name).ToString()+")";
				buttons[perkOrderNumber].GetComponentsInChildren<Button>()[1].gameObject.GetComponentInChildren<Text>().text = "ACTIVE";
				buttons [perkOrderNumber].GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ShowUpgradePerkWindow (perkOrderNumber));
				buttons [perkOrderNumber].GetComponentsInChildren<Button> () [1].onClick.AddListener (() => ShowUpgradePerkWindow (perkOrderNumber));
			}
		}
	}
}
