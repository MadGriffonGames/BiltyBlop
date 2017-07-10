using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemShop : MonoBehaviour
{

    public GameObject item;
    public Transform spacer;

    [SerializeField]
    public GameObject errorWindow;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject closeWindowButton;
    [SerializeField]
    GameObject buyItemWindow;
    [SerializeField]
    Button buyByCrystals;
    [SerializeField]
    Button buyByCoins;

    const string itemsFolder = "Sprites/UI/InventoryUI/";

    bool onStart;

    void Start () {
        onStart = true;
	}
	
	void Update () {
        if (Inventory.Instance.isActiveAndEnabled && onStart)
        {
            SetItemsButtons();
            onStart = false;  
        }
		
	}   

    void SetItemsButtons()
    {
        for (int i = 0; i < Inventory.Instance.items.Length; i++)
        {
            GameObject newItem = Instantiate(item) as GameObject;
            newItem.transform.SetParent(spacer, true);
            
            newItem.transform.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<Text>().text = Inventory.Instance.items[i];
            newItem.GetComponentInChildren<Button>().onClick.AddListener(() => ActivateBuyItemWindow(newItem.GetComponentInChildren<Text>().text));
            newItem.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(itemsFolder + Inventory.Instance.items[i]);
        }
    }


    public void ActivateBuyItemWindow(string itemName)
    {
        int itemNumber = 0;
        for (int i = 0; i < Inventory.Instance.items.Length; i++)
        {
            if (Inventory.Instance.items[i] == itemName)
            {
                itemNumber = i;
                break;
            }
        }

        closeWindowButton.SetActive(true);
        fade.SetActive(true);
        buyItemWindow.GetComponentInChildren<Text>().text = itemName;
        buyItemWindow.SetActive(true);
        buyItemWindow.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(itemsFolder + Inventory.Instance.items[itemNumber]);

        buyByCoins.GetComponent<Button>().onClick.RemoveAllListeners();
        buyByCrystals.GetComponent<Button>().onClick.RemoveAllListeners();

        buyByCoins.GetComponentInChildren<Text>().text = Inventory.Instance.GetCoinCostOfItem(itemName).ToString();
        buyByCoins.GetComponent<Button>().onClick.AddListener(() => Inventory.Instance.BuyItem(itemName, 1, true));

        buyByCrystals.GetComponentInChildren<Text>().text = Inventory.Instance.GetCrystalCostOfItem(itemName).ToString();
        buyByCrystals.GetComponent<Button>().onClick.AddListener(() => Inventory.Instance.BuyItem(itemName, 1, false));
    }

    public void ShowErrorWindow(string error)
    {
            fade.SetActive(true);
            errorWindow.SetActive(true);
            closeWindowButton.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = error;
    }

    public void CloseBuyItemWindow()
    {
        fade.SetActive(false);
        buyItemWindow.SetActive(false);
        closeWindowButton.SetActive(false);
    }
}
