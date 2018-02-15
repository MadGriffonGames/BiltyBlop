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

    const string itemsFolder = "Sprites/UI/InventoryUI/";

    bool onStart;

    void Start ()
    {
        onStart = true;
	}
	
	void Update ()
    {
        if (Inventory.Instance.isActiveAndEnabled && onStart)
        {
            SetItemsButtons();
            onStart = false;  
        }
	}   

    void SetItemsButtons()
    {
        for (int i = 0; i < Inventory.Instance.itemsNames.Length; i++)
        {
            GameObject newItem = Instantiate(item) as GameObject;
            newItem.transform.SetParent(spacer, true);
            
            newItem.transform.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentsInChildren<Text>()[0].text = Inventory.Instance.GetItemShopName(Inventory.Instance.itemsNames[i]); // shopName
            newItem.GetComponentsInChildren<Text>()[1].text = Inventory.Instance.itemsNames[i];                                     // itemName
            newItem.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => ActivateBuyItemWindow(newItem.GetComponentsInChildren<Text>()[1].text));
			newItem.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ActivateBuyItemWindow(newItem.GetComponentsInChildren<Text>()[1].text));
            newItem.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(itemsFolder + Inventory.Instance.itemsNames[i]);
        }
    }


    public void ActivateBuyItemWindow(string itemName)
    {
        closeWindowButton.SetActive(true);
        fade.SetActive(true);
        buyItemWindow.SetActive(true);
        buyItemWindow.GetComponent<BuyItemWindow>().SetBuyItemWindow(itemName);
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
