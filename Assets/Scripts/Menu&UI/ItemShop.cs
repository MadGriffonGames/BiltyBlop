using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{

    public GameObject itemButton;
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
            GameObject newButton = Instantiate(itemButton) as GameObject;
            newButton.transform.SetParent(spacer, true);
            newButton.transform.localScale.Set(1, 1, 1);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            newButton.GetComponentInChildren<Text>().text = Inventory.Instance.items[i];
            newButton.GetComponent<Button>().onClick.AddListener(() => ActivateBuyItemWindow(newButton.GetComponentInChildren<Text>().text));
        }
    }

    public void ActivateBuyItemWindow(string itemName)
    {
        closeWindowButton.SetActive(true);
        fade.SetActive(true);
        buyItemWindow.GetComponentInChildren<Text>().text = itemName;
        buyItemWindow.SetActive(true);

        buyByCoins.GetComponent<Button>().onClick.RemoveAllListeners();
        buyByCrystals.GetComponent<Button>().onClick.RemoveAllListeners();

        buyByCoins.GetComponentInChildren<Text>().text = Inventory.Instance.GetCoinCost(itemName).ToString();
        buyByCoins.GetComponent<Button>().onClick.AddListener(() => Inventory.Instance.BuyItem(itemName, 1, true));

        buyByCrystals.GetComponentInChildren<Text>().text = Inventory.Instance.GetCrystalCost(itemName).ToString();
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
