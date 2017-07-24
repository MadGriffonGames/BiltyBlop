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
            if (perkPrefabs[i].GetComponent<PerkPrefab>().isLocked)
            {
                buttons[perkOrderNumber].GetComponent<Image>().color = new Color32(167, 167, 167, 255);
                buttons[perkOrderNumber].GetComponentsInChildren<Button>()[1].gameObject.GetComponentInChildren<Text>().text = "UNLOCK";
            }
            else
            {
                buttons[perkOrderNumber].GetComponentsInChildren<Button>()[1].gameObject.GetComponentInChildren<Text>().text = "ACTIVE";
                buttons[perkOrderNumber].GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
            }
        }
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

                    perkCardObj.transform.SetParent(panel);
                    perkCardObj.transform.localPosition = new Vector3(i*DISTANCE, 0, 0);
                    perkCardObj.transform.localScale = new Vector3(1, 1, 1);
                    perkCardObj.gameObject.GetComponentsInChildren<Text>()[0].text = perk.shopName;
                    perkCardObj.gameObject.GetComponentsInChildren<Text>()[1].text = perk.description;

                    perkCardObj.gameObject.GetComponentsInChildren<Image>()[1].sprite = perk.perkSprite;
                    if (!perk.isLocked)
                    {
                        perkCardObj.gameObject.GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = "ACTIVE";
                    }
                    else
                    {
                        perkCardObj.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ShowUnlockPerkWindow(perk.orderNumber));
                        perkCardObj.gameObject.GetComponent<Image>().color = new Color32(188, 188, 188, 255);
                    }
                }
            }
        }
    }

    public void ShowUnlockPerkWindow(int perkOrderNumber)
    {
        fade.SetActive(true);
        closeUnlockPerkWindow.SetActive(true);
        unlockPerkWindow.SetActive(true);
        unlockPerkWindow.GetComponent<UnlockPerkWindow>().SetWindowWithPerkNumber(perkOrderNumber);
    }
    public void CloseUnlockPerkWindow()
    {
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
    public bool CanUnlockPerkByCoins(int perkNumber)
    {
        if (PlayerPrefs.GetInt("Coins") >= perkPrefabs[perkNumber].GetComponent<PerkPrefab>().coinCost)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - perkPrefabs[perkNumber].GetComponent<PerkPrefab>().coinCost);
            perkPrefabs[perkNumber].GetComponent<PerkPrefab>().UnlockPerk();
            buttons[perkPrefabs[perkNumber].GetComponent<PerkPrefab>().orderNumber].GetComponentsInChildren<Button>()[1].gameObject.GetComponentInChildren<Text>().text = "ACTIVE";
            buttons[perkPrefabs[perkNumber].GetComponent<PerkPrefab>().orderNumber].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ShopController.Instance.UpdateMoneyValues();
            return true;
        }
        else
            return false;  
    }
    public bool CanUnlockPerkByCrystals(int perkNumber)
    {
        if (PlayerPrefs.GetInt("Crystals") >= perkPrefabs[perkNumber].GetComponent<PerkPrefab>().crystalCost)
        {
            PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") - perkPrefabs[perkNumber].GetComponent<PerkPrefab>().crystalCost);
            perkPrefabs[perkNumber].GetComponent<PerkPrefab>().UnlockPerk();
            buttons[perkPrefabs[perkNumber].GetComponent<PerkPrefab>().orderNumber].GetComponentsInChildren<Button>()[1].gameObject.GetComponentInChildren<Text>().text = "ACTIVE";
            buttons[perkPrefabs[perkNumber].GetComponent<PerkPrefab>().orderNumber].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ShopController.Instance.UpdateMoneyValues();
            return true;
        }
        else
            return false;
    }
}
