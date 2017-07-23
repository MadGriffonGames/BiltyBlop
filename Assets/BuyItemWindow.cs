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
    Text coins;
    [SerializeField]
    Text crystals;
    [SerializeField]
    Text itemCount; // 2/3 items

    const string itemsFolder = "Sprites/UI/InventoryUI/";

    private string item;

    public void Update()
    {
        itemCount.text = Inventory.Instance.GetItemCount(item).ToString() + "/" + Inventory.Instance.GetItemMaxCount(item).ToString();
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
        crystals.text = PlayerPrefs.GetInt("Crystals").ToString();
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
        GetComponentsInChildren<Text>()[0].text = Inventory.Instance.GetItemShopName(itemName);
        GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(itemsFolder + itemName);

        buyByCoins.GetComponent<Button>().onClick.RemoveAllListeners();
        buyByCrystals.GetComponent<Button>().onClick.RemoveAllListeners();

        buyByCoins.GetComponentInChildren<Text>().text = Inventory.Instance.GetCoinCost(itemName).ToString();
        buyByCoins.GetComponent<Button>().onClick.AddListener(() => Inventory.Instance.BuyItem(itemName, 1, "Coins", Inventory.Instance.GetCoinCost(itemName)));

        buyByCrystals.GetComponentInChildren<Text>().text = Inventory.Instance.GetCrystalCost(itemName).ToString();
        buyByCrystals.GetComponent<Button>().onClick.AddListener(() => Inventory.Instance.BuyItem(itemName, 1, "Crystals", Inventory.Instance.GetCrystalCost(itemName)));
    }
}
