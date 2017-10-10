using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemWindow : MonoBehaviour {

    [SerializeField]
    Button buyByCrystals;
    [SerializeField]
    Button buyByCoins;
    [SerializeField]
    Text itemCount; // 2/3 items
	[SerializeField]
	Text buyCount;


    const string itemsFolder = "Sprites/UI/InventoryUI/";

    private string item;

    public void Update()
    {
        itemCount.text = Inventory.Instance.GetItemCount(item).ToString();
    }

    public void SetBuyItemWindow(string itemName)
    {
        item = itemName;
        int itemNumber = 0;
        for (int i = 0; i < Inventory.Instance.itemsNames.Length; i++)
        {
            if (Inventory.Instance.itemsNames[i] == itemName)
            {
                itemNumber = i;
                break;
            }
        }
		buyCount.text = "1";
        GetComponentsInChildren<Text>()[0].text = Inventory.Instance.GetItemShopName(itemName);
        GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(itemsFolder + itemName);

        buyByCoins.GetComponent<Button>().onClick.RemoveAllListeners();
        buyByCrystals.GetComponent<Button>().onClick.RemoveAllListeners();

		buyByCoins.GetComponentInChildren<Text>().text = (Inventory.Instance.GetCoinCost(itemName) * int.Parse (buyCount.text)).ToString();
        buyByCoins.GetComponent<Button>().onClick.AddListener(() => Inventory.Instance.BuyItem(itemName, 1, "Coins", Inventory.Instance.GetCoinCost(itemName)));

		buyByCrystals.GetComponentInChildren<Text>().text = (Inventory.Instance.GetCrystalCost(itemName) * int.Parse (buyCount.text)).ToString();
        buyByCrystals.GetComponent<Button>().onClick.AddListener(() => Inventory.Instance.BuyItem(itemName, 1, "Crystals", Inventory.Instance.GetCrystalCost(itemName)));
    }

	public void PlusButton()
	{
		buyCount.text = (int.Parse (buyCount.text) + 1).ToString ();
		UpdateValues ();
	}

	public void MinusButton()
	{
		buyCount.text = (int.Parse (buyCount.text) - 1).ToString ();
		UpdateValues ();
	}
	private void UpdateValues()
	{
		buyByCoins.GetComponent<Button>().onClick.RemoveAllListeners();
		buyByCrystals.GetComponent<Button>().onClick.RemoveAllListeners();

		buyByCoins.GetComponentInChildren<Text>().text = (Inventory.Instance.GetCoinCost(item) * int.Parse (buyCount.text)).ToString();
		buyByCrystals.GetComponentInChildren<Text>().text = (Inventory.Instance.GetCrystalCost(item) * int.Parse (buyCount.text)).ToString();

		buyByCoins.GetComponent<Button>().onClick.AddListener(() => Inventory.Instance.BuyItem(item, int.Parse (buyCount.text), "Coins", Inventory.Instance.GetCoinCost(item)));
		buyByCrystals.GetComponent<Button>().onClick.AddListener(() => Inventory.Instance.BuyItem(item, int.Parse (buyCount.text), "Crystals", Inventory.Instance.GetCrystalCost(item)));
	}
}
